using System.Text.Json;
using FluentAssertions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Models;

public class FeedTests {
    private readonly JsonSerializerOptions _jsonOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public void Should_Serialize_Feed_To_Json() {
        // Arrange
        var feed = TestDataFactory.CreateFeed();

        // Act
        var json = JsonSerializer.Serialize(feed, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<Feed>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(feed.Id);
        deserialized.FeedUrl.Should().Be(feed.FeedUrl);
        deserialized.SiteUrl.Should().Be(feed.SiteUrl);
        deserialized.Title.Should().Be(feed.Title);
        deserialized.CheckedAt.Should().Be(feed.CheckedAt);
        deserialized.Disabled.Should().Be(feed.Disabled);
        deserialized.IgnoreHttpCache.Should().Be(feed.IgnoreHttpCache);
        deserialized.AllowSelfSignedCertificates.Should().Be(feed.AllowSelfSignedCertificates);
        deserialized.FetchViaProxy.Should().Be(feed.FetchViaProxy);
        deserialized.Crawler.Should().Be(feed.Crawler);
        deserialized.HideGlobally.Should().Be(feed.HideGlobally);
        deserialized.DisableHttp2.Should().Be(feed.DisableHttp2);
    }

    [Fact]
    public void Should_Deserialize_Feed_From_Json() {
        // Arrange
        var json = """
        {
            "id": 500,
            "user_id": 2,
            "feed_url": "https://news.site.com/rss.xml",
            "site_url": "https://news.site.com",
            "title": "News Site",
            "checked_at": "2024-03-20T15:30:00Z",
            "etag_header": "W/\"abcdef123456\"",
            "last_modified_header": "Wed, 20 Mar 2024 15:30:00 GMT",
            "parsing_error_message": "Connection timeout",
            "parsing_error_count": 3,
            "disabled": true,
            "ignore_http_cache": true,
            "allow_self_signed_certificates": true,
            "fetch_via_proxy": true,
            "scraper_rules": ".article-content",
            "rewrite_rules": "example.com/rss=example.com/full",
            "urlrewrite_rules": "amp=1",
            "blocklist_rules": "ads|tracking",
            "keeplist_rules": "important|breaking",
            "block_filter_entry_rules": "sponsored",
            "keep_filter_entry_rules": "tech|science",
            "crawler": true,
            "user_agent": "Custom Bot 1.0",
            "cookie": "session=abc123",
            "username": "feeduser",
            "password": "feedpass",
            "category": {
                "id": 20,
                "title": "News",
                "user_id": 2,
                "hide_globally": false
            },
            "hide_globally": true,
            "disable_http2": true,
            "proxy_url": "http://proxy.example.com:8080"
        }
        """;

        // Act
        var feed = JsonSerializer.Deserialize<Feed>(json, _jsonOptions);

        // Assert
        feed.Should().NotBeNull();
        feed!.Id.Should().Be(500);
        feed.UserId.Should().Be(2);
        feed.FeedUrl.Should().Be("https://news.site.com/rss.xml");
        feed.SiteUrl.Should().Be("https://news.site.com");
        feed.Title.Should().Be("News Site");
        feed.CheckedAt.Should().Be(new DateTime(2024, 3, 20, 15, 30, 0, DateTimeKind.Utc));
        feed.EtagHeader.Should().Be("W/\"abcdef123456\"");
        feed.LastModifiedHeader.Should().Be("Wed, 20 Mar 2024 15:30:00 GMT");
        feed.ParsingErrorMessage.Should().Be("Connection timeout");
        feed.ParsingErrorCount.Should().Be(3);
        feed.Disabled.Should().BeTrue();
        feed.IgnoreHttpCache.Should().BeTrue();
        feed.AllowSelfSignedCertificates.Should().BeTrue();
        feed.FetchViaProxy.Should().BeTrue();
        feed.ScraperRules.Should().Be(".article-content");
        feed.RewriteRules.Should().Be("example.com/rss=example.com/full");
        feed.UrlRewriteRules.Should().Be("amp=1");
        feed.BlocklistRules.Should().Be("ads|tracking");
        feed.KeeplistRules.Should().Be("important|breaking");
        feed.BlockFilterEntryRules.Should().Be("sponsored");
        feed.KeepFilterEntryRules.Should().Be("tech|science");
        feed.Crawler.Should().BeTrue();
        feed.UserAgent.Should().Be("Custom Bot 1.0");
        feed.Cookie.Should().Be("session=abc123");
        feed.Username.Should().Be("feeduser");
        feed.Password.Should().Be("feedpass");
        feed.Category.Should().NotBeNull();
        feed.Category!.Id.Should().Be(20);
        feed.Category.Title.Should().Be("News");
        feed.HideGlobally.Should().BeTrue();
        feed.DisableHttp2.Should().BeTrue();
        feed.ProxyUrl.Should().Be("http://proxy.example.com:8080");
    }

    [Fact]
    public void FeedCreateRequest_Should_Serialize_Correctly() {
        // Arrange
        var request = new FeedCreateRequest {
            FeedUrl = "https://example.com/feed.rss",
            CategoryId = 15,
            Crawler = true,
            UserAgent = "MyBot/1.0",
            ScraperRules = ".content",
            IgnoreHttpCache = true
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<FeedCreateRequest>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.FeedUrl.Should().Be(request.FeedUrl);
        deserialized.CategoryId.Should().Be(request.CategoryId);
        deserialized.Crawler.Should().Be(request.Crawler);
        deserialized.UserAgent.Should().Be(request.UserAgent);
        deserialized.ScraperRules.Should().Be(request.ScraperRules);
        deserialized.IgnoreHttpCache.Should().Be(request.IgnoreHttpCache);
    }

    [Fact]
    public void FeedUpdateRequest_Should_Handle_Nullable_Properties() {
        // Arrange
        var request = new FeedUpdateRequest {
            Title = "Updated Feed Title",
            CategoryId = 20,
            Disabled = false,
            ScraperRules = null,
            IgnoreHttpCache = true
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);

        // Assert
        json.Should().Contain("\"title\":\"Updated Feed Title\"");
        json.Should().Contain("\"category_id\":20");
        json.Should().Contain("\"disabled\":false");
        json.Should().NotContain("\"scraper_rules\"");
        json.Should().Contain("\"ignore_http_cache\":true");
    }

    [Fact]
    public void FeedIcon_Should_Serialize_And_Deserialize_Correctly() {
        // Arrange
        var icon = TestDataFactory.CreateFeedIcon();

        // Act
        var json = JsonSerializer.Serialize(icon, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<FeedIcon>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(icon.Id);
        deserialized.MimeType.Should().Be(icon.MimeType);
        deserialized.Data.Should().Be(icon.Data);
    }

    [Fact]
    public void FeedCounters_Should_Serialize_And_Deserialize_Correctly() {
        // Arrange
        var counters = TestDataFactory.CreateFeedCounters();

        // Act
        var json = JsonSerializer.Serialize(counters, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<FeedCounters>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.ReadCounters.Should().BeEquivalentTo(counters.ReadCounters);
        deserialized.UnreadCounters.Should().BeEquivalentTo(counters.UnreadCounters);
    }
}
