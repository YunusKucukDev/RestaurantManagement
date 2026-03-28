using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.WebUI.Services.OutcomeService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Session için gerekli

namespace RestaurantManagement.WebUI.ViewComponents
{
    public class ListOutcome : ViewComponent
    {
        private readonly IOutcomeService _outcomeService;

        public ListOutcome(IOutcomeService outcomeService)
        {
            _outcomeService = outcomeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime selectedDate)
        {
            // 1. Session'dan aktif vardiyayı alıyoruz
            var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";

            // 2. Silme işlemi sonrası doğru tarihe dönmek için ViewBag'e tarihi gönderiyoruz
            ViewBag.SelectedDate = selectedDate.ToString("yyyy-MM-dd");

            // 3. Verileri çekiyoruz
            var values = await _outcomeService.GetAllOutcomes();

            // 4. HEM tarih HEM de vardiyaya göre filtreliyoruz
            // (Veritabanınızdaki kolon isminin 'Shift' olduğunu varsayıyorum)
            var filteredValues = values
                .Where(x => x.Date.Date == selectedDate.Date &&
                            x.ShiftType == activeShift)
                .OrderByDescending(x => x.OutcomeId)
                .ToList();

            return View(filteredValues);
        }
    }
}