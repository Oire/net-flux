using System.Text.Json;
using FluentAssertions;
using Oire.NetFlux.Models;
using Oire.NetFlux.Helpers;
using Oire.NetFlux.Tests.Fixtures;

namespace Oire.NetFlux.Tests.Models;

public class EntryTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public void Should_Serialize_Entry_To_Json()
    {
        // Arrange
        var entry = TestDataFactory.CreateEntry();

        // Act
        var json = JsonSerializer.Serialize(entry, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<Entry>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(entry.Id);
        deserialized.PublishedAt.Should().Be(entry.PublishedAt);
        deserialized.ChangedAt.Should().Be(entry.ChangedAt);
        deserialized.CreatedAt.Should().Be(entry.CreatedAt);
        deserialized.Hash.Should().Be(entry.Hash);
        deserialized.Url.Should().Be(entry.Url);
        deserialized.CommentsUrl.Should().Be(entry.CommentsUrl);
        deserialized.Title.Should().Be(entry.Title);
        deserialized.Status.Should().Be(entry.Status);
        deserialized.Content.Should().Be(entry.Content);
        deserialized.Author.Should().Be(entry.Author);
        deserialized.ShareCode.Should().Be(entry.ShareCode);
        deserialized.ReadingTime.Should().Be(entry.ReadingTime);
        deserialized.UserId.Should().Be(entry.UserId);
        deserialized.FeedId.Should().Be(entry.FeedId);
        deserialized.Starred.Should().Be(entry.Starred);
        deserialized.Tags.Should().BeEquivalentTo(entry.Tags);
        deserialized.Enclosures.Should().BeEquivalentTo(entry.Enclosures);
    }

    [Fact]
    public void Should_Deserialize_Entry_From_Json()
    {
        // Arrange
        var json = """
        {
            "id": 5000,
            "published_at": "2024-02-15T10:30:00Z",
            "changed_at": "2024-02-15T11:00:00Z",
            "created_at": "2024-02-15T10:45:00Z",
            "feed": {
                "id": 200,
                "user_id": 1,
                "feed_url": "https://blog.example.com/rss",
                "site_url": "https://blog.example.com",
                "title": "Example Blog",
                "checked_at": "2024-02-15T12:00:00Z",
                "etag_header": "",
                "last_modified_header": "",
                "parsing_error_message": "",
                "parsing_error_count": 0,
                "disabled": false,
                "ignore_http_cache": false,
                "allow_self_signed_certificates": false,
                "fetch_via_proxy": false,
                "scraper_rules": "",
                "rewrite_rules": "",
                "url_rewrite_rules": "",
                "blocklist_rules": "",
                "keeplist_rules": "",
                "block_filter_entry_rules": "",
                "keep_filter_entry_rules": "",
                "crawler": false,
                "user_agent": "",
                "cookie": "",
                "username": "",
                "password": "",
                "category": {
                    "id": 15,
                    "title": "Tech",
                    "user_id": 1,
                    "hide_globally": false
                },
                "hide_globally": false,
                "disable_http2": false,
                "proxy_url": ""
            },
            "hash": "xyz789",
            "url": "https://blog.example.com/posts/5000",
            "comments_url": "https://blog.example.com/posts/5000/comments",
            "title": "Interesting Article",
            "status": "read",
            "content": "<p>Article content here</p>",
            "author": "Jane Smith",
            "share_code": "share5000",
            "enclosures": [
                {
                    "id": 600,
                    "user_id": 1,
                    "entry_id": 5000,
                    "url": "https://example.com/image.jpg",
                    "mime_type": "image/jpeg",
                    "size": 204800,
                    "media_progression": 0
                }
            ],
            "tags": ["technology", "programming"],
            "reading_time": 8,
            "user_id": 1,
            "feed_id": 200,
            "starred": true
        }
        """;

        // Act
        var entry = JsonSerializer.Deserialize<Entry>(json, _jsonOptions);

        // Assert
        entry.Should().NotBeNull();
        entry!.Id.Should().Be(5000);
        entry.Title.Should().Be("Interesting Article");
        entry.Status.Should().Be(EntryStatus.Read);
        entry.Starred.Should().BeTrue();
        entry.Feed.Should().NotBeNull();
        entry.Feed!.Id.Should().Be(200);
        entry.Feed.Title.Should().Be("Example Blog");
        entry.Feed.Category.Should().NotBeNull();
        entry.Feed.Category!.Title.Should().Be("Tech");
        entry.Tags.Should().BeEquivalentTo(new[] { "technology", "programming" });
        entry.Enclosures.Should().HaveCount(1);
        entry.Enclosures[0].MimeType.Should().Be("image/jpeg");
        entry.Enclosures[0].Size.Should().Be(204800);
    }

    [Theory]
    [InlineData(EntryStatus.Unread, "unread")]
    [InlineData(EntryStatus.Read, "read")]
    [InlineData(EntryStatus.Removed, "removed")]
    public void Should_Serialize_EntryStatus_As_String(EntryStatus status, string expected)
    {
        // Arrange
        var entry = new Entry { Status = status };

        // Act
        var json = JsonSerializer.Serialize(entry, _jsonOptions);

        // Assert
        json.Should().Contain($"\"status\":\"{expected}\"");
    }

    [Theory]
    [InlineData("unread", EntryStatus.Unread)]
    [InlineData("read", EntryStatus.Read)]
    [InlineData("removed", EntryStatus.Removed)]
    public void Should_Deserialize_EntryStatus_From_String(string statusJson, EntryStatus expected)
    {
        // Arrange
        var json = $"{{\"status\":\"{statusJson}\"}}";

        // Act
        var entry = JsonSerializer.Deserialize<Entry>(json, _jsonOptions);

        // Assert
        entry.Should().NotBeNull();
        entry!.Status.Should().Be(expected);
    }

    [Fact]
    public void EntryFilter_Should_Serialize_With_Correct_Property_Names()
    {
        // Arrange
        var filter = new EntryFilter
        {
            Status = EntryStatus.Unread,
            Offset = 10,
            Limit = 50,
            Order = "published_at",
            Direction = "desc",
            Starred = true,
            Search = "keyword",
            CategoryId = 5,
            FeedId = 100
        };

        // Act
        var json = JsonSerializer.Serialize(filter, _jsonOptions);

        // Assert
        json.Should().Contain("\"status\":\"unread\"");
        json.Should().Contain("\"offset\":10");
        json.Should().Contain("\"limit\":50");
        json.Should().Contain("\"order\":\"published_at\"");
        json.Should().Contain("\"direction\":\"desc\"");
        json.Should().Contain("\"starred\":\"1\"");
        json.Should().Contain("\"search\":\"keyword\"");
        json.Should().Contain("\"categoryId\":5");
        json.Should().Contain("\"feedId\":100");
    }

    [Fact]
    public void EntryResultSet_Should_Deserialize_Correctly()
    {
        // Arrange
        var json = """
        {
            "total": 250,
            "entries": [
                {
                    "id": 1001,
                    "title": "First Entry",
                    "url": "https://example.com/1",
                    "status": "unread",
                    "content": "<p>Content 1</p>",
                    "author": "Author 1",
                    "published_at": "2024-01-01T12:00:00Z",
                    "changed_at": "2024-01-01T12:00:00Z",
                    "created_at": "2024-01-01T12:30:00Z",
                    "hash": "hash1",
                    "share_code": "share1",
                    "reading_time": 3,
                    "user_id": 1,
                    "feed_id": 100,
                    "starred": false,
                    "tags": [],
                    "enclosures": []
                },
                {
                    "id": 1002,
                    "title": "Second Entry",
                    "url": "https://example.com/2",
                    "status": "read",
                    "content": "<p>Content 2</p>",
                    "author": "Author 2",
                    "published_at": "2024-01-02T12:00:00Z",
                    "changed_at": "2024-01-02T12:00:00Z",
                    "created_at": "2024-01-02T12:30:00Z",
                    "hash": "hash2",
                    "share_code": "share2",
                    "reading_time": 5,
                    "user_id": 1,
                    "feed_id": 100,
                    "starred": true,
                    "tags": ["tech"],
                    "enclosures": []
                }
            ]
        }
        """;

        // Act
        var resultSet = JsonSerializer.Deserialize<EntryResultSet>(json, _jsonOptions);

        // Assert
        resultSet.Should().NotBeNull();
        resultSet!.Total.Should().Be(250);
        resultSet.Entries.Should().HaveCount(2);
        resultSet.Entries[0].Id.Should().Be(1001);
        resultSet.Entries[0].Title.Should().Be("First Entry");
        resultSet.Entries[0].Status.Should().Be(EntryStatus.Unread);
        resultSet.Entries[1].Id.Should().Be(1002);
        resultSet.Entries[1].Title.Should().Be("Second Entry");
        resultSet.Entries[1].Status.Should().Be(EntryStatus.Read);
        resultSet.Entries[1].Starred.Should().BeTrue();
    }

    [Fact]
    public void EntryUpdateRequest_Should_Serialize_Correctly()
    {
        // Arrange
        var request = new EntryUpdateRequest
        {
            Title = "Updated Title",
            Content = "<p>Updated content</p>"
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<EntryUpdateRequest>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.Title.Should().Be(request.Title);
        deserialized.Content.Should().Be(request.Content);
    }

    [Fact]
    public void EnclosureUpdateRequest_Should_Serialize_Correctly()
    {
        // Arrange
        var request = new EnclosureUpdateRequest
        {
            MediaProgression = 42
        };

        // Act
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<EnclosureUpdateRequest>(json, _jsonOptions);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.MediaProgression.Should().Be(request.MediaProgression);
    }
}