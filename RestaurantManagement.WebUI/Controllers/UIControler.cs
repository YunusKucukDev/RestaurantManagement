using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.WebUI.Controllers
{
    public class UIControler : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
