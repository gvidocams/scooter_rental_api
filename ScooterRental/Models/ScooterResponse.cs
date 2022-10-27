namespace ScooterRental.Models
{
    public class ScooterResponse
    {
        public int Id { get; set; }
        public decimal PricePerMinute { get; set; }
        public bool IsRented { get; set; }
    }
}