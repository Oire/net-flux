using Oire.NetFlux;
using Oire.NetFlux.Models;
using Oire.NetFlux.Exceptions;
using System.Text;

Console.WriteLine("NetFlux - Miniflux .NET Client Sample");
Console.WriteLine("=====================================\n");

// Get connection details
Console.Write("Enter your Miniflux URL (e.g., https://miniflux.example.com): ");
string? baseUrl = Console.ReadLine();

if (string.IsNullOrWhiteSpace(baseUrl)) {
    Console.WriteLine("URL is required!");
    return;
}

Console.Write("Use API key authentication? (y/n): ");
bool useApiKey = Console.ReadLine()?.ToLower() == "y";

MinifluxClient client;

if (useApiKey) {
    Console.Write("Enter your API key: ");
    string? apiKey = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(apiKey)) {
        Console.WriteLine("API key is required!");

        return;
    }

    client = new MinifluxClient(baseUrl, apiKey);
} else {
    Console.Write("Enter username: ");
    string? username = Console.ReadLine();
    Console.Write("Enter password: ");
    string? password = ReadPassword();

    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
        Console.WriteLine("Username and password are required!");

        return;
    }

    client = new MinifluxClient(baseUrl, username, password);
}

try {
    // Test connection and show user info
    Console.WriteLine("\nTesting connection...");
    var user = await client.GetCurrentUserAsync();
    Console.WriteLine($"✓ Connected as: {user.Username} (ID: {user.Id})");
    Console.WriteLine($"  Language: {user.Language}");
    Console.WriteLine($"  Theme: {user.Theme}");
    Console.WriteLine($"  Admin: {(user.IsAdmin ? "Yes" : "No")}");

    // Show main menu
    while (true) {
        Console.WriteLine("\n=== MAIN MENU ===");
        Console.WriteLine("1. Show feed statistics");
        Console.WriteLine("2. List unread entries");
        Console.WriteLine("3. Manage feeds");
        Console.WriteLine("4. Manage categories");
        Console.WriteLine("5. Search entries");
        Console.WriteLine("6. Export OPML");
        Console.WriteLine("7. Discover feeds from URL");
        Console.WriteLine("8. Show server version");
        Console.WriteLine("0. Exit");
        Console.Write("\nSelect an option: ");

        var choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice) {
            case "1":
                await ShowFeedStatistics(client);
                break;
            case "2":
                await ListUnreadEntries(client);
                break;
            case "3":
                await ManageFeeds(client);
                break;
            case "4":
                await ManageCategories(client);
                break;
            case "5":
                await SearchEntries(client);
                break;
            case "6":
                await ExportOpml(client);
                break;
            case "7":
                await DiscoverFeeds(client);
                break;
            case "8":
                await ShowServerVersion(client);
                break;
            case "0":
                Console.WriteLine("Goodbye!");
                return;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }
} catch (MinifluxAuthenticationException) {
    Console.WriteLine("\n✗ Authentication failed! Check your credentials.");
} catch (MinifluxNotFoundException ex) {
    Console.WriteLine($"\n✗ Not found: {ex.Message}");
} catch (MinifluxBadRequestException ex) {
    Console.WriteLine($"\n✗ Bad request: {ex.Message}");
} catch (MinifluxException ex) {
    Console.WriteLine($"\n✗ Error: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"\n✗ Unexpected error: {ex.Message}");
} finally {
    client.Dispose();
}

// Helper method to read password without echoing
static string ReadPassword() {
    StringBuilder password = new();
    while (true) {
        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Enter) {
            Console.WriteLine();
            break;
        } else if (key.Key == ConsoleKey.Backspace && password.Length > 0) {
            password.Remove(password.Length - 1, 1);
        } else if (key.KeyChar != '\0') {
            password.Append(key.KeyChar);
        }
    }

    return password.ToString();
}

static async Task ShowFeedStatistics(MinifluxClient client) {
    Console.WriteLine("Fetching feed statistics...");

    var feeds = await client.GetFeedsAsync();
    var categories = await client.GetCategoriesAsync(includeCounters: true);
    var feedCounters = await client.GetFeedCountersAsync();

    Console.WriteLine($"\nTotal feeds: {feeds.Count}");
    Console.WriteLine($"Total categories: {categories.Count}");

    var totalUnread = feedCounters.UnreadCounters.Values.Sum();
    Console.WriteLine($"Total unread entries: {totalUnread}");

    Console.WriteLine("\nTop 5 feeds with most unread entries:");
    var topFeeds = feeds
        .Where(f => feedCounters.UnreadCounters.ContainsKey(f.Id) && feedCounters.UnreadCounters[f.Id] > 0)
        .OrderByDescending(f => feedCounters.UnreadCounters[f.Id])
        .Take(5);

    foreach (var feed in topFeeds) {
        var unreadCount = feedCounters.UnreadCounters[feed.Id];
        Console.WriteLine($"  • {feed.Title}: {unreadCount} unread");
    }
}

static async Task ListUnreadEntries(MinifluxClient client) {
    Console.Write("Number of entries to show (default 10): ");
    if (!int.TryParse(Console.ReadLine(), out int limit)) {
        limit = 10;
    }

    Console.WriteLine($"\nFetching {limit} unread entries...");

    var filter = new EntryFilter {
        Status = EntryStatus.Unread,
        Limit = limit,
        Order = "published_at",
        Direction = "desc"
    };

    var result = await client.GetEntriesAsync(filter);
    Console.WriteLine($"Found {result.Total} total unread entries");

    if (result.Entries.Count == 0) {
        Console.WriteLine("No unread entries!");
        return;
    }

    foreach (var entry in result.Entries) {
        var feedTitle = entry.Feed?.Title ?? "Unknown Feed";
        Console.WriteLine($"\n[{feedTitle}]");
        Console.WriteLine($"Title: {entry.Title}");
        Console.WriteLine($"Date: {entry.PublishedAt:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"URL: {entry.Url}");
        if (entry.ReadingTime > 0) {
            Console.WriteLine($"Reading time: {entry.ReadingTime} min");
        }
    }

    Console.Write("\nMark all displayed entries as read? (y/n): ");
    if (Console.ReadLine()?.ToLower() == "y") {
        var entryIds = result.Entries.Select(e => e.Id).ToArray();
        await client.UpdateEntriesStatusAsync(entryIds, EntryStatus.Read);
        Console.WriteLine("✓ Marked as read!");
    }
}

static async Task ManageFeeds(MinifluxClient client) {
    Console.WriteLine("Feed Management");
    Console.WriteLine("1. List all feeds");
    Console.WriteLine("2. Add new feed");
    Console.WriteLine("3. Refresh all feeds");
    Console.WriteLine("4. Delete feed");
    Console.Write("\nSelect an option: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice) {
        case "1":
            var feeds = await client.GetFeedsAsync();
            Console.WriteLine($"You have {feeds.Count} feeds:\n");
            foreach (var feed in feeds.OrderBy(f => f.Title)) {
                Console.WriteLine($"ID {feed.Id}: {feed.Title}");
                Console.WriteLine($"  URL: {feed.FeedUrl}");
                Console.WriteLine($"  Category: {feed.Category?.Title ?? "None"}");
                if (!string.IsNullOrEmpty(feed.ParsingErrorMessage)) {
                    Console.WriteLine($"  ⚠️  Error: {feed.ParsingErrorMessage}");
                }
                Console.WriteLine();
            }
            break;

        case "2":
            Console.Write("Enter feed URL: ");
            var feedUrl = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(feedUrl)) {
                Console.WriteLine("URL is required!");
                return;
            }

            var categories = await client.GetCategoriesAsync();
            Console.WriteLine("\nAvailable categories:");
            foreach (var cat in categories) {
                Console.WriteLine($"  {cat.Id}: {cat.Title}");
            }

            Console.Write("Enter category ID: ");
            if (!long.TryParse(Console.ReadLine(), out long categoryId)) {
                Console.WriteLine("Invalid category ID!");
                return;
            }

            try {
                var newFeedId = await client.CreateFeedAsync(new FeedCreateRequest {
                    FeedUrl = feedUrl,
                    CategoryId = categoryId
                });
                var newFeed = await client.GetFeedAsync(newFeedId);
                Console.WriteLine($"\n✓ Feed added: {newFeed.Title}");
            } catch (MinifluxBadRequestException ex) {
                Console.WriteLine($"✗ Failed to add feed: {ex.Message}");
            }
            break;

        case "3":
            Console.WriteLine("Refreshing all feeds...");
            await client.RefreshAllFeedsAsync();
            Console.WriteLine("✓ All feeds refreshed!");
            break;

        case "4":
            Console.Write("Enter feed ID to delete: ");
            if (!long.TryParse(Console.ReadLine(), out long feedId)) {
                Console.WriteLine("Invalid feed ID!");
                return;
            }

            try {
                var feed = await client.GetFeedAsync(feedId);
                Console.Write($"Delete '{feed.Title}'? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y") {
                    await client.DeleteFeedAsync(feedId);
                    Console.WriteLine("✓ Feed deleted!");
                }
            } catch (MinifluxNotFoundException) {
                Console.WriteLine("Feed not found!");
            }
            break;
    }
}

static async Task ManageCategories(MinifluxClient client) {
    Console.WriteLine("Category Management");
    Console.WriteLine("1. List categories with counters");
    Console.WriteLine("2. Create category");
    Console.WriteLine("3. Rename category");
    Console.Write("\nSelect an option: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice) {
        case "1":
            var categories = await client.GetCategoriesAsync(includeCounters: true);
            Console.WriteLine("Categories:\n");
            foreach (var cat in categories.OrderBy(c => c.Title)) {
                Console.WriteLine($"• {cat.Title}");
                Console.WriteLine($"  Feeds: {cat.FeedCount ?? 0}");
                Console.WriteLine($"  Unread: {cat.TotalUnread ?? 0}");
                Console.WriteLine();
            }
            break;

        case "2":
            Console.Write("Enter category name: ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) {
                Console.WriteLine("Name is required!");
                return;
            }

            var category = await client.CreateCategoryAsync(new CategoryCreateRequest {
                Title = title
            });
            Console.WriteLine($"✓ Category created: {category.Title}");
            break;

        case "3":
            var allCats = await client.GetCategoriesAsync();
            Console.WriteLine("Categories:");
            foreach (var cat in allCats) {
                Console.WriteLine($"  {cat.Id}: {cat.Title}");
            }

            Console.Write("\nEnter category ID to rename: ");
            if (!long.TryParse(Console.ReadLine(), out long catId)) {
                Console.WriteLine("Invalid ID!");
                return;
            }

            Console.Write("Enter new name: ");
            var newTitle = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newTitle)) {
                Console.WriteLine("Name is required!");
                return;
            }

            var updatedCategory = await client.UpdateCategoryAsync(catId, new CategoryUpdateRequest {
                Title = newTitle
            });
            Console.WriteLine($"✓ Category renamed to: {updatedCategory.Title}");
            break;
    }
}

static async Task SearchEntries(MinifluxClient client) {
    Console.Write("Enter search query: ");
    var query = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(query)) {
        Console.WriteLine("Query is required!");
        return;
    }

    Console.WriteLine($"\nSearching for '{query}'...");

    var filter = new EntryFilter {
        Search = query,
        Limit = 20
    };

    var result = await client.GetEntriesAsync(filter);
    Console.WriteLine($"Found {result.Total} matching entries");

    foreach (var entry in result.Entries) {
        var feedTitle = entry.Feed?.Title ?? "Unknown Feed";
        Console.WriteLine($"\n• {entry.Title}");
        Console.WriteLine($"  Feed: {feedTitle}");
        Console.WriteLine($"  Date: {entry.PublishedAt:yyyy-MM-dd}");
        Console.WriteLine($"  Status: {entry.Status}");
    }
}

static async Task ExportOpml(MinifluxClient client) {
    Console.WriteLine("Exporting feeds to OPML...");

    var opmlBytes = await client.ExportOpmlAsync();
    var filename = $"miniflux-export-{DateTime.Now:yyyyMMdd-HHmmss}.opml";

    await File.WriteAllBytesAsync(filename, opmlBytes);
    Console.WriteLine($"✓ Exported to {filename}");
    Console.WriteLine($"  File size: {new FileInfo(filename).Length:N0} bytes");
}

static async Task DiscoverFeeds(MinifluxClient client) {
    Console.Write("Enter website URL to discover feeds: ");
    var url = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(url)) {
        Console.WriteLine("URL is required!");
        return;
    }

    Console.WriteLine($"\nDiscovering feeds at {url}...");

    try {
        var subscriptions = await client.DiscoverAsync(url);

        if (subscriptions.Count == 0) {
            Console.WriteLine("No feeds found!");
            return;
        }

        Console.WriteLine($"Found {subscriptions.Count} feed(s):\n");
        for (int i = 0; i < subscriptions.Count; i++) {
            var sub = subscriptions[i];
            Console.WriteLine($"{i + 1}. {sub.Title}");
            Console.WriteLine($"   Type: {sub.Type}");
            Console.WriteLine($"   URL: {sub.Url}");
            Console.WriteLine();
        }
    } catch (MinifluxException ex) {
        Console.WriteLine($"✗ Discovery failed: {ex.Message}");
    }
}

static async Task ShowServerVersion(MinifluxClient client) {
    var version = await client.GetVersionAsync();

    Console.WriteLine("Miniflux Server Information:");
    Console.WriteLine($"  Version: {version.Version}");
    Console.WriteLine($"  Build Date: {version.BuildDate}");
    Console.WriteLine($"  Commit: {version.Commit}");
    Console.WriteLine($"  Go Version: {version.GoVersion}");
    Console.WriteLine($"  Compiler: {version.Compiler}");
    Console.WriteLine($"  Architecture: {version.Arch}");
    Console.WriteLine($"  OS: {version.Os}");
}
