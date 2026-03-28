using RestaurantManagement.CatalogMicroservice.Dtos.DailyReport;

namespace RestaurantManagement.CatalogMicroservice.Services.DailyReportService
{
    public interface IDailyReportService
    {
        Task<List<ResultDailyReportDto>> GetAllDailyReports();
        Task CreateDailyReport(CreateDailyReportDto dto);
        Task<List<ResultDailyReportDto>> GetDailyReportsByShiftAsync(string shift);
        Task DeleteDailyReport(string id);
    }
}
