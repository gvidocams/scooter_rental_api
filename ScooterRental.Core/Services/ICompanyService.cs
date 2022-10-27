using ScooterRental.Core.Models;
using ScooterRental.Models;

namespace ScooterRental.Core.Services
{
    public interface ICompanyService : IEntityService<RentalCompany>
    {
        bool Exists(int id);
        bool Exists(RentalCompany company);
        Scooter Create(int id, Scooter scooter);
        Scooter StartRent(RentalCompany company, Scooter scooter);
        decimal EndRent(Scooter scooter);
        decimal CalculateIncome(int id, IncomeReportRequest request);
    }
}