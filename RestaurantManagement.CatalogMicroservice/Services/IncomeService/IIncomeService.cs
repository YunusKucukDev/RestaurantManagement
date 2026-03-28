using AutoMapper;
using MongoDB.Driver;
using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Entities.InComes;
using RestaurantManagement.CatalogMicroservice.Settings;

namespace RestaurantManagement.CatalogMicroservice.Services.IncomeService
{
    public interface IIncomeService
    {
        Task<List<ResultIncomeDto>> GetAllIncomes();
        Task<GetByIdIncomeDto> GetByIdAbotDto(string id);
        Task<List<ResultIncomeDto>> GetIncomesByShiftAsync(string shift);
        Task UpdateIncomeDto(UpdateInComeDtos updateIncomeDto);
        Task DeleteIncomeDto(string id);
        Task CreateIncomeDto(CreateInComeDto createIncomeDto);
    }
}
