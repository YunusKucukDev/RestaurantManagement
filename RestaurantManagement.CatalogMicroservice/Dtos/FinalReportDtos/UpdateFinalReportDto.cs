using RestaurantManagement.CatalogMicroservice.Entities.FinalReport;

namespace RestaurantManagement.CatalogMicroservice.Dtos.FinalReportDtos
{
    public class UpdateFinalReportDto
    {
        public string Id { get; set; }
        public string ReportName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CostItem> SelectedCosts { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public string ShiftType { get; set; }
    }
}
