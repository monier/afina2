using Afina2.Server.WebApp.Controllers.Sysinfos;
using System.Net;
using System.Net.Http.Json;

namespace Afina2.Server.WebApp.Tests.Controllers.Sysinfos;

public class SysinfoControllerTests : ControllerTestsBase
{
    [Fact]
    public async void Ping_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/sysinfo/ping");

        Assert.True(HttpStatusCode.OK == response.StatusCode, $"Expected status code {HttpStatusCode.OK} but got {response.StatusCode}");
    }

    [Fact]
    public async void GetAllEndpoints_EnumeratesAllExistingEndpoints()
    {
        var response = await _client.GetFromJsonAsync<GetEndpointsResponse>("/api/sysinfo/endpoints");

        Assert.NotNull(response?.Items);
        Assert.True(response?.Items?.Count() > 0, "The endpoint list is empty");
        Assert.True(response?.Items?.FirstOrDefault(e => e.Route?.ToLower() == "/api/sysinfo/ping") != null, "Sysinfo/Ping endpoint is not found");
    }
}
