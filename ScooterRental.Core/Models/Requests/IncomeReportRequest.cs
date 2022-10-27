namespace ScooterRental.Models
{
    public class IncomeReportRequest
    {
        public int? year { get; set; }
        public bool IncludeNotCompletedRentals { get; set; }
    }
}