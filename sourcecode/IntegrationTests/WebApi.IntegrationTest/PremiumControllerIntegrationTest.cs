using AutoFixture;
using Newtonsoft.Json;
using PremiumDomain.Model;
using PremiumFinder.ApiServices;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTest
{
    public class PremiumControllerIntegrationTest : IClassFixture<CustomWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PremiumControllerIntegrationTest(CustomWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetPlayers()
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
    }
}