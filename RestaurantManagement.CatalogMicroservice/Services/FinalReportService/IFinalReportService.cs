using RestaurantManagement.CatalogMicroservice.Dtos.FinalReportDtos;

namespace RestaurantManagement.CatalogMicroservice.Services.FinalReportService
{
    public interface IFinalReportService
    {
        Task<List<ResultFinalReportDto>> GetAllFinalReports();
        Task<GetByIdFinalReportDto> GetByIdFinalReport(string id);
        Task DeleteFinalReport(string id);
        Task CreateFinalReport(CreateFinalReportDto dto);
        Task<List<ResultFinalReportDto>> GetFinalReportsByShiftAsync(string shift);
    }
}
