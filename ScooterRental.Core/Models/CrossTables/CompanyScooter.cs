using ScooterRental.Core.Models;

namespace ScooterRental.Core.CrossTables
{
    public class CompanyScooter : Entity
    {
        public RentalCompany RentalCompany { get; set; }
        public Scooter Scooter { get; set; }
    }
}