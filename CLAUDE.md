# NetFlux - Miniflux .NET Client

## Project Overview
NetFlux is a .NET 8.0 client library for the Miniflux RSS reader REST API. It provides a fully async, modern C# implementation with comprehensive API coverage, robust error handling, and enterprise-grade quality.

## Architecture

### Project Structure
```
src/NetFlux/
├── Models/                  # Data models and DTOs (22 files)
│   ├── Core Entities/
│   │   ├── User.cs         # 34 properties for user settings
│   │   ├── Category.cs     # Category with feed counts
│   │   ├── Feed.cs         # 31 properties including advanced features
│   │   ├── Entry.cs        # 21 properties for articles
│   │   └── ApiKey.cs       # API key management
│   ├── Request Models/
│   │   ├── UserCreateRequest.cs
│   │   ├── UserUpdateRequest.cs
│   │   ├── FeedCreateRequest.cs
│   │   ├── FeedUpdateRequest.cs
│   │   ├── EntryFilter.cs  # 16 filter properties
│   │   └── ...
│   └── Support Models/
│       ├── Enclosure.cs     # Media attachments
│       ├── FeedIcon.cs      # Base64 feed icons
│       ├── EntryResultSet.cs # Paginated results
│       └── ...
├── Exceptions/              # Typed exception hierarchy (7 types)
│   ├── MinifluxException.cs # Base exception
│   ├── MinifluxAuthenticationException.cs (401)
│   ├── MinifluxForbiddenException.cs (403)
│   ├── MinifluxNotFoundException.cs (404)
│   ├── MinifluxBadRequestException.cs (400)
│   ├── MinifluxServerException.cs (500)
│   └── MinifluxConfigurationException.cs
├── Http/
│   └── MinifluxHttpClient.cs # Low-level HTTP implementation
├── Helpers/
│   ├── BoolToStringConverter.cs # "0"/"1" to bool conversion
│   └── CamelCaseJsonStringEnumConverter.cs
├── Logging/
│   └── LogMessages.cs       # 30+ structured log messages
└── MinifluxClient.cs        # Main API client (615 lines, ~50 methods)

tests/NetFlux.Tests/
├── Http/                    # HTTP endpoint tests
├── Models/                  # Serialization tests
├── Helpers/                 # Converter tests
├── Exceptions/              # Exception behavior tests
├── Utils/                   # Test infrastructure
│   ├── HttpMessageHandlerMock.cs
│   └── TestDataFactory.cs
└── 121 comprehensive tests
```

### Key Design Decisions

1. **Modern C# Features**: 
   - File-scoped namespaces throughout
   - Nullable reference types enabled (`<Nullable>enable</Nullable>`)
   - Required properties where appropriate
   - Pattern matching in switch expressions
   - Source generators for logging

2. **Authentication**: 
   - Dual authentication support via constructor overloads
   - API key: `new MinifluxClient(baseUrl, apiKey)`
   - Basic auth: `new MinifluxClient(baseUrl, username, password)`
   - Automatic header configuration based on auth type

3. **Error Handling**: 
   - Comprehensive typed exception hierarchy
   - HTTP status code to exception mapping
   - JSON error message extraction with fallback
   - Client configuration validation

4. **Async/Await**: 
   - All API methods are async with `Task<T>` returns
   - `CancellationToken` support on every method
   - Proper cancellation propagation

5. **Resource Management**: 
   - Full `IDisposable` pattern implementation
   - Optional external `HttpClient` injection
   - Proper cleanup prevents connection exhaustion

6. **Performance Optimizations**:
   - `HttpCompletionOption.ResponseHeadersRead` for large responses
   - Efficient query string building with minimal allocations
   - Resource pooling through `HttpClient` reuse

## API Coverage

Complete implementation of Miniflux v1 REST API (~50 methods):

### User Management (8 methods)
- `GetCurrentUserAsync()` - Get authenticated user
- `GetUsersAsync()` - List all users (admin)
- `GetUserByIdAsync(userId)` - Get specific user
- `GetUserByUsernameAsync(username)` - Get by username
- `CreateUserAsync(request)` - Create new user
- `UpdateUserAsync(userId, request)` - Update user
- `DeleteUserAsync(userId)` - Delete user
- `MarkUserEntriesAsReadAsync(userId)` - Mark all as read

### API Key Management (3 methods)
- `GetApiKeysAsync()` - List API keys
- `CreateApiKeyAsync(description)` - Create new key
- `DeleteApiKeyAsync(keyId)` - Revoke key

### Category Management (6 methods)
- `GetCategoriesAsync()` - List categories
- `GetCategoriesWithCountersAsync()` - With feed/unread counts
- `GetCategoryAsync(categoryId)` - Get single category
- `CreateCategoryAsync(title)` - Create category
- `UpdateCategoryAsync(categoryId, title, hideGlobally)` - Update
- `DeleteCategoryAsync(categoryId)` - Delete
- `GetCategoryFeedsAsync(categoryId)` - Get category's feeds
- `MarkCategoryEntriesAsReadAsync(categoryId)` - Mark read
- `RefreshCategoryFeedsAsync(categoryId)` - Refresh all feeds

### Feed Management (11 methods)
- `GetFeedsAsync()` - List all feeds
- `GetFeedsWithCountersAsync()` - With unread counts
- `GetFeedAsync(feedId)` - Get single feed
- `GetFeedCountersAsync()` - Get all feed counters
- `CreateFeedAsync(request)` - Add new feed
- `UpdateFeedAsync(feedId, request)` - Update feed
- `DeleteFeedAsync(feedId)` - Remove feed
- `RefreshFeedAsync(feedId)` - Refresh single feed
- `RefreshAllFeedsAsync()` - Refresh all feeds
- `GetFeedIconAsync(feedId)` - Get favicon
- `MarkFeedEntriesAsReadAsync(feedId)` - Mark all read

### Entry Management (12 methods)
- `GetEntriesAsync(filter)` - Get entries with filtering
- `GetFeedEntriesAsync(feedId, filter)` - By feed
- `GetCategoryEntriesAsync(categoryId, filter)` - By category
- `GetEntryAsync(entryId)` - Get single entry
- `UpdateEntryAsync(entryId, title, content)` - Update entry
- `UpdateEntriesStatusAsync(entryIds, status)` - Bulk status
- `ToggleEntryBookmarkAsync(entryId)` - Star/unstar
- `SaveEntryAsync(entryId)` - Save to third-party
- `FetchEntryContentAsync(entryId)` - Get original content
- `GetEntriesAsync(entryIds)` - Get multiple by IDs
- `ToggleEntryBookmarksAsync(entryIds)` - Bulk star
- `FlushHistoryAsync(retentionDays)` - Clean old entries

### Import/Export (3 methods)
- `DiscoverSubscriptionsAsync(url)` - Discover feeds
- `ImportOpmlAsync(opmlContent)` - Import OPML
- `ExportOpmlAsync()` - Export OPML

### Other Operations (5 methods)
- `HealthCheckAsync()` - Service health (returns bool)
- `GetVersionInfoAsync()` - Server version
- `GetIntegrationsAsync()` - Integration config
- `GetIconsAsync(iconIds)` - Get multiple icons
- `GetEnclosureAsync(enclosureId)` - Get media attachment
- `UpdateEnclosureAsync(enclosureId, mediaProgression)` - Update progress

## Advanced Features

### Query Building
```csharp
var filter = new EntryFilter 
{
    Status = EntryStatus.Unread,
    Starred = true,
    Search = "technology",
    CategoryId = 42,
    Limit = 100,
    Direction = "desc",
    Order = "published_at",
    Before = DateTime.UtcNow.AddDays(-7),
    After = DateTime.UtcNow.AddDays(-30)
};
```

### HTTP Client Configuration
- 80-second default timeout
- Configurable via external `HttpClient`
- Base URL normalization (removes `/v1` suffix)
- Automatic content-type headers

### Logging Integration
- Structured logging throughout
- Debug: Request/response details
- Information: Operation status
- Warning: Non-critical issues
- Error: Exceptions and failures

### JSON Serialization
- System.Text.Json with source generators
- CamelCase property naming
- Custom converters for Miniflux quirks
- Null value handling

## Testing Infrastructure

### Test Stack
- **xUnit** - Test framework
- **FluentAssertions** - Readable assertions
- **Moq** - HTTP mocking
- **Coverlet** - Code coverage

### Test Coverage (121 tests)
- HTTP endpoint testing with mocked responses
- Model serialization/deserialization
- Exception handling scenarios
- Helper/converter functionality
- Client initialization variations
- Error response parsing

## Code Quality

### Build Configuration
```xml
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
<EnableNETAnalyzers>true</EnableNETAnalyzers>
<AnalysisLevel>latest</AnalysisLevel>
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

### XML Documentation
- All public types and members documented
- IntelliSense-friendly descriptions
- Parameter documentation
- Exception documentation

## Production Readiness

### ✅ Complete Features
- 100% API coverage
- Comprehensive error handling
- Full async/await implementation
- Resource management
- Structured logging
- XML documentation
- Extensive test suite

### 🚀 Performance Considerations
- Efficient HTTP usage
- Minimal allocations
- Connection pooling
- Response streaming ready

### 🔒 Security
- No credential logging
- Secure header handling
- Input validation
- Proper encoding

## Future Enhancements

### Post-1.0 Considerations
1. **Resilience**: Add Polly for retry policies
2. **Caching**: Response caching layer
3. **Streaming**: Large OPML file support
4. **Batch Operations**: Bulk API calls
5. **Metrics**: OpenTelemetry integration
6. **GraphQL**: Alternative API support

## Development Guidelines

### Contributing
1. Maintain existing code style
2. Add tests for new features
3. Update XML documentation
4. Follow async best practices
5. Use structured logging

### Release Process
```bash
# Build and test
dotnet build -c Release
dotnet test

# Create package
dotnet pack -c Release

# Package location
src/NetFlux/bin/Release/Oire.NetFlux.1.0.0.nupkg
```

## Version History

### 1.0.0 (Current)
- Initial release
- Complete Miniflux v1 API support
- Comprehensive test suite
- Full documentation