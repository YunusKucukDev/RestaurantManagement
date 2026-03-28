using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.DailyReport;
using RestaurantManagement.WebUI.Models;
using RestaurantManagement.WebUI.Services.DailyReportService;
using RestaurantManagement.WebUI.Services.FixedExpense;
using RestaurantManagement.WebUI.Services.IncomeService;
using RestaurantManagement.WebUI.Services.OutcomeService;

[Authorize]
[Route("Report/")]
public class ReportController : Controller
{
    private readonly IIncomeService _incomeService;
    private readonly IOutcomeService _outcomeService;
    private readonly IFixedExpenseService _fixedExpenseService;
    private readonly IDailyReportService _dailyReportService;

    public ReportController(IIncomeService incomeService, IOutcomeService outcomeService, IFixedExpenseService fixedExpenseService, IDailyReportService dailyReportService)
    {
        _incomeService = incomeService;
        _outcomeService = outcomeService;
        _fixedExpenseService = fixedExpenseService;
        _dailyReportService = dailyReportService;
    }

    [HttpPost]
    [Route("CloseDay")]
    public async Task<IActionResult> CloseDay(DateTime reportDate)
    {
        
        var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";
        var targetDate = reportDate.Date.AddHours(12);

       
        var allIncomes = await _incomeService.GetIncomesByShiftAsync(activeShift);
        var allOutcomes = await _outcomeService.GetOutcomesByShiftAsync(activeShift);

        var incomes = allIncomes.Where(x => x.Date.Date == reportDate.Date).ToList();
        var outcomes = allOutcomes.Where(x => x.Date.Date == reportDate.Date).ToList();

        var report = new CreateDailyReportDto
        {
            Date = targetDate,
            ShiftType = activeShift, // Raporun hangi vardiyaya ait olduğunu kaydediyoruz
            TotalIncome = incomes.Sum(x => x.IncomeAmount),
            TotalOutcome = outcomes.Sum(x => x.OutcomeAmount),
            NetProfit = incomes.Sum(x => x.IncomeAmount) - outcomes.Sum(x => x.OutcomeAmount)
        };
        await _dailyReportService.CreateDailyReports(report);
        return RedirectToAction("Index", "Report");
    }



    [HttpGet]
    [Route("Index")] 
    public async Task<IActionResult> Index(DateTime? start, DateTime? end)
    {
        ViewBag.HideShiftButtons = true; 

        var allReports = await _dailyReportService.GetAllDailyReports();

        if (start.HasValue && end.HasValue)
        {
            allReports = allReports.Where(x => x.Date.Date >= start.Value.Date &&
                                             x.Date.Date <= end.Value.Date).ToList();
            ViewBag.StartDate = start.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = end.Value.ToString("yyyy-MM-dd");
        }
        else
        {
            ViewBag.StartDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        return View(allReports.OrderByDescending(x => x.Date).ToList());
    }

    [HttpGet]
    [Route("SelectFixedExpenses")] 
    public async Task<IActionResult> SelectFixedExpenses(DateTime startDate, DateTime endDate, string ShiftType)
    {
        ViewBag.HideShiftButtons = true; 
        string targetShift = !string.IsNullOrEmpty(ShiftType) ? ShiftType : "Gunduz";


        var allIncomes = await _incomeService.GetIncomesByShiftAsync(targetShift);
        var allOutcomes = await _outcomeService.GetOutcomesByShiftAsync(targetShift);


        var filteredIncomes = allIncomes
            .Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date)
            .OrderBy(x => x.Date)
            .ToList();

        var filteredOutcomes = allOutcomes
            .Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date)
            .OrderBy(x => x.Date)
            .ToList();

        var viewModel = new FixedExpensesDetailViewModel
        {
            Incomes = filteredIncomes,
            Outcomes = filteredOutcomes,
            StartDate = startDate,
            EndDate = endDate,
            ShiftType = targetShift
        };

        ViewBag.VardiyaBilgisi = targetShift; 
        return View(viewModel);
    }

    [HttpGet]
    [Route("PeriodicalAnalysis")] 
    public async Task<IActionResult> PeriodicalAnalysis(DateTime? start, DateTime? end)
    {
        ViewBag.HideShiftButtons = true;


        DateTime startDate = (start ?? DateTime.Now.AddMonths(-1)).Date;
        DateTime endDate = (end ?? DateTime.Now).Date.AddDays(1).AddTicks(-1);

        var incomes = await _incomeService.GetAllIncomes();
        var outcomes = await _outcomeService.GetAllOutcomes();
        var fixedExpenses = await _fixedExpenseService.GetAllFixedExpense();

        
        var totalInc = incomes
            .Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date)
            .Sum(x => x.IncomeAmount);

        var totalOut = outcomes
            .Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date)
            .Sum(x => x.OutcomeAmount);

        var viewModel = new PeriodicalAnalysisViewModel
        {
            StartDate = startDate,
            EndDate = endDate,
            IncomeDetails = incomes,
            OutcomeDetails = outcomes,
            PeriodTotalIncome = totalInc,
            PeriodTotalOutcome = totalOut,
            AllFixedExpenses = fixedExpenses
        };
        return View(viewModel);
    }

    [HttpGet] 
    [Route("DeleteDailyReport/{id}")]
    public async Task<IActionResult> DeleteDailyReport(string id)
    {
        await _dailyReportService.DeleteDailyReports(id);
        return RedirectToAction("Index");
    }
}