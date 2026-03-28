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
        public AuthService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<bool> Login(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7260/api/Account/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }
    }
}