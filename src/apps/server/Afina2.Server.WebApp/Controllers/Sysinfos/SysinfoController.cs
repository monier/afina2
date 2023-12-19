using My = Afina2.Server.WebApp.Controllers.Sysinfos;
using Afina2.Server.WebApp.Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Afina2.Server.WebApp.Controllers.Sysinfos;
/// <summary>
/// Provides information about the server.
/// </summary>
public class SysinfoController : ApiControllerBase
{
    private readonly IEnumerable<EndpointDataSource> _endpointSources;

    public SysinfoController(
        IEnumerable<EndpointDataSource> endpointSources
    )
    {
        _endpointSources = endpointSources;
    }

    /// <summary>
    /// Ping the server to check if it is alive.
    /// </summary>
    /// <returns>Always return "Alive!"</returns>
    /// <response code="200">The server is alive.</response>
    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("Alive!");
    }

    /// <summary>
    /// Enumerates all endpoints in the application.
    /// </summary>
    /// <returns>A list of all endpoints in the application.</returns>
    [HttpGet("endpoints")]
    public ActionResult GetAllEndpoints()
    {
        // following code is retrieved from https://stackoverflow.com/questions/28435734/how-to-get-a-list-of-all-routes-in-asp-net-core
        var endpoints = _endpointSources
            .SelectMany(es => es.Endpoints)
            .OfType<RouteEndpoint>();
        var output = endpoints.Select(
            e =>
            {
                var controller = e.Metadata
                    .OfType<ControllerActionDescriptor>()
                    .FirstOrDefault();
                var action = controller != null
                    ? $"{controller.ControllerName}.{controller.ActionName}"
                    : null;
                var controllerMethod = controller != null
                    ? $"{controller.ControllerTypeInfo.FullName}:{controller.MethodInfo.Name}"
                    : null;
                return new My.Endpoint
                {
                    Method = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods?[0],
                    Route = $"/{e.RoutePattern?.RawText?.TrimStart('/')}",
                    Action = action,
                    ControllerMethod = controllerMethod
                };
            }
        );

        return Ok(output);
    }
}
