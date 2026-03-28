using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;
using System.Net.Http.Json; // PostAsJsonAsync, GetFromJsonAsync vb. için

namespace RestaurantManagement.WebUI.Services.FixedExpense
{
    public class FixedExpenseService : IFixedExpenseService
    {
        private readonly HttpClient _httpClient;

        public FixedExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFixedExpense(CreateFixedExpense dto)
        {
            // Sadece endpoint ve action ismi yeterli
            await _httpClient.PostAsJsonAsync("FixedExpenses/CreateFixedExpense", dto);
        }

        public async Task DeleteFixedExpense(string id)
        {
            await _httpClient.DeleteAsync($"FixedExpenses/{id}");
        }

        public async Task<List<ResultFixedExpense>> GetAllFixedExpense()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultFixedExpense>>("FixedExpenses/GetAllFixedExpense");
            return values ?? new List<ResultFixedExpense>();
        }

        public async Task<List<ResultFixedExpense>> GetFixedExpensesByShiftAsync(string shift)
        {
            var responseMessage = await _httpClient.GetAsync($"FixedExpenses/GetFixedExpensesByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultFixedExpense>>() ?? new List<ResultFixedExpense>();
            }
            return new List<ResultFixedExpense>();
        }

        public async Task<UpdateFixedExpense> GetByIdFixedExpense(string id)
        {
            // "FixedExpenses/" + id kullanımı BaseAddress ile otomatik birleşir
            var values = await _httpClient.GetFromJsonAsync<UpdateFixedExpense>($"FixedExpenses/{id}");
            return values;
        }

        public async Task UpdateFixedExpense(UpdateFixedExpense dto)
        {
            await _httpClient.PutAsJsonAsync("FixedExpenses/UpdateFixedExpense", dto);
        }
    }
}