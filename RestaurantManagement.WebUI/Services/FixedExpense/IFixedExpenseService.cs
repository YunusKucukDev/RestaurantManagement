using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;

namespace RestaurantManagement.WebUI.Services.FixedExpense
{
    public interface IFixedExpenseService
    {
        Task<List<ResultFixedExpense>> GetAllFixedExpense();
        Task<UpdateFixedExpense> GetByIdFixedExpense(string id);
        Task<List<ResultFixedExpense>> GetFixedExpensesByShiftAsync(string shift);
        Task CreateFixedExpense(CreateFixedExpense dto);
        Task UpdateFixedExpense(UpdateFixedExpense dto);
        Task DeleteFixedExpense(string id);
    }
}
