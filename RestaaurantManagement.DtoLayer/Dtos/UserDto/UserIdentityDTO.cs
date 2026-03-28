using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaaurantManagement.DtoLayer.Dtos.UserDto
{
    public class UserIdentityDTO
    {
        [JsonPropertyName("token")] // API'den küçük harfle geleni yakala
        public string Token { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}