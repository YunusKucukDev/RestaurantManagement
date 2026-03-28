using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;
using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;
using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;

namespace RestaurantManagement.WebUI.Models
{
    public class PeriodicalAnalysisViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PeriodTotalIncome { get; set; } // O tarihler arası toplam gelir
        public decimal PeriodTotalOutcome { get; set; } // O tarihler arası toplam kasa gideri

        public List<ResultIncomeDto> IncomeDetails { get; set; } // O dönemdeki tüm gelirler
        public List<ResultOutcomeDto> OutcomeDetails { get; set; } // O dönemdeki tüm giderler

        public List<ResultFixedExpense> AllFixedExpenses { get; set; } // Sürükle-bırak için sağ kutu
    }
}
