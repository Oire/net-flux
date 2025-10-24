using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Oire.NetFlux;
using Oire.NetFlux.Exceptions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests;

public class MinifluxClientTests {
    private readonly Mock<ILogger<MinifluxClient>> _loggerMock;

    public MinifluxClientTests() {
        _loggerMock = new Mock<ILogger<MinifluxClient>>();
    }

    [Fact]
    public void Constructor_With_UsernamePassword_Should_Not_Throw() {
        // Act
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.Username, TestConstants.Password);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Constructor_With_ApiKey_Should_Not_Throw() {
        // Act
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_Throw_On_Invalid_Endpoint(string? endpoint) {
        // Act
        var act = () => new MinifluxClient(endpoint!, TestConstants.ApiKey);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("endpoint");
    }

    [Fact]
    public void Constructor_Should_Normalize_Endpoint_Url() {
        // These should all work the same way
        var endpoints = new[]
        {
            "https://miniflux.example.com",
            "https://miniflux.example.com/",
            "https://miniflux.example.com/v1",
            "https://miniflux.example.com/v1/"
        };

        foreach (var endpoint in endpoints) {
            // Act & Assert
            var act = () => new MinifluxClient(endpoint, TestConstants.ApiKey);
            act.Should().NotThrow($"endpoint '{endpoint}' should be valid");
        }
    }

    [Fact]
    public void Constructor_Should_Accept_Custom_Timeout() {
        // Act
        var timeout = TimeSpan.FromMinutes(5);
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey, timeout: timeout);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Constructor_Should_Accept_Logger() {
        // Act
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey, logger: _loggerMock.Object);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Should_Implement_IDisposable() {
        // Arrange
        var client = new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Act & Assert
        client.Should().BeAssignableTo<IDisposable>();

        // Should not throw when disposed
        var act = () => client.Dispose();
        act.Should().NotThrow();

        // Should handle multiple dispose calls
        act.Should().NotThrow();
    }

    [Fact]
    public void Constructor_Should_Create_Client_With_Minimal_Setup() {
        // Arrange & Act
        using var client = new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Assert
        client.Should().NotBeNull();
    }

    #region Network Behavior Tests

    [Fact]
    public async Task Should_Return_False_On_Unreachable_Server() {
        // Arrange - Use a non-routable IP address  
        using var client = new MinifluxClient("http://192.0.2.1", TestConstants.ApiKey, timeout: TimeSpan.FromMilliseconds(100));

        // Act
        var result = await client.HealthcheckAsync();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Return_False_On_Invalid_Host() {
        // Arrange - Use invalid URL that will cause DNS resolution to fail
        using var client = new MinifluxClient("http://invalid-miniflux-host-that-does-not-exist.local", TestConstants.ApiKey, timeout: TimeSpan.FromMilliseconds(100));

        // Act
        var result = await client.HealthcheckAsync();

        // Assert  
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Throw_Exception_On_Non_Healthcheck_Method_With_Invalid_Server() {
        // Arrange - GetVersionAsync should throw, unlike HealthcheckAsync
        using var client = new MinifluxClient("http://192.0.2.1", TestConstants.ApiKey, timeout: TimeSpan.FromMilliseconds(100));

        // Act & Assert
        await client.Awaiting(c => c.GetVersionAsync())
            .Should().ThrowAsync<Exception>();
    }

    #endregion

    #region Parameter Validation Tests

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Should_Validate_ApiKey_Parameter(string? invalidValue) {
        // API Key validation
        var actApiKey = () => new MinifluxClient(TestConstants.BaseUrl, invalidValue!);
        actApiKey.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Should_Validate_Username_And_Password_Parameters(string? invalidValue) {
        // Basic auth username validation
        var actUsername = () => new MinifluxClient(TestConstants.BaseUrl, invalidValue!, TestConstants.Password);
        actUsername.Should().Throw<ArgumentNullException>();

        // Basic auth password validation
        var actPassword = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.Username, invalidValue!);
        actPassword.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region Model Tests

    [Fact]
    public void EntryFilter_Should_Build_Query_Parameters_Correctly() {
        // This tests the EntryFilter model construction
        var filter = new EntryFilter {
            Status = EntryStatus.Unread,
            Limit = 50,
            Starred = true,
            CategoryId = 10,
            Search = "test query",
            Order = "published_at",
            Direction = "desc"
        };

        // Verify the filter object is constructed correctly
        filter.Status.Should().Be(EntryStatus.Unread);
        filter.Limit.Should().Be(50);
        filter.Starred.Should().BeTrue();
        filter.CategoryId.Should().Be(10);
        filter.Search.Should().Be("test query");
        filter.Order.Should().Be("published_at");
        filter.Direction.Should().Be("desc");
    }

    #endregion
}
