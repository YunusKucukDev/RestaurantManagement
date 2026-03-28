namespace RestaurantManagement.CatalogMicroservice.Dtos.DailyReport
{
    public class CreateDailyReportDto
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal TotalIncome { get; set; }
        public decimal TotalOutcome { get; set; }
        public decimal NetProfit { get; set; }
        public int TransactionCount { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
