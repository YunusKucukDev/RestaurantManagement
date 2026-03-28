using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;
using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;
using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaaurantManagement.DtoLayer.Dtos.FinalReportDtos
{
    public class CreateFinalReportDto
    {
        public string ReportName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CostItemDtos> SelectedCosts { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }

        public List<ResultIncomeDto> IncomeDetails { get; set; } // O dönemdeki tüm gelirler
        public List<ResultOutcomeDto> OutcomeDetails { get; set; } // O dönemdeki tüm giderler



        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
