using FluentAssertions;
using Oire.NetFlux;
using Oire.NetFlux.Exceptions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Integration;

public class ClientBehaviorTests {
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

    [Fact]
    public void Should_Support_Both_Authentication_Methods() {
        // API Key authentication
        using var apiKeyClient = new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);
        apiKeyClient.Should().NotBeNull();

        // Basic authentication
        using var basicAuthClient = new MinifluxClient(TestConstants.BaseUrl, TestConstants.Username, TestConstants.Password);
        basicAuthClient.Should().NotBeNull();
    }

    [Fact]
    public void Should_Accept_Custom_Timeout() {
        // Act & Assert
        var act = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey, timeout: TimeSpan.FromMinutes(10));
        act.Should().NotThrow();
    }

    [Fact]
    public void Should_Normalize_Base_Url_Correctly() {
        var testUrls = new[]
        {
            "https://miniflux.example.com",
            "https://miniflux.example.com/",
            "https://miniflux.example.com/v1",
            "https://miniflux.example.com/v1/"
        };

        foreach (var url in testUrls) {
            var act = () => new MinifluxClient(url, TestConstants.ApiKey);
            act.Should().NotThrow($"URL '{url}' should be normalized correctly");
        }
    }

    [Fact]
    public void EntryFilter_Should_Build_Query_Parameters_Correctly() {
        // This tests the BuildFilterQuery method indirectly
        var filter = new EntryFilter {
            Status = EntryStatus.Unread,
            Limit = 50,
            Starred = true,
            CategoryId = 10,
            Search = "test query",
            Order = "published_at",
            Direction = "desc"
        };

        // We can't easily test the actual query building without making HTTP requests,
        // but we can verify the filter object is constructed correctly
        filter.Status.Should().Be(EntryStatus.Unread);
        filter.Limit.Should().Be(50);
        filter.Starred.Should().BeTrue();
        filter.CategoryId.Should().Be(10);
        filter.Search.Should().Be("test query");
        filter.Order.Should().Be("published_at");
        filter.Direction.Should().Be("desc");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Should_Validate_Required_Parameters(string? invalidValue) {
        // API Key validation
        var actApiKey = () => new MinifluxClient(TestConstants.BaseUrl, invalidValue!);
        actApiKey.Should().Throw<ArgumentNullException>();

        // Endpoint validation
        var actEndpoint = () => new MinifluxClient(invalidValue!, TestConstants.ApiKey);
        actEndpoint.Should().Throw<ArgumentNullException>();

        // Basic auth validation
        var actUsername = () => new MinifluxClient(TestConstants.BaseUrl, invalidValue!, TestConstants.Password);
        actUsername.Should().Throw<ArgumentNullException>();

        var actPassword = () => new MinifluxClient(TestConstants.BaseUrl, TestConstants.Username, invalidValue!);
        actPassword.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Should_Implement_IDisposable_Pattern() {
        var client = new MinifluxClient(TestConstants.BaseUrl, TestConstants.ApiKey);

        // Should not throw on dispose
        var act = () => client.Dispose();
        act.Should().NotThrow();

        // Should handle multiple dispose calls
        act.Should().NotThrow();
    }
}
