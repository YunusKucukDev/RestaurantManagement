using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Services.IncomeService;

[Route("api/[controller]")]
[ApiController]
public class IncomesController : ControllerBase
{
    private readonly IIncomeService _incomeService;
    public IncomesController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    // Vardiya bilgisine göre o vardiyadaki tüm gelirleri (ciroyu) getirme
    [HttpGet("GetIncomesByShift/{shift}")]
    public async Task<IActionResult> GetIncomesByShift(string shift)
    {
        var results = await _incomeService.GetIncomesByShiftAsync(shift);
        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIncomes()
    {
        var results = await _incomeService.GetAllIncomes();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdIncomeDto(string id)
    {
        // Not: Servis metodundaki yazım hatasını (GetByIdAbotDto) düzelttiğini varsayıyorum
        var result = await _incomeService.GetByIdAbotDto(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIncomeDto([FromBody] CreateInComeDto createIncomeDto)
    {
        // Kayıt esnasında DTO içindeki ShiftType veritabanına aktarılır
        await _incomeService.CreateIncomeDto(createIncomeDto);
        return Ok("Başarılı");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateIncomeDto(UpdateInComeDtos updateIncomeDto)
    {
        await _incomeService.UpdateIncomeDto(updateIncomeDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncomeDto(string id)
    {
        await _incomeService.DeleteIncomeDto(id);
        return NoContent();
    }
}