using RestaaurantManagement.DtoLayer.Dtos.FinalReportDtos;
using RestaurantManagement.WebUI.Models;
using System.Net.Http.Json; // PostAsJsonAsync ve GetFromJsonAsync için

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
            // Sadece endpoint ismi
            await _httpClient.PostAsJsonAsync("FinalReports", dto);
        }

        public async Task<List<ResultFinalReportDto>> GetFinalReportsByShiftAsync(string shift)
        {
            var responseMessage = await _httpClient.GetAsync($"FinalReports/GetFinalReportsByShift/{shift}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFinalReportDto>>();
                return values ?? new List<ResultFinalReportDto>();
            }
            return new List<ResultFinalReportDto>();
        }

        public async Task DeleteFinalReport(string id)
        {
            await _httpClient.DeleteAsync($"FinalReports/{id}");
        }

        public async Task<List<ResultFinalReportDto>> GetAllFinalReports()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultFinalReportDto>>("FinalReports");
            return values ?? new List<ResultFinalReportDto>();
        }

        public async Task<ResultFinalReportDto> GetByIdFinalReport(string id)
        {
            // DİKKAT: Burada GetByID için endpoint'e ID eklemeyi unutmuşsun gibi görünüyor. 
            // Şöyle düzelttim:
            var values = await _httpClient.GetFromJsonAsync<ResultFinalReportDto>($"FinalReports/{id}");
            return values;
        }
    }
}