using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;
using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;
using RestaurantManagement.WebUI.Services.DailyReportService;
using RestaurantManagement.WebUI.Services.IncomeService;
using RestaurantManagement.WebUI.Services.OutcomeService;

[Authorize]
public class HomeController : Controller
{
    private readonly IIncomeService _incomeService;
    private readonly IOutcomeService _outcomeService;
    private readonly IDailyReportService _dailyReportService;

    public HomeController(IIncomeService incomeService, IOutcomeService outcomeService, IDailyReportService dailyReportService)
    {
        _incomeService = incomeService;
        _outcomeService = outcomeService;
        _dailyReportService = dailyReportService;
    }

    private void PopulateCompanies(string selectedValue = null)
    {
        var companies = new List<string> { "YemekSepeti", "Getir", "Trendyol", "Metropol", "Pluxee", "TokenFlex", "SetCard", "Multinet" };
        ViewBag.Companies = new SelectList(companies, selectedValue);
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateTime? selectedDate)
    {
        PopulateCompanies();
        // 1. Tarihi belirle
        DateTime filterDate = selectedDate?.Date ?? DateTime.Today.Date;
        var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";

        // 2. Tüm verileri çek
        var incomesByShift = await _incomeService.GetIncomesByShiftAsync(activeShift);
        var outcomesByShift = await _outcomeService.GetOutcomesByShiftAsync(activeShift);
        var allDailyReports = await _dailyReportService.GetDailyReportsByShiftAsync(activeShift);

        // 3. FİLTRELEME (Burayı dikkatli güncelleyin)
        // ToLocalTime() ekleyerek MongoDB'den gelen UTC saatini yerel saate çeviriyoruz
        var incomes = incomesByShift
            .Where(x => x.Date.ToLocalTime().Date == filterDate.Date)
            .ToList();

        var outcomes = outcomesByShift
     .Where(x => x.Date.Date == filterDate.Date) // filterDate.Date ile x.Date.Date karşılaştırılıyor
     .ToList();

        // 4. HESAPLAMA
        ViewBag.TotalIncome = incomes.Sum(x => x.IncomeAmount);
        ViewBag.TotalOutcome = outcomes.Sum(x => x.OutcomeAmount);
        ViewBag.FinalIncome = (decimal)ViewBag.TotalIncome - (decimal)ViewBag.TotalOutcome;

        // Diğer ViewBag atamaları
        ViewBag.ActiveShift = activeShift;
        ViewBag.SelectedDate = filterDate.ToString("yyyy-MM-dd");
        ViewBag.RawDate = filterDate;
        ViewBag.IsReported = allDailyReports.Any(x => x.Date.ToLocalTime().Date == filterDate.Date);

        return View();
    }

    [HttpPost]
    [Route("CreateIncome")] // Route tanımı ekleyelim
    public async Task<IActionResult> CreateIncome(CreateInComeDto dto)
    {
        ModelState.Remove("ShiftType");
        dto.Date = dto.Date.Date.AddHours(12);
        dto.ShiftType = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", new { selectedDate = dto.Date.ToString("yyyy-MM-dd") });
        }
        if (!string.IsNullOrEmpty(dto.SelectedCompany))
        {
            decimal brutTutar = dto.IncomeAmount;
            decimal kesintiOrani = (dto.SelectedCompany == "YemekSepeti" ||
                                    dto.SelectedCompany == "Getir" ||
                                    dto.SelectedCompany == "Trendyol") ? 0.38m : 0.10m;

            decimal kesintiTutari = brutTutar * kesintiOrani;
            dto.IncomeAmount = brutTutar - kesintiTutari;
            dto.IncomeDescription = kesintiTutari;
        }
        await _incomeService.CreateIncomeDto(dto);
        return RedirectToAction("Index", new { selectedDate = dto.Date.ToString("yyyy-MM-dd") });
    }


    [HttpPost]
    [Route("CreateOutcome")] // Route tanımı ekleyelim
    public async Task<IActionResult> CreateOutcome(CreateOutcomeDto dto)
    {

        ModelState.Remove("ShiftType");

        // 2. Tarihi UTC çakışmasını önlemek için sabitle
        dto.Date = dto.Date.Date.AddHours(12);
        dto.ShiftType = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", new { selectedDate = dto.Date.ToString("yyyy-MM-dd") });
        }

        await _outcomeService.CreateOutcomeDto(dto);
        return RedirectToAction("Index", new { selectedDate = dto.Date.ToString("yyyy-MM-dd") });
    }

    [HttpGet] 
    [Route("Home/DeleteIncome/{id}")] 
    public async Task<IActionResult> DeleteIncome(string id, DateTime selectedDate)
    {
        await _incomeService.DeleteIncomeDto(id);
        return RedirectToAction("Index", new { selectedDate = selectedDate.ToString("yyyy-MM-dd") });
    }

    [Route("Home/DeleteOutcome/{id}")] // Route tanımı ekleyelim
    [HttpGet]
    public async Task<IActionResult> DeleteOutcome(string id, DateTime selectedDate)
    {
        await _outcomeService.DeleteOutcomeDto(id);
        return RedirectToAction("Index", new { selectedDate = selectedDate.ToString("yyyy-MM-dd") });
    }
}