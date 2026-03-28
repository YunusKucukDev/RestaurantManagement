using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;

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
            await _httpClient.PostAsJsonAsync("https://localhost:7110/api/Incomes", createIncomeDto);

        }

        public async Task DeleteIncomeDto(string id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7110/api/Incomes/{id}");
        }

        public async Task<List<ResultIncomeDto>> GetIncomesByShiftAsync(string shift)
        {
           
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7110/api/Incomes/GetIncomesByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultIncomeDto>>();
                return values;
            }

            return new List<ResultIncomeDto>();
        }

        public async Task<List<ResultIncomeDto>> GetAllIncomes()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultIncomeDto>>("https://localhost:7110/api/Incomes");
            return values;
        }

        public async Task<UpdateInComeDtos> GetByIdAbotDto(string id)
        {
            var values = await _httpClient.GetFromJsonAsync<UpdateInComeDtos>("https://localhost:7110/api/Incomes/"+id);
            return values;
        }

        public async Task UpdateIncomeDto(UpdateInComeDtos updateIncomeDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateInComeDtos>("https://localhost:7110/api/Incomes", updateIncomeDto);
        }
    }
}
