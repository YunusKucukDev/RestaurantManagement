using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.CatalogMicroservice.Dtos.FinalReportDtos;
using RestaurantManagement.CatalogMicroservice.Services.FinalReportService;
using System.Threading.Tasks;

namespace RestaurantManagement.CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinalReportsController : ControllerBase
    {
        private readonly IFinalReportService _finalReportService;

        public FinalReportsController(IFinalReportService finalReportService)
        {
            _finalReportService = finalReportService;
        }

        // Değişiklik: Vardiya bazlı tüm raporları listeleme
        [HttpGet("GetFinalReportsByShift/{shift}")]
        public async Task<IActionResult> GetFinalReportsByShift(string shift)
        {
            var values = await _finalReportService.GetFinalReportsByShiftAsync(shift);
            return Ok(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFinalReports()
        {
            var rvalues = await _finalReportService.GetAllFinalReports();
            return Ok(rvalues);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinalReport(CreateFinalReportDto dto)
        {
            // ÖNEMLİ: UI'dan gelen dto.ShiftType ("Gunduz"/"Gece") burada kaydedilecek
            await _finalReportService.CreateFinalReport(dto);
            return Ok("Başarılı");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdFinalReport(string id)
        {
            var values = await _finalReportService.GetByIdFinalReport(id);
            return Ok(values);
        }

        [HttpDelete("{id}")] // Silme işlemi için ID'yi route'dan almak standarttır
        public async Task<IActionResult> DeleteFinalReport(string id)
        {
            await _finalReportService.DeleteFinalReport(id);
            return Ok("Başarılı");
        }
    }
}