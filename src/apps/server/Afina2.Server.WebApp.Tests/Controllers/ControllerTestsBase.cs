namespace Afina2.Server.WebApp.Tests.Controllers;

public abstract class ControllerTestsBase : IDisposable
{
    protected readonly ServerWebApp _application;
    protected readonly HttpClient _client;

    public ControllerTestsBase()
    {
        _application = new ServerWebApp();
        _client = _application.CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
        _application.Dispose();
    }
}
