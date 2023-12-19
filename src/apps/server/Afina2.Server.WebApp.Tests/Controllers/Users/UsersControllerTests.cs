using System.Net;
using System.Net.Http.Json;
using Afina2.Server.WebApp.Controllers.Users;
using Afina2.Server.WebApp.Tests.Controllers.Users.TestData;
using Afina2.Server.WebApp.Tests.TestInfrastructure.Web;
using Xunit;

namespace Afina2.Server.WebApp.Tests.Controllers.Users;
public class UsersControllerTests : ControllerTestsBase
{
    [Theory]
    [ClassData(typeof(AddUserRequest_BadRequests))]
    public async void AddUser_ReturnsBadRequestResult(AddUserRequest request)
    {
        var response = await _client.PostAsJsonAsync("/api/users", request);

        Assert.True(HttpStatusCode.BadRequest == response.StatusCode, $"Expected status code {HttpStatusCode.BadRequest} but got {response.StatusCode}");
    }

    [Fact]
    public async void AddUser_ReturnsCreatedStatusCode()
    {
        var response = await _client.PostAsJsonAsync("/api/users", new AddUserRequest
        {
            Username = "test"
        });

        Assert.True(HttpStatusCode.Created == response.StatusCode, $"Expected status code {HttpStatusCode.Created} but got {response.StatusCode}");
    }

    [Fact]
    public async void AddUser_ReturnsCreatedResult()
    {
        var newUser = await CreateUser("test");

        if (newUser?.Resource == null)
            throw new Exception("Created user not found");

        // Test that the created user can be retrieved
        var getUserResponse = await _client.GetAsync(newUser.Location);

        Assert.Equal(HttpStatusCode.OK, getUserResponse.StatusCode);

        var createdUser = await getUserResponse.Content.ReadFromJsonAsync<GetUserResponse>();

        Assert.True(newUser.Resource.Username == createdUser?.Username, $"Created username ({createdUser?.Username}) does not match expected username ({newUser.Resource.Username}))");
    }

    [Fact]
    public async void GetUser_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/users/1");

        Assert.True(HttpStatusCode.OK == response.StatusCode, $"Expected status code {HttpStatusCode.OK} but got {response.StatusCode}");
    }

    [Fact]
    public async void DeleteUser_ReturnsNoContentResult()
    {
        var response = await _client.DeleteAsync("/api/users/1");

        Assert.True(HttpStatusCode.NoContent == response.StatusCode, $"Expected status code {HttpStatusCode.NoContent} but got {response.StatusCode}");
    }

    private async Task<CreatedResource<AddUserResponse>> CreateUser(string username)
    {
        var response = await _client.PostAsJsonAsync("/api/users", new AddUserRequest
        {
            Username = username
        });

        Assert.True(HttpStatusCode.Created == response.StatusCode, $"Expected status code {HttpStatusCode.Created} but got {response.StatusCode}");

        if (response?.Headers?.Location == null)
            throw new Exception("Location header not found");

        return new CreatedResource<AddUserResponse>
        {
            Location = response.Headers.Location.ToString(),
            Resource = response?.Content != null ? await response.Content.ReadFromJsonAsync<AddUserResponse>() : null
        };
    }

    private async Task<GetUserResponse?> GetUser(string userId)
    {
        var response = await _client.GetAsync($"/api/users/{userId}");

        return response.Content.ReadFromJsonAsync<GetUserResponse>().Result;
    }
}
