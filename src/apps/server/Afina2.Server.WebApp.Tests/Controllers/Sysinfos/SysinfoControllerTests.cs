using Afina2.Server.WebApp.Controllers.Sysinfos;
using Xunit;
using System.Net;
using System.Net.Http.Json;

namespace Afina2.Server.WebApp.Tests.Controllers.Sysinfos;

public class SysinfoControllerTests : ControllerTestsBase
{
    [Fact]
    public async void Ping_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/sysinfo/ping");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void GetAllEndpoints_EnumeratesAllExistingEndpoints()
    {
        var response = await _client.GetFromJsonAsync<Endpoint[]>("/api/sysinfo/endpoints");

        Assert.NotNull(response);
        Assert.True(response?.Length > 0, "The endpoint list is empty");
        Assert.True(response?.FirstOrDefault(e => e.Route?.ToLower() == "/api/sysinfo/ping") != null, "Sysinfo/Ping endpoint is not found");
    }
}
