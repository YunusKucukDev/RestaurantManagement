using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.CatalogMicroservice.Dtos.DailyReport;
using RestaurantManagement.CatalogMicroservice.Services.DailyReportService;
using System.Threading.Tasks;

namespace RestaurantManagement.CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyReportsController : ControllerBase
    {
        private readonly IDailyReportService _dailyReportService;

        public DailyReportsController(IDailyReportService dailyReportService)
        {
            _dailyReportService = dailyReportService;
        }

        // Değişiklik: Artık dışarıdan shift parametresi alıyoruz
        [HttpGet("GetAllByShift/{shift}")]
        public async Task<ActionResult> GetAllDailyReportsByShift(string shift)
        {
            // Servis katmanında bu filtrelemeyi yapacak metodu çağırmalıyız
            var values = await _dailyReportService.GetDailyReportsByShiftAsync(shift);
            return Ok(values);
        }

        // Mevcut metodu koruyup tümünü görmek istersen:
        [HttpGet]
        public async Task<ActionResult> GetAllDailyReports()
        {
            var values = await _dailyReportService.GetAllDailyReports();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDailyReport(CreateDailyReportDto dto)
        {
            // DTO içinde ShiftType alanı olmalı ki veritabanına "Gunduz" veya "Gece" yazılabilsin
            await _dailyReportService.CreateDailyReport(dto);
            return Ok("Başarılı");
        }

        [HttpDelete("{id}")] // Silme işleminde ID'yi URL'den almak daha standarttır
        public async Task<IActionResult> DeleteDailyReport(string id)
        {
            await _dailyReportService.DeleteDailyReport(id);
            return Ok("Başarılı bir şekilde silindi");
        }
    }
}