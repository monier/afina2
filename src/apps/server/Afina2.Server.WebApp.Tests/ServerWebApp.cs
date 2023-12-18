using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;

namespace Afina2.Server.WebApp.Tests;

public class ServerWebApp : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            builder.UseEnvironment(Microsoft.Extensions.Hosting.Environments.Development);
        });
        base.ConfigureWebHost(builder);
    }
}
