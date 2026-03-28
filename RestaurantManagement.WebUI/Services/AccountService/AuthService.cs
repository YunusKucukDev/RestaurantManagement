using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaaurantManagement.DtoLayer.Dtos.UserDto;

namespace RestaurantManagement.WebUI.Services.AccountService
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(LoginDTO loginDto)
        {
            // Baþýna https... yazmana gerek kalmadý, BaseAddress otomatik eklenir.
            // Dikkat: appsettings'deki URL "api/" ile bitiyorsa burasý "Account/login" olmalý.
            var response = await _httpClient.PostAsJsonAsync("Account/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Ýpucu: Token'ý burada Session veya Cookie'ye kaydedebilirsin.
                return true;
            }
            return false;
        }
    }
}