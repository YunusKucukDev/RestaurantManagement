
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto;
using RestaurantManagement.WebUI.Services.FixedExpense;
[Authorize]
[Route("FixedExpense/")] // Controller seviyesinde route tanımı ekleyelim
public class FixedExpenseController : Controller
{
    private readonly IFixedExpenseService _service;

    public FixedExpenseController(IFixedExpenseService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("Index")] // Route tanımı ekleyelim
    public async Task<IActionResult> Index()
    {
        ViewBag.HideShiftButtons = true;
        var values = await _service.GetAllFixedExpense();
        return View(values);
    }

    [HttpGet]
    [Route("CreateFixedExpense")] // Route tanımı ekleyelim
    public IActionResult CreateFixedExpense()
    {
        ViewBag.HideShiftButtons = true;
        return View();
    }

    [HttpPost]
    [Route("CreateFixedExpense")] // Route tanımı ekleyelim
    public async Task<IActionResult> CreateFixedExpense(CreateFixedExpense dto)
    {
        var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";
        dto.ShiftType = activeShift;

        await _service.CreateFixedExpense(dto);
        return RedirectToAction("Index");
    }

    
    [Route("UpdateFixedExpense/{id}")] // Route tanımı ekleyelim
    public async Task<IActionResult> UpdateFixedExpense(string id)
    {
        ViewBag.HideShiftButtons = true;
        var value = await _service.GetByIdFixedExpense(id);
        return View(value);
    }

    [HttpPost]
    [Route("UpdateFixedExpense")] // Route tanımı ekleyelim
    public async Task<IActionResult> UpdateFixedExpense(UpdateFixedExpense dto)
    {
        var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";
        dto.ShiftType = activeShift;

        await _service.UpdateFixedExpense(dto);
        return RedirectToAction("Index");
    }

    
    [Route("DeleteFixedExpense/{id}")] // Route tanımı ekleyelim
    public async Task<IActionResult> DeleteFixedExpense(string id)
    {
        await _service.DeleteFixedExpense(id);
        return RedirectToAction("Index");
    }
}