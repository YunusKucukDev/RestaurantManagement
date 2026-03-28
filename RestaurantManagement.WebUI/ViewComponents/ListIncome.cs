using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.WebUI.Services.IncomeService;
using System.Threading.Tasks;

namespace RestaurantManagement.WebUI.ViewComponents
{
    public class ListIncomeViewComponent : ViewComponent
    {
        private readonly IIncomeService _incomeService;
        public ListIncomeViewComponent(IIncomeService incomeService) { _incomeService = incomeService; }

        public async Task<IViewComponentResult> InvokeAsync(DateTime? selectedDate)
        {
            // 1. Parametre null gelirse bugünü al (Hata önleyici)
            var dateFilter = selectedDate ?? DateTime.Today;

            // 2. Aktif vardiyayı al (Filtreleme için şart)
            var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";

            var values = await _incomeService.GetAllIncomes();

            // 3. Tarih ve Vardiya (ShiftType) kontrolü
            // Not: Veritabanındaki kolon adınız ShiftType ise ona göre güncelleyin
            var filteredValues = values.Where(x =>
                x.Date.Date == dateFilter.Date &&
                (x.ShiftType == activeShift || x.ShiftType == activeShift)
            ).ToList();

            return View(filteredValues);
        }
    }
}
