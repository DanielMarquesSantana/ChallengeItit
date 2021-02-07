using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectChallenge.ItiDigital.Validation.Entities
{
    public class PasswordRequest
    {
        [JsonPropertyName("password"), Required]
        public string Password { get; set; }
    }
}
