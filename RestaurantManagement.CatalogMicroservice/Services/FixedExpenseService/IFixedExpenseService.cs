using RestaurantManagement.CatalogMicroservice.Dtos.FixedExpenseDto;

namespace RestaurantManagement.CatalogMicroservice.Services.FixedExpenseService
{
    public interface IFixedExpenseService
    {
        Task<List<ResultFixedExpenseDto>> GetAllFixedExpenseDto();
        Task <UpdateFixedExpensedto> GetByIdFixedExpenseDto(string id);
        Task<List<ResultFixedExpenseDto>> GetFixedExpensesByShiftAsync(string shift);
        Task CreateFixedExpenseDto(CreateFixedExpenseDto dto);
        Task UpdateFixedExpenseDto(UpdateFixedExpensedto dto);
        Task DeleteFixedExpenseDto(string id);
    }
}
