using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Oire.NetFlux;
using Oire.NetFlux.Exceptions;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests;

public class MinifluxClientTests
{
    private readonly Mock<ILogger<MinifluxClient>> _loggerMock;

    public MinifluxClientTests()
    {
        _loggerMock = new Mock<ILogger<MinifluxClient>>();
    }

    [Fact]
    public void Constructor_With_UsernamePassword_Should_Not_Throw()
    {
        // Act
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.Username, TestConstants.Password);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Constructor_With_ApiKey_Should_Not_Throw()
    {
        // Act
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_Throw_On_Invalid_Endpoint(string? endpoint)
    {
        // Act
        var act = () => new MinifluxClient(endpoint!, TestConstants.ApiKey);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("endpoint");
    }

    [Fact]
    public void Constructor_Should_Normalize_Endpoint_Url()
    {
        // These should all work the same way
        var endpoints = new[]
        {
            "https://miniflux.example.com",
            "https://miniflux.example.com/",
            "https://miniflux.example.com/v1",
            "https://miniflux.example.com/v1/"
        };

        foreach (var endpoint in endpoints)
        {
            // Act & Assert
            var act = () => new MinifluxClient(endpoint, TestConstants.ApiKey);
            act.Should().NotThrow($"endpoint '{endpoint}' should be valid");
        }
    }

    [Fact]
    public void Constructor_Should_Accept_Custom_Timeout()
    {
        // Act
        var timeout = TimeSpan.FromMinutes(5);
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey, timeout: timeout);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Constructor_Should_Accept_Logger()
    {
        // Act
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey, logger: _loggerMock.Object);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Should_Implement_IDisposable()
    {
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
    public void Constructor_Should_Create_Client_With_Minimal_Setup()
    {
        // Arrange & Act
        using var client = new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Assert
        client.Should().NotBeNull();
    }

    [Fact]
    public void Should_Dispose_Without_Throwing()
    {
        // Arrange
        var client = new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Act & Assert
        var act = () => client.Dispose();
        act.Should().NotThrow();
        
        // Multiple disposals should not throw
        act.Should().NotThrow();
    }
}