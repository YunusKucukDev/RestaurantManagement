using RestaaurantManagement.DtoLayer.Dtos.FinalReportDtos;
using RestaurantManagement.WebUI.Models;

namespace RestaurantManagement.WebUI.Services.FinalReportService
{
    public interface IFinalReportService
    {
        Task<List<ResultFinalReportDto>> GetAllFinalReports();
        Task<ResultFinalReportDto> GetByIdFinalReport(string id);
        Task<List<ResultFinalReportDto>> GetFinalReportsByShiftAsync(string shift);
        Task DeleteFinalReport(string id);
        Task CreateFinalReport(CreateFinalReportDto dto);
    }
}
