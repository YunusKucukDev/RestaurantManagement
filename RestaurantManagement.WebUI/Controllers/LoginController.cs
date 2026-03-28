using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestaaurantManagement.DtoLayer.Dtos.UserDto;
using RestaurantManagement.WebUI.Services.AccountService; // AuthService için
using System.Security.Claims;

namespace RestaurantManagement.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO loginDto)
        {
            try
            {
                // ARTIK MANUEL HTTPCLIENT YERİNE MERKEZİ AUTHSERVICE'İ ÇAĞIRIYORUZ
                // AuthService zaten Program.cs'den doğru URL'i alıyor.
                var loginSuccess = await _authService.Login(loginDto);

                if (loginSuccess)
                {
                    // NOT: AuthService içinde Token'ı Session'a kaydettiğinden emin ol
                    // veya Token'ı buradan Claims içine eklemek istersen AuthService'i 
                    // geriye bool yerine bir obje dönecek şekilde güncelleyebiliriz.

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginDto.UserName)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bağlantı hatası: " + ex.Message);
            }

            return View(loginDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}