using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebUI.Areas.Admin.Controllers
{
    
    public class ShiftController : Controller
    {
        [HttpGet]
        public IActionResult SetShift(string type)
        {
            HttpContext.Session.SetString("ActiveShift", type ?? "Gunduz");
            string referer = Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                return RedirectToAction("Index", "Computation", new { area = "Admin" });
            }
            return Redirect(referer);
        }
    }
}
