using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Session işlemleri için şart
using RestaaurantManagement.DtoLayer.Dtos.UserDto;
using System.Security.Claims;
using System.Net.Http.Json; // PostAsJsonAsync ve ReadFromJsonAsync için gerekli

namespace RestaurantManagement.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Eğer kullanıcı zaten giriş yapmışsa direkt ana sayfaya gönder
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO loginDto)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                // 1. API'ye giriş isteği atıyoruz
                var response = await client.PostAsJsonAsync("https://localhost:7110/api/Account/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    // 2. API yanıtını DTO'ya dönüştürüyoruz
                    var user = await response.Content.ReadFromJsonAsync<UserIdentityDTO>();

                    // 3. TOKEN KONTROLÜ: Eğer token null veya boş gelirse içeri sokma!
                    if (user != null && !string.IsNullOrEmpty(user.Token))
                    {
                        string token = user.Token;

                        // Claims (Kimlik Bilgileri) oluşturma
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginDto.UserName),
                            new Claim("Token", token) // Token'ı claim olarak da saklıyoruz
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Cookie ayarları
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true, // Tarayıcı kapatılınca hatırlasın
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) // 1 saatlik oturum
                        };

                        // 4. Tarayıcıda Cookie oturumunu başlat
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        // 5. Token'ı Session'da sakla (Servisler içinden erişmek için)
                        HttpContext.Session.SetString("JWToken", token);

                        return RedirectToAction("Index", "Home");
                    }
                }

                // Buraya gelirse API 401 dönmüştür veya Token boştur
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }
            catch (Exception)
            {
                // API kapalıysa veya bağlantı hatası varsa
                ModelState.AddModelError("", "API bağlantısı kurulamadı. Lütfen API projesinin çalıştığından emin olun.");
            }

            return View(loginDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Cookie'yi temizle
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Session'ı temizle
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}