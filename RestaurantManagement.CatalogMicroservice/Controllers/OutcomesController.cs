using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos;
using RestaurantManagement.CatalogMicroservice.Services.OutComeService;

[Route("api/[controller]")]
[ApiController]
public class OutcomesController : ControllerBase
{
    private readonly IOutcomeService _outcomeService;
    public OutcomesController(IOutcomeService outcomeService)
    {
        _outcomeService = outcomeService;
    }

    // Belirli bir vardiyaya (Gündüz/Gece) ait giderleri listeler
    [HttpGet("GetOutcomesByShift/{shift}")]
    public async Task<IActionResult> GetOutcomesByShift(string shift)
    {
        var results = await _outcomeService.GetOutcomesByShiftAsync(shift);
        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOutcomes()
    {
        var results = await _outcomeService.GetAllOutcomes();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdOutcomeDto(string id)
    {
        var result = await _outcomeService.GetByIdAbotDto(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOutcomeDto(CreateOutcomeDto createOutcomeDto)
    {
        // Gelen DTO içindeki ShiftType bilgisiyle beraber gider kaydedilir
        await _outcomeService.CreateOutcomeDto(createOutcomeDto);
        return Created("", createOutcomeDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOutcomeDto(UpdateOutComeDto updateOutcomeDto)
    {
        await _outcomeService.UpdateOutcomeDto(updateOutcomeDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOutcomeDto(string id)
    {
        await _outcomeService.DeleteOutcomeDto(id);
        return NoContent();
    }
}