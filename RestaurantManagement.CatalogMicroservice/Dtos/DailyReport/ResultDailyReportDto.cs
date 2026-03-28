namespace RestaurantManagement.CatalogMicroservice.Dtos.DailyReport
{
    public class ResultDailyReportDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }

        // O günün toplam geliri (Örn: Tüm satışlar)
        public decimal TotalIncome { get; set; }

        // O günün toplam gideri (Örn: Malzeme alımları)
        public decimal TotalOutcome { get; set; }

        // Net Kar (Income - Outcome)
        public decimal NetProfit { get; set; }

        // Kartın üzerinde "12 İşlem Yapıldı" gibi bilgi göstermek istersen:
        public int TransactionCount { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
