using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;
using System.Net.Http.Json; // PostAsJsonAsync, GetFromJsonAsync vb. için

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
            await _httpClient.PostAsJsonAsync("Outcomes", createOutcomeDto);
        }

        public async Task DeleteOutcomeDto(string id)
        {
            await _httpClient.DeleteAsync($"Outcomes/{id}");
        }

        public async Task<List<ResultOutcomeDto>> GetOutcomesByShiftAsync(string shift)
        {
            var responseMessage = await _httpClient.GetAsync($"Outcomes/GetOutcomesByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultOutcomeDto>>();
                return values ?? new List<ResultOutcomeDto>();
            }

            return new List<ResultOutcomeDto>();
        }

        public async Task<List<ResultOutcomeDto>> GetAllOutcomes()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultOutcomeDto>>("Outcomes");
            return values ?? new List<ResultOutcomeDto>();
        }

        public async Task<UpdateOutComeDto> GetByIdAbotDto(string id)
        {
            // Burada ID parametresini URL'e eklemeyi unutmamalıyız:
            var values = await _httpClient.GetFromJsonAsync<UpdateOutComeDto>($"Outcomes/{id}");
            return values;
        }

        public async Task UpdateOutcomeDto(UpdateOutComeDto updateOutcomeDto)
        {
            await _httpClient.PutAsJsonAsync("Outcomes", updateOutcomeDto);
        }
    }
}