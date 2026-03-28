using RestaaurantManagement.DtoLayer.Dtos.FinalReportDtos;

using RestaurantManagement.WebUI.Models;

namespace RestaurantManagement.WebUI.Services.FinalReportService
{
    public class FinalReportService : IFinalReportService
    {
        private readonly HttpClient _httpClient;

        public FinalReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFinalReport(CreateFinalReportDto dto)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7110/api/FinalReports", dto);
        }

        public async Task<List<ResultFinalReportDto>> GetFinalReportsByShiftAsync(string shift)
        {
            // API'deki "GetFinalReportsByShift" endpoint'ine istek atar
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7110/api/FinalReports/GetFinalReportsByShift/{shift}");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFinalReportDto>>();
            return values;
        }

        public async Task DeleteFinalReport(string id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7110/api/FinalReports/{id}");
        }

        public async Task<List<ResultFinalReportDto>> GetAllFinalReports()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultFinalReportDto>>("https://localhost:7110/api/FinalReports");
            return values;
        }

        public async Task<ResultFinalReportDto> GetByIdFinalReport(string id)
        {
            var values = await _httpClient.GetFromJsonAsync<ResultFinalReportDto>("https://localhost:7110/api/FinalReports");
            return values;
        }
    }
}
