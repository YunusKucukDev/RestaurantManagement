using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;
using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;

namespace RestaurantManagement.WebUI.Models
{
    public class FixedExpensesDetailViewModel
    {
        public List<ResultIncomeDto> Incomes { get; set; }
        public List<ResultOutcomeDto> Outcomes { get; set; }
        public DateTime StartDate { get; set; }
        public string ShiftType { get; set; }
        public DateTime EndDate { get; set; }
    }
}
