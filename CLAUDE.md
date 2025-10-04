# NetFlux - Miniflux .NET Client

## Project Overview
NetFlux is a .NET 8.0 client library for the Miniflux RSS reader REST API. It provides a fully async, modern C# implementation with comprehensive API coverage.

## Architecture

### Project Structure
```
/
├── Models/              # Data models and DTOs
│   ├── User.cs
│   ├── Category.cs
│   ├── Feed.cs
│   ├── Entry.cs
│   ├── Subscription.cs
│   ├── ApiKey.cs
│   └── VersionInfo.cs
├── Exceptions/          # Custom exception types
│   └── MinifluxException.cs
├── Http/               # HTTP client implementation
│   └── MinifluxHttpClient.cs
└── MinifluxClient.cs   # Main client API
```

### Key Design Decisions

1. **Modern C# Features**: Uses file-scoped namespaces, nullable reference types, required properties, and pattern matching throughout.

2. **Authentication**: Supports both basic authentication (username/password) and API key authentication via separate constructors.

3. **Error Handling**: Typed exceptions (`MinifluxAuthenticationException`, `MinifluxNotFoundException`, etc.) for different HTTP status codes.

4. **Async/Await**: All API methods are async with `CancellationToken` support.

5. **Resource Management**: Implements `IDisposable` pattern for proper cleanup of HTTP resources.

## API Coverage

All Miniflux v1 API endpoints are implemented:
- User management (CRUD, mark all as read)
- API key management
- Categories (CRUD, counters, mark as read, refresh)
- Feeds (CRUD, refresh, icons, OPML import/export)
- Entries (CRUD, filtering, starring, save to third-party)
- Discovery (feed discovery from URL)
- Integrations status

## Usage Examples

### Basic Authentication
```csharp
var client = new MinifluxClient("https://miniflux.example.com", "username", "password");
var user = await client.GetCurrentUserAsync();
```

### API Key Authentication
```csharp
var client = new MinifluxClient("https://miniflux.example.com", "api-key-here");
```

### Entry Filtering
```csharp
var filter = new EntryFilter 
{
    Status = EntryStatus.Unread,
    Limit = 50,
    Order = "published_at",
    Direction = "desc"
};
var entries = await client.GetEntriesAsync(filter);
```

## Building and Testing

```bash
# Build
dotnet build

# Pack NuGet package
dotnet pack -c Release

# Run tests (when added)
dotnet test
```

## NuGet Package

- Package ID: `Oire.NetFlux`
- Target Framework: .NET 8.0
- License: Apache-2.0
- Author: Oire Software

## Future Enhancements

Consider adding:
1. Unit tests with mocked HTTP responses
2. Integration tests against a real Miniflux instance
3. Retry policies for transient failures
4. Request/response logging for debugging
5. Streaming support for large OPML imports
6. Bulk operations for better performance

## Development Notes

- The HTTP client uses `HttpCompletionOption.ResponseHeadersRead` for better performance with large responses
- JSON serialization uses `System.Text.Json` with camelCase property naming
- All models use `JsonPropertyName` attributes to match the Miniflux API
- The base URL is normalized to remove trailing slashes and `/v1` suffix
- Proper disposal pattern prevents HTTP client connection exhaustion