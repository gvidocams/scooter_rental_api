using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScooterRental.Core.CrossTables;
using ScooterRental.Core.Models;

namespace ScooterRental.Data
{
    public class ScooterRentalDbContext : DbContext, IScooterRentalDbContext
    {
        public ScooterRentalDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<RentalCompany> RentalCompanies { get; set; }
        public DbSet<CompanyScooter> CompaniesScooters { get; set; }
        public DbSet<CompanyRentalDetail> CompanyRentalDetails { get; set; }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}