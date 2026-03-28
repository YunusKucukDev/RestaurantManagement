using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.IncomeDto;
using Microsoft.AspNetCore.Http;

namespace RestaurantManagement.WebUI.ViewComponents
{
    public class CreateIncome : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Session'dan aktif vardiyayı alıyoruz
            var activeShift = HttpContext.Session.GetString("ActiveShift") ?? "Gunduz";

            // Form modelini oluşturup varsayılan değerleri atıyoruz
            var model = new CreateInComeDto
            {
                Date = DateTime.Now, // Varsayılan olarak bugünü ata
                ShiftType = activeShift  // Aktif vardiyayı otomatik ata
            };

            return View(model);
        }
    }
}