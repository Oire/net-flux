using Oire.NetFlux.Models;

namespace Oire.NetFlux.Tests.Fixtures;

public static class TestDataFactory {
    public static User CreateUser(long id = TestConstants.UserId, string username = TestConstants.Username) {
        return new User {
            Id = id,
            Username = username,
            IsAdmin = false,
            Theme = "system_serif",
            Language = "en_US",
            Timezone = "America/New_York",
            EntrySortingDirection = "desc",
            EntrySortingOrder = "published_at",
            Stylesheet = "",
            GoogleId = "",
            OpenIdConnectId = "",
            EntriesPerPage = 100,
            KeyboardShortcuts = true,
            ShowReadingTime = true,
            EntrySwipe = true,
            GestureNav = "tap",
            LastLoginAt = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc),
            DisplayMode = "standalone",
            DefaultReadingSpeed = 265,
            CjkReadingSpeed = 500,
            DefaultHomePage = "unread",
            CategoriesSortingOrder = "alphabetical",
            MarkReadOnView = false,
            MediaPlaybackRate = 1.0,
            ExternalFontHosts = "fonts.googleapis.com,fonts.gstatic.com",
            AlwaysOpenExternalLinks = false,
            OpenExternalLinksInNewTab = true
        };
    }

    public static Category CreateCategory(long id = TestConstants.CategoryId, string title = "Technology") {
        return new Category {
            Id = id,
            Title = title,
            UserId = TestConstants.UserId,
            HideGlobally = false,
            FeedCount = 5,
            TotalUnread = 10
        };
    }

    public static Feed CreateFeed(long id = TestConstants.FeedId, long categoryId = TestConstants.CategoryId) {
        return new Feed {
            Id = id,
            UserId = TestConstants.UserId,
            FeedUrl = "https://example.com/feed.xml",
            SiteUrl = "https://example.com",
            Title = "Example Blog",
            CheckedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc),
            EtagHeader = "W/\"123456789\"",
            LastModifiedHeader = "Mon, 01 Jan 2024 12:00:00 GMT",
            ParsingErrorMessage = "",
            ParsingErrorCount = 0,
            Disabled = false,
            IgnoreHttpCache = false,
            AllowSelfSignedCertificates = false,
            FetchViaProxy = false,
            ScraperRules = "",
            RewriteRules = "",
            UrlRewriteRules = "",
            BlocklistRules = "",
            KeeplistRules = "",
            BlockFilterEntryRules = "",
            KeepFilterEntryRules = "",
            Crawler = false,
            UserAgent = "",
            Cookie = "",
            Username = "",
            Password = "",
            Category = CreateCategory(categoryId),
            HideGlobally = false,
            DisableHttp2 = false,
            ProxyUrl = ""
        };
    }

    public static Entry CreateEntry(long id = TestConstants.EntryId, long feedId = TestConstants.FeedId) {
        return new Entry {
            Id = id,
            PublishedAt = new DateTime(2024, 1, 1, 8, 0, 0, DateTimeKind.Utc),
            ChangedAt = new DateTime(2024, 1, 1, 8, 0, 0, DateTimeKind.Utc),
            CreatedAt = new DateTime(2024, 1, 1, 8, 30, 0, DateTimeKind.Utc),
            Feed = CreateFeed(feedId),
            Hash = "abc123def456",
            Url = "https://example.com/post/123",
            CommentsUrl = "https://example.com/post/123/comments",
            Title = "Example Post Title",
            Status = EntryStatus.Unread,
            Content = "<p>This is the post content.</p>",
            Author = "John Doe",
            ShareCode = "share123",
            Enclosures = new List<Enclosure>(),
            Tags = new List<string> { "tech", "news" },
            ReadingTime = 5,
            UserId = TestConstants.UserId,
            FeedId = feedId,
            Starred = false
        };
    }

    public static ApiKey CreateApiKey(long id = TestConstants.ApiKeyId) {
        return new ApiKey {
            Id = id,
            UserId = TestConstants.UserId,
            Token = TestConstants.ApiKey,
            Description = "Test API Key",
            LastUsedAt = new DateTime(2024, 1, 1, 15, 0, 0, DateTimeKind.Utc),
            CreatedAt = new DateTime(2024, 1, 1, 9, 0, 0, DateTimeKind.Utc)
        };
    }

    public static VersionInfo CreateVersionInfo() {
        return new VersionInfo {
            Version = "2.1.0",
            Commit = "abc123def",
            BuildDate = "2024-01-01T00:00:00Z",
            GoVersion = "go1.21.5",
            Compiler = "gc",
            Arch = "amd64",
            Os = "linux"
        };
    }

    public static Subscription CreateSubscription(string title = "Example Feed", string url = "https://example.com/feed.xml") {
        return new Subscription {
            Title = title,
            Url = url,
            Type = "rss"
        };
    }

    public static Enclosure CreateEnclosure(long id = TestConstants.EnclosureId) {
        return new Enclosure {
            Id = id,
            UserId = TestConstants.UserId,
            EntryId = TestConstants.EntryId,
            Url = "https://example.com/podcast.mp3",
            MimeType = "audio/mpeg",
            Size = 10485760, // 10MB
            MediaProgression = 0
        };
    }

    public static FeedIcon CreateFeedIcon(long id = TestConstants.IconId) {
        return new FeedIcon {
            Id = id,
            MimeType = "image/png",
            Data = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg=="
        };
    }

    public static FeedCounters CreateFeedCounters() {
        return new FeedCounters {
            ReadCounters = new Dictionary<long, int> { { 100, 50 }, { 101, 30 } },
            UnreadCounters = new Dictionary<long, int> { { 100, 10 }, { 101, 5 } }
        };
    }

    public static EntryResultSet CreateEntryResultSet(int total = 100, int count = 10) {
        var entries = new List<Entry>();
        for (int i = 0; i < count; i++) {
            entries.Add(CreateEntry(TestConstants.EntryId + i));
        }

        return new EntryResultSet {
            Total = total,
            Entries = entries
        };
    }
}
