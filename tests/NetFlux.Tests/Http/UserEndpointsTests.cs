using System.Net;
using System.Text;
using FluentAssertions;
using Oire.NetFlux.Http;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Http;

public class UserEndpointsTests: IDisposable {
    private const string BaseUrl = "https://miniflux.example.com";
    private const string ApiKey = "test-api-key";

    private readonly HttpMessageHandlerMock _httpMock;
    private readonly HttpClient _httpClient;
    private readonly MinifluxHttpClient _client;

    public UserEndpointsTests() {
        _httpMock = new HttpMessageHandlerMock();
        _httpClient = new HttpClient(_httpMock) { BaseAddress = new Uri(BaseUrl) };
        _client = new MinifluxHttpClient(BaseUrl, apiKey: ApiKey, httpClient: _httpClient);
    }

    public void Dispose() {
        _client?.Dispose();
        _httpClient?.Dispose();
        _httpMock?.Dispose();
    }

    [Fact]
    public async Task GetCurrentUserAsync_Should_Return_User() {
        // Arrange
        var expectedUser = TestDataFactory.CreateUser();
        _httpMock.SetupJsonResponse(expectedUser, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == "/v1/me");

        // Act
        var user = await _client.GetAsync<User>("/v1/me");

        // Assert
        user.Should().NotBeNull();
        user!.Id.Should().Be(expectedUser.Id);
        user.Username.Should().Be(expectedUser.Username);

        // Verify authentication header
        var request = _httpMock.CapturedRequests.First();
        request.Headers.Should().ContainKey("X-Auth-Token");
        request.Headers.GetValues("X-Auth-Token").Should().ContainSingle(ApiKey);
    }

    [Fact]
    public async Task GetUsersAsync_Should_Return_UserList() {
        // Arrange
        var expectedUsers = new[] { TestDataFactory.CreateUser(1), TestDataFactory.CreateUser(2) };
        _httpMock.SetupJsonResponse(expectedUsers, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == "/v1/users");

        // Act
        var users = await _client.GetAsync<User[]>("/v1/users");

        // Assert
        users.Should().NotBeNull();
        users!.Should().HaveCount(2);
        users[0].Id.Should().Be(expectedUsers[0].Id);
        users[1].Id.Should().Be(expectedUsers[1].Id);
    }

    [Fact]
    public async Task GetUserAsync_Should_Return_SpecificUser() {
        // Arrange
        var userId = 42;
        var expectedUser = TestDataFactory.CreateUser(userId);
        _httpMock.SetupJsonResponse(expectedUser, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Get && req.RequestUri!.AbsolutePath == $"/v1/users/{userId}");

        // Act
        var user = await _client.GetAsync<User>($"/v1/users/{userId}");

        // Assert
        user.Should().NotBeNull();
        user!.Id.Should().Be(userId);
    }

    [Fact]
    public async Task CreateUserAsync_Should_Send_UserData() {
        // Arrange
        var createRequest = new UserCreateRequest {
            Username = "newuser",
            Password = "password123",
            IsAdmin = false
        };
        var expectedUser = TestDataFactory.CreateUser(10, "newuser");

        _httpMock.SetupJsonResponse(expectedUser, HttpStatusCode.Created, req =>
            req.Method == HttpMethod.Post && req.RequestUri!.AbsolutePath == "/v1/users");

        // Act
        var user = await _client.PostAsync<User>("/v1/users", createRequest);

        // Assert
        user.Should().NotBeNull();
        user!.Username.Should().Be("newuser");

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<UserCreateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Username.Should().Be(createRequest.Username);
        capturedRequest.Password.Should().Be(createRequest.Password);
        capturedRequest.IsAdmin.Should().Be(createRequest.IsAdmin);
    }

    [Fact]
    public async Task UpdateUserAsync_Should_Send_UpdateData() {
        // Arrange
        var userId = 10;
        var updateRequest = new UserUpdateRequest {
            Username = "updateduser",
            Theme = "dark",
            Language = "en",
            Timezone = "UTC"
        };
        var expectedUser = TestDataFactory.CreateUser(userId, "updateduser");

        _httpMock.SetupJsonResponse(expectedUser, HttpStatusCode.OK, req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/users/{userId}");

        // Act
        var user = await _client.PutAsync<User>($"/v1/users/{userId}", updateRequest);

        // Assert
        user.Should().NotBeNull();
        user!.Username.Should().Be("updateduser");

        // Verify request content
        var capturedRequest = await _httpMock.GetCapturedRequestBodyAsync<UserUpdateRequest>();
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Username.Should().Be(updateRequest.Username);
        capturedRequest.Theme.Should().Be(updateRequest.Theme);
    }

    [Fact]
    public async Task DeleteUserAsync_Should_Send_DeleteRequest() {
        // Arrange
        var userId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Delete && req.RequestUri!.AbsolutePath == $"/v1/users/{userId}");

        // Act
        await _client.DeleteAsync($"/v1/users/{userId}");

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests.First();
        request.Method.Should().Be(HttpMethod.Delete);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/users/{userId}");
    }

    [Fact]
    public async Task MarkAllAsReadAsync_Should_Send_PutRequest() {
        // Arrange
        var userId = 10;
        _httpMock.SetupResponse(new HttpResponseMessage(HttpStatusCode.NoContent), req =>
            req.Method == HttpMethod.Put && req.RequestUri!.AbsolutePath == $"/v1/users/{userId}/mark-all-as-read");

        // Act
        await _client.PutAsync<object>($"/v1/users/{userId}/mark-all-as-read", null);

        // Assert
        _httpMock.CapturedRequests.Should().HaveCount(1);
        var request = _httpMock.CapturedRequests.First();
        request.Method.Should().Be(HttpMethod.Put);
        request.RequestUri!.AbsolutePath.Should().Be($"/v1/users/{userId}/mark-all-as-read");
    }

    [Fact]
    public async Task Authentication_Should_Use_ApiKey_Header() {
        // Arrange
        _httpMock.SetupJsonResponse(TestDataFactory.CreateUser(), HttpStatusCode.OK);

        // Act
        _ = await _client.GetAsync<User>("/v1/me");

        // Assert
        var request = _httpMock.CapturedRequests.First();
        request.Headers.Should().ContainKey("X-Auth-Token");
        request.Headers.GetValues("X-Auth-Token").Should().ContainSingle(ApiKey);
        request.Headers.Should().ContainKey("User-Agent");
        request.Headers.Accept.Should().Contain(h => h.MediaType == "application/json");
    }

    [Fact]
    public async Task BasicAuth_Should_Use_Authorization_Header() {
        // Arrange
        const string username = "testuser";
        const string password = "testpass";
        var authClient = new MinifluxHttpClient(BaseUrl, username: username, password: password, httpClient: _httpClient);

        _httpMock.SetupJsonResponse(TestDataFactory.CreateUser(), HttpStatusCode.OK);

        // Act
        _ = await authClient.GetAsync<User>("/v1/me");

        // Assert
        var request = _httpMock.CapturedRequests.First();
        request.Headers.Should().ContainKey("Authorization");
        var authHeader = request.Headers.GetValues("Authorization").First();
        authHeader.Should().StartWith("Basic ");

        // Verify base64 encoding
        var expectedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        authHeader.Should().Be($"Basic {expectedAuth}");

        authClient.Dispose();
    }
}
