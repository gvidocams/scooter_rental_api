using System;

namespace ScooterRental.Core.Models
{
    public class RentalDetail : Entity
    {
        public int ScooterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}