using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.OutcomeDto;
using RestaurantManagement.WebUI.Services.OutcomeService;
using System.Threading.Tasks;

namespace RestaurantManagement.WebUI.ViewComponents
{
    public class CreateOutcome : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new CreateOutcomeDto());
        }
    }
}
