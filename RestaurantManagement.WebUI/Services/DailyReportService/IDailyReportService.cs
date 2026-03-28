using RestaaurantManagement.DtoLayer.Dtos.DailyReport;

namespace RestaurantManagement.WebUI.Services.DailyReportService
{
    public interface IDailyReportService
    {
        Task<List<ResultDailyReportDto>> GetAllDailyReports();
        Task<List<ResultDailyReportDto>> GetDailyReportsByShiftAsync(string shift);
        Task CreateDailyReports(CreateDailyReportDto dto);
        Task DeleteDailyReports(string id);
    }
}
