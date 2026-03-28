using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.FinalReportDtos;
using RestaurantManagement.WebUI.Services.FinalReportService;

[Authorize]
[Route("FinalReport/")]
public class FinalReportController : Controller
{
    private readonly IFinalReportService _finalReportService;

    public FinalReportController(IFinalReportService finalReportService)
    {
        _finalReportService = finalReportService;
    }

    [HttpGet]
    [Route("Index")] // Route tanımı ekleyelim
    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        ViewBag.HideShiftButtons = true;

        // 1. Saatleri tamamen temizle (00:00:00 yap)
        DateTime start = startDate?.Date ?? DateTime.Now.AddMonths(-1).Date;
        DateTime end = endDate?.Date ?? DateTime.Now.Date;

        // 2. Tüm raporları çek
        var values = await _finalReportService.GetAllFinalReports();

        if (values != null)
        {
          
            values = values.Where(x =>
                x.StartDate.ToLocalTime().Date >= start &&
                x.EndDate.ToLocalTime().Date <= end).ToList();
        }

  
        ViewBag.StartDate = start.ToString("yyyy-MM-dd");
        ViewBag.EndDate = end.ToString("yyyy-MM-dd");

        return View(values);
    }

    [HttpPost]
    [Route("SaveReport")] 
    public async Task<IActionResult> SaveReport([FromBody] CreateFinalReportDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Hata: Tarayıcıdan gelen veri null.");
        }

        try
        {
            var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";
            dto.ShiftType = activeShift; 

            dto.ReportName = $"{dto.StartDate:dd/MM/yyyy} - {dto.EndDate:dd/MM/yyyy} Raporu";
            dto.NetTotal = dto.TotalRevenue - dto.TotalExpenses;

            await _finalReportService.CreateFinalReport(dto);

            return Ok(new { success = true, message = "Rapor başarıyla arşivlendi." });
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, $"Sunucu Hatası: {errorMessage}");
        }
    }

    [HttpGet]
    [Route("DeleteReport/{id}")] 
    public async Task<IActionResult> DeleteReport(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            await _finalReportService.DeleteFinalReport(id);
        }
        return RedirectToAction("Index");
    }
}