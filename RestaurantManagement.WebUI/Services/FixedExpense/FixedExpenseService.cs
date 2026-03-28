using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;

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
            await _httpClient.PostAsJsonAsync("https://localhost:7110/api/FixedExpenses/CreateFixedExpense", dto);
        }

        public async  Task DeleteFixedExpense(string id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7110/api/FixedExpenses/{id}");
        }

        public async Task<List<ResultFixedExpense>> GetAllFixedExpense()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultFixedExpense>>("https://localhost:7110/api/FixedExpenses/GetAllFixedExpense");
            return values;
        }

        public async Task<List<ResultFixedExpense>> GetFixedExpensesByShiftAsync(string shift)
        {
            // API'deki endpoint'i çağırıyoruz
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7110/api/FixedExpenses/GetFixedExpensesByShift/{shift}");
            return await responseMessage.Content.ReadFromJsonAsync<List<ResultFixedExpense>>();
        }

        public async Task<UpdateFixedExpense> GetByIdFixedExpense(string id)
        {
            var values = await _httpClient.GetFromJsonAsync<UpdateFixedExpense>("https://localhost:7110/api/FixedExpenses/" + id);
            return values;
        }

        public async Task UpdateFixedExpense(UpdateFixedExpense dto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFixedExpense>("https://localhost:7110/api/FixedExpenses/UpdateFixedExpense", dto);
        }
    }
}
