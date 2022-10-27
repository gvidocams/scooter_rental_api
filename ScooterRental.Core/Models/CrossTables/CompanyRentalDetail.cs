using ScooterRental.Core.Models;

namespace ScooterRental.Core.CrossTables
{
    public class CompanyRentalDetail : Entity
    {
        public RentalCompany RentalCompany { get; set; }
        public RentalDetail RentalDetail { get; set; }
    }
}