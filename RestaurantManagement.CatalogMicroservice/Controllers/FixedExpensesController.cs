using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.CatalogMicroservice.Dtos.FixedExpenseDto;
using RestaurantManagement.CatalogMicroservice.Services.FixedExpenseService;

[Route("api/[controller]")]
[ApiController]
public class FixedExpensesController : ControllerBase
{
    private readonly IFixedExpenseService _fixedExpenseService;

    public FixedExpensesController(IFixedExpenseService fixedExpenseService)
    {
        _fixedExpenseService = fixedExpenseService;
    }

    
    [HttpGet("GetFixedExpensesByShift/{shift}")]
    public async Task<ActionResult> GetFixedExpensesByShift(string shift)
    {
        var values = await _fixedExpenseService.GetFixedExpensesByShiftAsync(shift);
        return Ok(values);
    }

    [HttpGet]
    [Route("GetAllFixedExpense")] // Route tanımı ekleyelim
    public async Task<ActionResult> GetAllFixedExpense()
    {
        var values = await _fixedExpenseService.GetAllFixedExpenseDto();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdFixedExpense(string id)
    {
        var values = await _fixedExpenseService.GetByIdFixedExpenseDto(id);
        return Ok(values);
    }

    [HttpPost]
    [Route("CreateFixedExpense")] // Route tanımı ekleyelim
    public async Task<IActionResult> CreateFixedExpense(CreateFixedExpenseDto dto)
    {
        // UI'dan gelen 'ShiftType' verisi burada veritabanına kaydedilir
        await _fixedExpenseService.CreateFixedExpenseDto(dto);
        return Ok("başarılı");
    }

    [HttpPut]
    [Route("UpdateFixedExpense")] // Route tanımı ekleyelim
    public async Task<IActionResult> UpdateFixedExpense(UpdateFixedExpensedto dto)
    {
        await _fixedExpenseService.UpdateFixedExpenseDto(dto);
        return Ok("başarılı");
    }

    [HttpDelete("{id}")] // Silme işlemi için ID parametresi route üzerinden alınmalı
    public async Task<IActionResult> DeleteFixedExpense(string id)
    {
        await _fixedExpenseService.DeleteFixedExpenseDto(id);
        return Ok("başarılı");
    }
}