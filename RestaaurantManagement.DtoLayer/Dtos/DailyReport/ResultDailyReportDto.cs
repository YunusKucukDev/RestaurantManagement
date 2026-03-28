namespace RestaaurantManagement.DtoLayer.Dtos.DailyReport
{
    public class ResultDailyReportDto
    {
        public string Id { get; set; } // MongoDB kullanıyorsan string
        public DateTime Date { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalOutcome { get; set; }
        public decimal NetProfit { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
