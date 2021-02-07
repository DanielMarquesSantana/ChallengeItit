using Microsoft.AspNetCore.Mvc.Testing;
using ProjectChallenge.ItiDigital.Validation.Entities;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProjectChallenge.ItiDigital.Validation.IntegrTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            httpClient = factory.CreateClient();
        }

        [Theory]
        [InlineData("AbTp9!fok%", true)]
        [InlineData("AbTp9!fok",true)]
        [InlineData("AbTp9!fok@", true)]
        public async Task PostPasswordValidatinSuccess(string password, bool expectedResult)
        {
            //Arrenge
            var request = new PasswordRequest
            {
                Password = password
            };

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("Validation/Password", body);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var passwordResponse = JsonSerializer.Deserialize<PasswordResponse>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //Assert
            Assert.Equal(expectedResult, passwordResponse.IsValid);
        }


        [Theory]
        [InlineData("aa", false)]
        [InlineData("ab", false)]
        [InlineData("AAAbbbCc", false)]
        [InlineData("AbTp9!foo", false)]
        [InlineData("AbTp9!foA", false)]
        [InlineData("AbTp9 fok", false)]
        [InlineData("AbTp9!fok;", false)]
        public async Task PostPasswordValidatinNotSuccess(string password, bool expectedResult)
        {
            //Arrenge
            var request = new PasswordRequest
            {
                Password = password
            };

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("Validation/Password", body);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var passwordResponse = JsonSerializer.Deserialize<PasswordResponse>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //Assert
            Assert.Equal(expectedResult, passwordResponse.IsValid);
        }

        [Theory]
        [InlineData("", "Password cannot empy or null.")]
        public async Task PostPasswordValidatinNotSuccessEmpy(string password, string expectedResult)
        {
            //Arrenge
            var request = new PasswordRequest
            {
                Password = password
            };

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("Validation/Password", body);

            var stringResponse = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(expectedResult, stringResponse);
        }
    }
}
