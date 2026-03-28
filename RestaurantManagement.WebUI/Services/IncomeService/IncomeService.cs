using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;
using System.Net.Http.Json; // PostAsJsonAsync, GetFromJsonAsync vb. için

namespace RestaurantManagement.WebUI.Services.IncomeService
{
    public class IncomeService : IIncomeService
    {
        private readonly HttpClient _httpClient;
        public IncomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateIncomeDto(CreateInComeDto createIncomeDto)
        {
            await _httpClient.PostAsJsonAsync("Incomes", createIncomeDto);
        }

        public async Task DeleteIncomeDto(string id)
        {
            await _httpClient.DeleteAsync($"Incomes/{id}");
        }

        public async Task<List<ResultIncomeDto>> GetIncomesByShiftAsync(string shift)
        {
            var responseMessage = await _httpClient.GetAsync($"Incomes/GetIncomesByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultIncomeDto>>();
                return values ?? new List<ResultIncomeDto>();
            }

            return new List<ResultIncomeDto>();
        }

        public async Task<List<ResultIncomeDto>> GetAllIncomes()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultIncomeDto>>("Incomes");
            return values ?? new List<ResultIncomeDto>();
        }

        public async Task<UpdateInComeDtos> GetByIdAbotDto(string id)
        {
            // BaseAddress ile otomatik birleşir
            var values = await _httpClient.GetFromJsonAsync<UpdateInComeDtos>($"Incomes/{id}");
            return values;
        }

        public async Task UpdateIncomeDto(UpdateInComeDtos updateIncomeDto)
        {
            await _httpClient.PutAsJsonAsync("Incomes", updateIncomeDto);
        }
    }
}