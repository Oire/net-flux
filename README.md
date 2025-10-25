# NetFlux

A comprehensive .NET client library for the [Miniflux](https://miniflux.app) RSS reader REST API.

[![NuGet](https://img.shields.io/nuget/v/Oire.NetFlux.svg)](https://www.nuget.org/packages/Oire.NetFlux/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4.svg)](https://dotnet.microsoft.com/download)

## Features

- ✅ **Complete API Coverage** - Full support for Miniflux REST API v1
- 🔐 **Dual Authentication** - API key and basic authentication support  
- 🚀 **Modern Async/Await** - Fully async with CancellationToken support
- 🛡️ **Typed Exceptions** - Comprehensive error handling with specific exception types
- 📚 **Full Documentation** - XML documentation for IntelliSense
- 🧪 **Extensively Tested** - 121+ unit tests with mocked HTTP responses
- 🏗️ **Enterprise Ready** - Logging support, proper resource disposal, and more

## Installation

```bash
dotnet add package Oire.NetFlux
```

## Quick Start

### Using API Key Authentication (Recommended)

```csharp
using Oire.NetFlux;
using Oire.NetFlux.Models;

// Create client with API key
var client = new MinifluxClient("https://your-miniflux-instance.com", "your-api-key");

// Get unread entries
var entries = await client.GetEntriesAsync(new EntryFilter 
{ 
    Status = EntryStatus.Unread,
    Limit = 20 
});

foreach (var entry in entries.Entries)
{
    Console.WriteLine($"{entry.Title} - {entry.FeedTitle}");
}
```

### Using Basic Authentication

```csharp
var client = new MinifluxClient("https://your-miniflux-instance.com", "username", "password");

// Get current user info
var user = await client.GetCurrentUserAsync();
Console.WriteLine($"Logged in as: {user.Username}");
```

## Common Usage Scenarios

### Managing Feeds

```csharp
// Add a new feed
var newFeed = await client.CreateFeedAsync(new FeedCreateRequest
{
    FeedUrl = "https://example.com/rss",
    CategoryId = 1
});

// Get all feeds with unread counts
var feeds = await client.GetFeedsWithCountersAsync();
foreach (var feed in feeds)
{
    Console.WriteLine($"{feed.Title}: {feed.UnreadCount} unread");
}

// Refresh a specific feed
await client.RefreshFeedAsync(feedId);

// Update feed settings
await client.UpdateFeedAsync(feedId, new FeedUpdateRequest
{
    Title = "New Title",
    CategoryId = 2,
    Disabled = false
});
```

### Working with Entries

```csharp
// Get entries with advanced filtering
var filter = new EntryFilter
{
    Status = EntryStatus.Unread,
    Starred = true,
    Search = "technology",
    CategoryId = 5,
    Limit = 50,
    Direction = "desc",
    Order = "published_at",
    After = DateTime.UtcNow.AddDays(-7)
};

var result = await client.GetEntriesAsync(filter);
Console.WriteLine($"Found {result.Total} matching entries");

// Mark entry as read
await client.UpdateEntriesStatusAsync(new[] { entryId }, EntryStatus.Read);

// Star/bookmark an entry
await client.ToggleEntryBookmarkAsync(entryId);

// Save to third-party service (Pocket, Instapaper, etc.)
await client.SaveEntryAsync(entryId);

// Fetch original content
var content = await client.FetchEntryContentAsync(entryId);
```

### Category Management

```csharp
// Create a category
await client.CreateCategoryAsync("Technology");

// Get categories with feed counts
var categories = await client.GetCategoriesWithCountersAsync();
foreach (var category in categories)
{
    Console.WriteLine($"{category.Title}: {category.FeedCount} feeds, {category.TotalUnread} unread");
}

// Mark all entries in category as read
await client.MarkCategoryEntriesAsReadAsync(categoryId);
```

### OPML Import/Export

```csharp
// Export feeds as OPML
string opml = await client.ExportOpmlAsync();
File.WriteAllText("feeds.opml", opml);

// Import OPML file
string opmlContent = File.ReadAllText("feeds.opml");
await client.ImportOpmlAsync(opmlContent);
```

### Feed Discovery

```csharp
// Discover feeds from a URL
var subscriptions = await client.DiscoverSubscriptionsAsync("https://example.com");
foreach (var sub in subscriptions)
{
    Console.WriteLine($"Found: {sub.Title} ({sub.Type}) - {sub.Url}");
}
```

### Health Checks and Version Info

```csharp
// Check if Miniflux is healthy
bool isHealthy = await client.HealthCheckAsync();

// Get version information
var version = await client.GetVersionInfoAsync();
Console.WriteLine($"Miniflux {version.Version} (Go {version.GoVersion})");
```

## Error Handling

NetFlux provides typed exceptions for different error scenarios:

```csharp
try 
{
    var feed = await client.GetFeedAsync(123);
}
catch (MinifluxNotFoundException)
{
    // Handle 404 - Resource not found
    Console.WriteLine("Feed not found");
}
catch (MinifluxAuthenticationException)
{
    // Handle 401 - Authentication failed
    Console.WriteLine("Invalid credentials or API key");
}
catch (MinifluxForbiddenException)
{
    // Handle 403 - Access denied
    Console.WriteLine("You don't have permission to access this resource");
}
catch (MinifluxBadRequestException ex)
{
    // Handle 400 - Bad request
    Console.WriteLine($"Invalid request: {ex.Message}");
}
catch (MinifluxServerException)
{
    // Handle 500 - Server errors
    Console.WriteLine("Server error occurred");
}
catch (MinifluxException ex)
{
    // Handle any other Miniflux-related errors
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Advanced Configuration

### Using Custom HttpClient

```csharp
// Use your own HttpClient instance (e.g., from HttpClientFactory)
var httpClient = httpClientFactory.CreateClient("miniflux");
var client = new MinifluxClient(httpClient, "https://miniflux.example.com", "api-key");
```

### Logging Integration

NetFlux uses Microsoft.Extensions.Logging for structured logging:

```csharp
using Microsoft.Extensions.Logging;

// Configure logging
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

var logger = loggerFactory.CreateLogger<MinifluxClient>();

// Pass logger to client
var client = new MinifluxClient(
    "https://miniflux.example.com", 
    "api-key",
    logger: logger
);
```

### Working with Cancellation Tokens

All async methods support cancellation:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

try
{
    var feeds = await client.GetFeedsAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation timed out");
}
```

## API Coverage

### User Management
- ✅ Get current user
- ✅ List all users (admin)
- ✅ Get user by ID/username
- ✅ Create/update/delete users
- ✅ Mark all user entries as read

### Feed Management  
- ✅ List feeds with/without counters
- ✅ Get/create/update/delete feeds
- ✅ Refresh single feed or all feeds
- ✅ Get feed icon
- ✅ Mark feed entries as read

### Entry Management
- ✅ Get entries with filtering
- ✅ Get entries by feed/category
- ✅ Get/update single entry
- ✅ Update entry status (read/unread)
- ✅ Toggle bookmark status
- ✅ Save to third-party services
- ✅ Fetch original content
- ✅ Flush old entries

### Category Management
- ✅ List categories with/without counters
- ✅ Get/create/update/delete categories
- ✅ Get category feeds
- ✅ Mark category entries as read
- ✅ Refresh category feeds

### Other Features
- ✅ API key management
- ✅ OPML import/export
- ✅ Feed discovery
- ✅ Health checks
- ✅ Version information
- ✅ Icons and enclosures
- ✅ Integration status

## Sample Application

Try the interactive console sample to explore NetFlux features:

```bash
# Clone the repository
git clone https://github.com/Oire/net-flux.git
cd net-flux

# Run the sample
cd samples/NetFlux.Samples.Console
dotnet run
```

The sample demonstrates:
- Interactive authentication (API key or username/password)
- Feed and category management
- Entry browsing and filtering
- OPML export
- Feed discovery
- All major NetFlux features

Perfect for learning the API or testing with your Miniflux instance!

## Requirements

- .NET 8.0 or later
- Miniflux 2.0.0 or later

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

Copyright © 2025 [André Polykanine](https://github.com/Menelion), [Oire Software](https://github.com/Oire) and contributors. 

Licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE) for details.

## Acknowledgments

- [Miniflux](https://miniflux.app) - The excellent minimalist RSS reader
- All contributors who help improve this library