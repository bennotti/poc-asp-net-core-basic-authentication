using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using SampleProject.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleProjectxUnitTest
{
    public class BasicAuthenticationControllerTest
    {
        private readonly HttpClient _client;
        public BasicAuthenticationControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<SampleProject.Api.Startup>());
            _client = server.CreateClient();
        }
        [Fact(DisplayName = "should be pass and Ok status code")]
        [Trait("BasicAuthenticationController", "Defined")]
        public async Task ServiceTest_with_service_defined()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/BasicAuthentication/");

            // Act
            var response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            var dataResponse = JsonConvert.DeserializeObject<IEnumerable<string>>(content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(dataResponse.Any());
        }
    }
}
