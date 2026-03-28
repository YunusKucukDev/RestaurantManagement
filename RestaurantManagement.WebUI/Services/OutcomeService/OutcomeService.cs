using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;

namespace RestaurantManagement.WebUI.Services.OutcomeService
{
    public class OutcomeService : IOutcomeService
    {
        private readonly HttpClient _httpClient;
        public OutcomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOutcomeDto(CreateOutcomeDto createOutcomeDto)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7110/api/Outcomes", createOutcomeDto);

        }

        public async Task DeleteOutcomeDto(string id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7110/api/Outcomes/{id}");
        }

        public async Task<List<ResultOutcomeDto>> GetOutcomesByShiftAsync(string shift)
        {
            // API tarafındaki OutcomesController içinde yazdığımız endpoint'e istek atar
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7110/api/Outcomes/GetOutcomesByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultOutcomeDto>>();
                return values;
            }

            return new List<ResultOutcomeDto>();
        }

        public async Task<List<ResultOutcomeDto>> GetAllOutcomes()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultOutcomeDto>>("https://localhost:7110/api/Outcomes");
            return values;
        }

        public async Task<UpdateOutComeDto> GetByIdAbotDto(string id)
        {
            var values = await _httpClient.GetFromJsonAsync<UpdateOutComeDto>("https://localhost:7110/api/Outcomes");
            return values;
        }

        public async Task UpdateOutcomeDto(UpdateOutComeDto updateOutcomeDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateOutComeDto>("https://localhost:7110/api/Outcomes", updateOutcomeDto);
        }
    
    }
}
