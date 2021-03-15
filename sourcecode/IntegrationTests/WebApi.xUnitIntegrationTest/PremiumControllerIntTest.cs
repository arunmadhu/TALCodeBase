using Newtonsoft.Json;
using PremiumDomain.Model;
using PremiumFinder.ApiServices;
using PremiumFinder.ApiServices.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.xUnitIntegrationTest
{
    public class PremiumControllerIntTest :  IClassFixture<CustomWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PremiumControllerIntTest(CustomWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task OccupationsApiTest()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/premium/occupations");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var occupations = JsonConvert.DeserializeObject<IEnumerable<Occupation>>(stringResponse);

            Assert.Contains(occupations, p => p.OccupationName == "Cleaner");
            Assert.Contains(occupations, p => p.OccupationName == "Doctor");
        }

        [Fact]
        public async Task GetPremiumApiTest()
        {
            var request = new
            {
                Url = "/api/premium",
                Body = new
                {
                    UserName = "Integration Tester",
                    OccupationId = 1,
                    DateOfBirth = DateTime.Now.AddYears(-20),
                    DeathSumInsured = 100000
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"); ;

            var httpResponse = await _client.PostAsync(request.Url, content);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var premiumInfo = JsonConvert.DeserializeObject<PremiumResponse>(stringResponse);

            // Assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.Equal(20,premiumInfo.Age);
        }

        [Fact]
        public async Task GetPremiumApiValidationTest()
        {
            var request = new
            {
                Url = "/api/premium",
                Body = new
                {
                    UserName = "Integration Tester",
                    OccupationId = 1,
                    DateOfBirth = DateTime.Now.AddYears(1),
                    DeathSumInsured = 100000
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"); ;

            var httpResponse = await _client.PostAsync(request.Url, content);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }
    }
}
