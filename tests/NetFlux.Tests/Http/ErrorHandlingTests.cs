using System.Net;
using FluentAssertions;
using Oire.NetFlux.Exceptions;
using Oire.NetFlux.Http;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Http;

public class ErrorHandlingTests: IDisposable {
    private const string BaseUrl = "https://miniflux.example.com";
    private const string ApiKey = "test-api-key";

    private readonly HttpMessageHandlerMock _httpMock;
    private readonly HttpClient _httpClient;
    private readonly MinifluxHttpClient _client;

    public ErrorHandlingTests() {
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
    public async Task Unauthorized_Should_Throw_MinifluxAuthenticationException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxAuthenticationException>(
            () => _client.GetAsync<User>("/v1/me"));

        exception.Message.Should().Be("Unauthorized");
    }

    [Fact]
    public async Task Forbidden_Should_Throw_MinifluxForbiddenException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.Forbidden, "Forbidden");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxForbiddenException>(
            () => _client.GetAsync<User>("/v1/users/999"));

        exception.Message.Should().Be("Forbidden");
    }

    [Fact]
    public async Task NotFound_Should_Throw_MinifluxNotFoundException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.NotFound, "Not Found");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxNotFoundException>(
            () => _client.GetAsync<User>("/v1/users/999"));

        exception.Message.Should().Be("Not found");
    }

    [Fact]
    public async Task BadRequest_Should_Throw_MinifluxBadRequestException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.BadRequest, "Bad Request");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxBadRequestException>(
            () => _client.PostAsync<User>("/v1/users", new { invalidData = true }));

        exception.Message.Should().Be("Bad request");
    }

    [Fact]
    public async Task InternalServerError_Should_Throw_MinifluxServerException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxServerException>(
            () => _client.GetAsync<User>("/v1/me"));

        exception.Message.Should().Be("Internal server error");
    }

    [Fact]
    public async Task BadGateway_Should_Throw_MinifluxException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.BadGateway, "Bad Gateway");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxException>(
            () => _client.GetAsync<User>("/v1/me"));

        exception.Message.Should().StartWith("Request failed with status BadGateway:");
    }

    [Fact]
    public async Task ServiceUnavailable_Should_Throw_MinifluxException() {
        // Arrange
        _httpMock.SetupErrorResponse(HttpStatusCode.ServiceUnavailable, "Service Unavailable");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<MinifluxException>(
            () => _client.GetAsync<User>("/v1/me"));

        exception.Message.Should().StartWith("Request failed with status ServiceUnavailable:");
    }

    [Fact]
    public async Task NetworkError_Should_Throw_HttpRequestException() {
        // Arrange
        _httpMock.Dispose(); // Dispose to simulate network error
        var isolatedClient = new MinifluxHttpClient(BaseUrl, apiKey: ApiKey);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => isolatedClient.GetAsync<User>("/v1/me"));

        isolatedClient.Dispose();
    }
}
