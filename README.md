# NetFlux

A comprehensive .NET client library for the [Miniflux](https://miniflux.app) RSS reader REST API.

## Installation

```bash
dotnet add package Oire.NetFlux
```

## Features

- Full support for Miniflux REST API v1
- Modern C# with nullable reference types
- Async/await throughout
- Proper exception handling with typed exceptions
- Support for both API key and basic authentication
- Comprehensive XML documentation

## Usage

### Basic Authentication

```csharp
using Oire.NetFlux;

var client = new MinifluxClient("https://your-miniflux-instance.com", "username", "password");

// Get current user
var user = await client.GetCurrentUserAsync();
Console.WriteLine($"Logged in as: {user.Username}");

// Get all feeds
var feeds = await client.GetFeedsAsync();
foreach (var feed in feeds)
{
    Console.WriteLine($"{feed.Title}: {feed.FeedUrl}");
}
```

### API Key Authentication

```csharp
var client = new MinifluxClient("https://your-miniflux-instance.com", "your-api-key");

// Get unread entries
var filter = new EntryFilter 
{ 
    Status = EntryStatus.Unread,
    Limit = 10 
};
var entries = await client.GetEntriesAsync(filter);
```

### Error Handling

```csharp
try {
    var feed = await client.GetFeedAsync(123);
}
catch (MinifluxNotFoundException)
{
    Console.WriteLine("Feed not found");
}
catch (MinifluxAuthenticationException)
{
    Console.WriteLine("Authentication failed");
}
catch (MinifluxException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

## API Coverage

- [X] User management
- [X] Category management  
- [X] Feed management
- [X] Entry management
- [X] API key management
- [X] OPML import/export
- [X] Feed discovery
- [X] Icons and enclosures

## Requirements

- .NET 8.0 or later

## License

Copyright © 2025 [André Polykanine](https://github.com/Menelion), [Oire Software](https://github.com/Oire) and contributors. Licensed under the Apache License, Version 2.0.
