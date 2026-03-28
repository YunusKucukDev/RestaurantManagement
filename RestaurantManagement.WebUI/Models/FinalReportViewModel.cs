namespace RestaurantManagement.WebUI.Models
{
    public class FinalReportViewModel
    {
        public string Id { get; set; }
        public string ReportName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CostItem> SelectedCosts { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
    }

    public class CostItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
