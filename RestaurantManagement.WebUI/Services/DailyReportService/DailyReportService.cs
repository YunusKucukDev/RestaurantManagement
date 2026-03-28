using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.DailyReport;
using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;
using System.Net.Http.Json; // PostAsJsonAsync ve GetFromJsonAsync için gerekli

namespace RestaurantManagement.WebUI.Services.DailyReportService
{
    public class DailyReportService : IDailyReportService
    {
        private readonly HttpClient _httpClient;

        public DailyReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateDailyReports(CreateDailyReportDto dto)
        {
            // Artık sadece endpoint adı yeterli!
            await _httpClient.PostAsJsonAsync("DailyReports", dto);
        }

        public async Task<List<ResultDailyReportDto>> GetAllDailyReports()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultDailyReportDto>>("DailyReports");
            return values ?? new List<ResultDailyReportDto>();
        }

        public async Task<List<ResultDailyReportDto>> GetDailyReportsByShiftAsync(string shift)
        {
            // URL birleştirme işlemini sadece endpoint bazlı yapıyoruz
            var responseMessage = await _httpClient.GetAsync($"DailyReports/GetAllByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultDailyReportDto>>();
                return values ?? new List<ResultDailyReportDto>();
            }

            return new List<ResultDailyReportDto>();
        }

        public async Task DeleteDailyReports(string id)
        {
            await _httpClient.DeleteAsync($"DailyReports/{id}");
        }
    }
}