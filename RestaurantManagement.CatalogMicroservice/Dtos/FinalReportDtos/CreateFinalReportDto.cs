using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos;
using RestaurantManagement.CatalogMicroservice.Entities.FinalReport;

namespace RestaurantManagement.CatalogMicroservice.Dtos.FinalReportDtos
{
    public class CreateFinalReportDto
    {
       
        public string ReportName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CostItem> SelectedCosts { get; set; }

        public List<ResultIncomeDto> IncomeDetails { get; set; } // O dönemdeki tüm gelirler
        public List<ResultOutcomeDto> OutcomeDetails { get; set; } // O dönemdeki tüm giderler

        public decimal NetTotal { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public string ShiftType { get; set; }
    }
}
