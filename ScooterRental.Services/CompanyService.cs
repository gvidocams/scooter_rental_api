using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScooterRental.Core.Calculate;
using ScooterRental.Core.CrossTables;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;
using ScooterRental.Models;

namespace ScooterRental.Services
{
    public class CompanyService : EntityService<RentalCompany>, ICompanyService
    {
        public CompanyService(IScooterRentalDbContext context) : base(context) {}

        public bool Exists(int id)
        {
            return _context.RentalCompanies.Any(c => c.Id == id);
        }

        public bool Exists(RentalCompany company)
        {
            return _context.RentalCompanies.Any(c => c.Name.ToLower() == company.Name.ToLower());
        }

        public Scooter Create(int id, Scooter scooter)
        {
            var company = _context.RentalCompanies.FirstOrDefault(c => c.Id == id);

            _context.Scooters.Add(scooter);

            var companyScooter = new CompanyScooter
            {
                RentalCompany = company,
                Scooter = scooter
            };

            _context.CompaniesScooters.Add(companyScooter);
            _context.SaveChanges();

            return scooter;
        }

        public Scooter StartRent(RentalCompany company, Scooter scooter)
        {
            var rentalDetail = new RentalDetail
            {
                ScooterId = scooter.Id,
                StartTime = DateTime.Now,
                PricePerMinute = scooter.PricePerMinute,
            };

            var companyRentalDetail = new CompanyRentalDetail
            {
                RentalCompany = company,
                RentalDetail = rentalDetail
            };

            scooter.IsRented = true;

            _context.CompanyRentalDetails.Add(companyRentalDetail);
            _context.SaveChanges();
            
            return scooter;
        }

        public decimal EndRent(Scooter scooter)
        {
            var rentalDetail = _context.CompanyRentalDetails
                .Include(d => d.RentalDetail)
                .FirstOrDefault(d => d.RentalDetail.ScooterId == scooter.Id &&
                                     !d.RentalDetail.EndTime.HasValue)
                .RentalDetail;

            rentalDetail.EndTime = DateTime.Now;

            scooter.IsRented = false;

            _context.SaveChanges();

            var income = Income.TotalRentalPrice(rentalDetail);

            return income;
        }

        public decimal CalculateIncome(int id, IncomeReportRequest request)
        {
            var companyRentalDetails = _context.CompanyRentalDetails
                .Include(d => d.RentalDetail)
                .Where(d => d.RentalCompany.Id == id)
                .ToList();

            var rentalDetails = companyRentalDetails.ConvertAll(d => d.RentalDetail);

            var income = Total.TotalIncome(rentalDetails, request);

            return income;
        }
    }
}