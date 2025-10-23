using System.Text.Json;
using FluentAssertions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Models;

public class MiscellaneousModelsTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public void ApiKey_Should_Serialize_And_Deserialize_Correctly()
    {
        // Arrange
        var apiKey = TestDataFactory.CreateApiKey();

        // Act
        var json = JsonSerializer.Serialize(apiKey, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<ApiKey>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(apiKey.Id);
        deserialized.UserId.Should().Be(apiKey.UserId);
        deserialized.Token.Should().Be(apiKey.Token);
        deserialized.Description.Should().Be(apiKey.Description);
        deserialized.LastUsedAt.Should().Be(apiKey.LastUsedAt);
        deserialized.CreatedAt.Should().Be(apiKey.CreatedAt);
    }

    [Fact]
    public void ApiKeyCreateRequest_Should_Serialize_Correctly()
    {
        // Arrange
        var request = new ApiKeyCreateRequest
        {
            Description = "Mobile App API Key"
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<ApiKeyCreateRequest>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Description.Should().Be(request.Description);
    }

    [Fact]
    public void VersionInfo_Should_Deserialize_Correctly()
    {
        // Arrange
        var json = """
        {
            "version": "2.0.50",
            "commit": "abc123def456",
            "build_date": "2024-01-15T10:00:00Z",
            "go_version": "go1.21.6",
            "compiler": "gc",
            "arch": "arm64",
            "os": "darwin"
        }
        """;

        // Act
        var version = JsonSerializer.Deserialize<VersionInfo>(json, _jsonOptions);

        // Assert
        version.Should().NotBeNull();
        version!.Version.Should().Be("2.0.50");
        version.Commit.Should().Be("abc123def456");
        version.BuildDate.Should().Be("2024-01-15T10:00:00Z");
        version.GoVersion.Should().Be("go1.21.6");
        version.Compiler.Should().Be("gc");
        version.Arch.Should().Be("arm64");
        version.Os.Should().Be("darwin");
    }

    [Fact]
    public void Subscription_Should_Serialize_And_Deserialize_Correctly()
    {
        // Arrange
        var subscription = TestDataFactory.CreateSubscription();

        // Act
        var json = JsonSerializer.Serialize(subscription, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<Subscription>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Title.Should().Be(subscription.Title);
        deserialized.Url.Should().Be(subscription.Url);
        deserialized.Type.Should().Be(subscription.Type);
    }

    [Fact]
    public void Should_Format_Subscription_ToString_Correctly()
    {
        // Arrange
        var subscription = new Subscription
        {
            Title = "Tech News Feed",
            Url = "https://technews.com/feed.xml",
            Type = "rss"
        };

        // Act
        var result = subscription.ToString();

        // Assert
        result.Should().Be("Title=\"Tech News Feed\", URL=\"https://technews.com/feed.xml\", Type=\"rss\"");
    }

    [Fact]
    public void Enclosure_Should_Serialize_And_Deserialize_Correctly()
    {
        // Arrange
        var enclosure = TestDataFactory.CreateEnclosure();

        // Act
        var json = JsonSerializer.Serialize(enclosure, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<Enclosure>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(enclosure.Id);
        deserialized.UserId.Should().Be(enclosure.UserId);
        deserialized.EntryId.Should().Be(enclosure.EntryId);
        deserialized.Url.Should().Be(enclosure.Url);
        deserialized.MimeType.Should().Be(enclosure.MimeType);
        deserialized.Size.Should().Be(enclosure.Size);
        deserialized.MediaProgression.Should().Be(enclosure.MediaProgression);
    }

    [Fact]
    public void Enclosure_Should_Deserialize_From_Api_Response()
    {
        // Arrange
        var json = """
        {
            "id": 1500,
            "user_id": 3,
            "entry_id": 2000,
            "url": "https://podcast.example.com/episode50.mp3",
            "mime_type": "audio/mpeg",
            "size": 52428800,
            "media_progression": 1800
        }
        """;

        // Act
        var enclosure = JsonSerializer.Deserialize<Enclosure>(json, _jsonOptions);

        // Assert
        enclosure.Should().NotBeNull();
        enclosure!.Id.Should().Be(1500);
        enclosure.UserId.Should().Be(3);
        enclosure.EntryId.Should().Be(2000);
        enclosure.Url.Should().Be("https://podcast.example.com/episode50.mp3");
        enclosure.MimeType.Should().Be("audio/mpeg");
        enclosure.Size.Should().Be(52428800);
        enclosure.MediaProgression.Should().Be(1800);
    }

    [Fact]
    public void Multiple_Subscriptions_Should_Deserialize_As_List()
    {
        // Arrange
        var json = """
        [
            {
                "title": "Blog One",
                "url": "https://blog1.com/rss",
                "type": "rss"
            },
            {
                "title": "Blog Two", 
                "url": "https://blog2.com/atom",
                "type": "atom"
            },
            {
                "title": "News Site",
                "url": "https://news.com/feed",
                "type": "json"
            }
        ]
        """;

        // Act
        var subscriptions = JsonSerializer.Deserialize<List<Subscription>>(json, _jsonOptions);

        // Assert
        subscriptions.Should().NotBeNull();
        subscriptions.Should().HaveCount(3);
        subscriptions![0].Title.Should().Be("Blog One");
        subscriptions[0].Type.Should().Be("rss");
        subscriptions[1].Title.Should().Be("Blog Two");
        subscriptions[1].Type.Should().Be("atom");
        subscriptions[2].Title.Should().Be("News Site");
        subscriptions[2].Type.Should().Be("json");
    }
}