// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Oire.NetFlux.Exceptions;
using Oire.NetFlux.Helpers;
using Oire.NetFlux.Http;
using Oire.NetFlux.Models;

namespace Oire.NetFlux;

public class MinifluxClient: IDisposable {
    private readonly MinifluxHttpClient _httpClient;
    private readonly ILogger<MinifluxClient> _logger;
    private bool _disposed;

    /// <summary>
    /// Creates a new Miniflux client with basic authentication.
    /// </summary>
    /// <param name="endpoint">The Miniflux server URL</param>
    /// <param name="username">Username for authentication</param>
    /// <param name="password">Password for authentication</param>
    /// <param name="logger">Optional logger instance</param>
    /// <param name="timeout">Optional timeout for HTTP requests</param>
    public MinifluxClient(string endpoint, string username, string password, ILogger<MinifluxClient>? logger = null, TimeSpan? timeout = null) {
        if (string.IsNullOrWhiteSpace(endpoint)) {
            throw new ArgumentNullException(nameof(endpoint));
        }
        if (string.IsNullOrWhiteSpace(username)) {
            throw new ArgumentNullException(nameof(username));
        }
        if (string.IsNullOrWhiteSpace(password)) {
            throw new ArgumentNullException(nameof(password));
        }

        _logger = logger ?? NullLogger<MinifluxClient>.Instance;
        _httpClient = new MinifluxHttpClient(endpoint, username, password, null, timeout, _logger);
    }

    /// <summary>
    /// Creates a new Miniflux client with API key authentication.
    /// </summary>
    /// <param name="endpoint">The Miniflux server URL</param>
    /// <param name="apiKey">API key for authentication</param>
    /// <param name="logger">Optional logger instance</param>
    /// <param name="timeout">Optional timeout for HTTP requests</param>
    public MinifluxClient(string endpoint, string apiKey, ILogger<MinifluxClient>? logger = null, TimeSpan? timeout = null) {
        if (string.IsNullOrWhiteSpace(endpoint)) {
            throw new ArgumentNullException(nameof(endpoint));
        }
        if (string.IsNullOrWhiteSpace(apiKey)) {
            throw new ArgumentNullException(nameof(apiKey));
        }

        _logger = logger ?? NullLogger<MinifluxClient>.Instance;
        _httpClient = new MinifluxHttpClient(endpoint, null, null, apiKey, timeout, _logger);
    }

    #region Health and Version

    /// <summary>
    /// Checks if the Miniflux instance is healthy.
    /// </summary>
    public async Task<bool> HealthcheckAsync(CancellationToken cancellationToken = default) {
        try {
            _logger.LogDebug("Performing healthcheck");
            var response = await _httpClient.GetBytesAsync("/healthcheck", cancellationToken);
            var isHealthy = Encoding.UTF8.GetString(response) == "OK";
            _logger.LogInformation("Healthcheck result: {IsHealthy}", isHealthy);
            return isHealthy;
        } catch (Exception ex) {
            _logger.LogWarning(ex, "Healthcheck failed");
            return false;
        }
    }

    /// <summary>
    /// Gets version information about the Miniflux instance.
    /// </summary>
    public async Task<VersionInfo> GetVersionAsync(CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<VersionInfo>("/v1/version", cancellationToken)
            ?? throw new MinifluxException("Failed to retrieve version information");
    }

    #endregion

    #region User Management

    /// <summary>
    /// Gets the currently authenticated user.
    /// </summary>
    public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default) {
        _logger.LogDebug("Retrieving current user");
        return await _httpClient.GetAsync<User>("/v1/me", cancellationToken)
            ?? throw new MinifluxException("Failed to retrieve current user");
    }

    /// <summary>
    /// Gets all users (admin only).
    /// </summary>
    public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<List<User>>("/v1/users", cancellationToken)
            ?? new List<User>();
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    public async Task<User> GetUserAsync(long userId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<User>($"/v1/users/{userId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"User with ID {userId} not found");
    }

    /// <summary>
    /// Gets a user by username.
    /// </summary>
    public async Task<User> GetUserAsync(string username, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<User>($"/v1/users/{username}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"User '{username}' not found");
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    public async Task<User> CreateUserAsync(UserCreateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        _logger.LogInformation("Creating new user: {Username}", request.Username);
        return await _httpClient.PostAsync<User>("/v1/users", request, cancellationToken)
            ?? throw new MinifluxException("Failed to create user");
    }

    /// <summary>
    /// Updates a user.
    /// </summary>
    public async Task<User> UpdateUserAsync(long userId, UserUpdateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        return await _httpClient.PutAsync<User>($"/v1/users/{userId}", request, cancellationToken)
            ?? throw new MinifluxException("Failed to update user");
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    public async Task DeleteUserAsync(long userId, CancellationToken cancellationToken = default) {
        _logger.LogWarning("Deleting user with ID: {UserId}", userId);
        await _httpClient.DeleteAsync($"/v1/users/{userId}", cancellationToken);
    }

    /// <summary>
    /// Marks all entries as read for a user.
    /// </summary>
    public async Task MarkAllAsReadAsync(long userId, CancellationToken cancellationToken = default) {
        _logger.LogInformation("Marking all entries as read for user: {UserId}", userId);
        await _httpClient.PutAsync<object>($"/v1/users/{userId}/mark-all-as-read", null, cancellationToken);
    }

    #endregion

    #region API Key Management

    /// <summary>
    /// Gets all API keys for the current user.
    /// </summary>
    public async Task<List<ApiKey>> GetApiKeysAsync(CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<List<ApiKey>>("/v1/api-keys", cancellationToken)
            ?? new List<ApiKey>();
    }

    /// <summary>
    /// Creates a new API key.
    /// </summary>
    public async Task<ApiKey> CreateApiKeyAsync(ApiKeyCreateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        return await _httpClient.PostAsync<ApiKey>("/v1/api-keys", request, cancellationToken)
            ?? throw new MinifluxException("Failed to create API key");
    }

    /// <summary>
    /// Deletes an API key.
    /// </summary>
    public async Task DeleteApiKeyAsync(long apiKeyId, CancellationToken cancellationToken = default) {
        await _httpClient.DeleteAsync($"/v1/api-keys/{apiKeyId}", cancellationToken);
    }

    #endregion

    #region Category Management

    /// <summary>
    /// Gets all categories.
    /// </summary>
    public async Task<List<Category>> GetCategoriesAsync(bool includeCounters = false, CancellationToken cancellationToken = default) {
        var path = includeCounters ? "/v1/categories?counts=true" : "/v1/categories";
        return await _httpClient.GetAsync<List<Category>>(path, cancellationToken)
            ?? new List<Category>();
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    public async Task<Category> CreateCategoryAsync(CategoryCreateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        return await _httpClient.PostAsync<Category>("/v1/categories", request, cancellationToken)
            ?? throw new MinifluxException("Failed to create category");
    }

    /// <summary>
    /// Updates a category.
    /// </summary>
    public async Task<Category> UpdateCategoryAsync(long categoryId, CategoryUpdateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        return await _httpClient.PutAsync<Category>($"/v1/categories/{categoryId}", request, cancellationToken)
            ?? throw new MinifluxException("Failed to update category");
    }

    /// <summary>
    /// Deletes a category.
    /// </summary>
    public async Task DeleteCategoryAsync(long categoryId, CancellationToken cancellationToken = default) {
        await _httpClient.DeleteAsync($"/v1/categories/{categoryId}", cancellationToken);
    }

    /// <summary>
    /// Marks all entries in a category as read.
    /// </summary>
    public async Task MarkCategoryAsReadAsync(long categoryId, CancellationToken cancellationToken = default) {
        await _httpClient.PutAsync<object>($"/v1/categories/{categoryId}/mark-all-as-read", null, cancellationToken);
    }

    /// <summary>
    /// Gets all feeds in a category.
    /// </summary>
    public async Task<List<Feed>> GetCategoryFeedsAsync(long categoryId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<List<Feed>>($"/v1/categories/{categoryId}/feeds", cancellationToken)
            ?? new List<Feed>();
    }

    /// <summary>
    /// Refreshes all feeds in a category.
    /// </summary>
    public async Task RefreshCategoryAsync(long categoryId, CancellationToken cancellationToken = default) {
        await _httpClient.PutAsync<object>($"/v1/categories/{categoryId}/refresh", null, cancellationToken);
    }

    #endregion

    #region Feed Management

    /// <summary>
    /// Gets all feeds.
    /// </summary>
    public async Task<List<Feed>> GetFeedsAsync(CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<List<Feed>>("/v1/feeds", cancellationToken)
            ?? new List<Feed>();
    }

    /// <summary>
    /// Gets a feed by ID.
    /// </summary>
    public async Task<Feed> GetFeedAsync(long feedId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<Feed>($"/v1/feeds/{feedId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Feed with ID {feedId} not found");
    }

    /// <summary>
    /// Creates a new feed.
    /// </summary>
    public async Task<long> CreateFeedAsync(FeedCreateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        _logger.LogInformation("Creating new feed: {FeedUrl}", request.FeedUrl);
        var response = await _httpClient.PostAsync<Dictionary<string, long>>("/v1/feeds", request, cancellationToken);
        var feedId = response?["feed_id"] ?? throw new MinifluxException("Failed to create feed");
        _logger.LogInformation("Feed created successfully with ID: {FeedId}", feedId);
        return feedId;
    }

    /// <summary>
    /// Updates a feed.
    /// </summary>
    public async Task<Feed> UpdateFeedAsync(long feedId, FeedUpdateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        return await _httpClient.PutAsync<Feed>($"/v1/feeds/{feedId}", request, cancellationToken)
            ?? throw new MinifluxException("Failed to update feed");
    }

    /// <summary>
    /// Deletes a feed.
    /// </summary>
    public async Task DeleteFeedAsync(long feedId, CancellationToken cancellationToken = default) {
        await _httpClient.DeleteAsync($"/v1/feeds/{feedId}", cancellationToken);
    }

    /// <summary>
    /// Marks all entries in a feed as read.
    /// </summary>
    public async Task MarkFeedAsReadAsync(long feedId, CancellationToken cancellationToken = default) {
        await _httpClient.PutAsync<object>($"/v1/feeds/{feedId}/mark-all-as-read", null, cancellationToken);
    }

    /// <summary>
    /// Refreshes a feed.
    /// </summary>
    public async Task RefreshFeedAsync(long feedId, CancellationToken cancellationToken = default) {
        await _httpClient.PutAsync<object>($"/v1/feeds/{feedId}/refresh", null, cancellationToken);
    }

    /// <summary>
    /// Refreshes all feeds.
    /// </summary>
    public async Task RefreshAllFeedsAsync(CancellationToken cancellationToken = default) {
        _logger.LogInformation("Refreshing all feeds");
        await _httpClient.PutAsync<object>("/v1/feeds/refresh", null, cancellationToken);
    }

    /// <summary>
    /// Gets a feed icon.
    /// </summary>
    public async Task<FeedIcon> GetFeedIconAsync(long feedId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<FeedIcon>($"/v1/feeds/{feedId}/icon", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Icon for feed {feedId} not found");
    }

    /// <summary>
    /// Gets feed counters.
    /// </summary>
    public async Task<FeedCounters> GetFeedCountersAsync(CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<FeedCounters>("/v1/feeds/counters", cancellationToken)
            ?? new FeedCounters();
    }

    #endregion

    #region Entry Management

    /// <summary>
    /// Gets entries with optional filtering.
    /// </summary>
    public async Task<EntryResultSet> GetEntriesAsync(EntryFilter? filter = null, CancellationToken cancellationToken = default) {
        var path = BuildFilterQuery("/v1/entries", filter);
        return await _httpClient.GetAsync<EntryResultSet>(path, cancellationToken)
            ?? new EntryResultSet();
    }

    /// <summary>
    /// Gets entries for a specific feed.
    /// </summary>
    public async Task<EntryResultSet> GetFeedEntriesAsync(long feedId, EntryFilter? filter = null, CancellationToken cancellationToken = default) {
        var path = BuildFilterQuery($"/v1/feeds/{feedId}/entries", filter);
        return await _httpClient.GetAsync<EntryResultSet>(path, cancellationToken)
            ?? new EntryResultSet();
    }

    /// <summary>
    /// Gets entries for a specific category.
    /// </summary>
    public async Task<EntryResultSet> GetCategoryEntriesAsync(long categoryId, EntryFilter? filter = null, CancellationToken cancellationToken = default) {
        var path = BuildFilterQuery($"/v1/categories/{categoryId}/entries", filter);
        return await _httpClient.GetAsync<EntryResultSet>(path, cancellationToken)
            ?? new EntryResultSet();
    }

    /// <summary>
    /// Gets a single entry.
    /// </summary>
    public async Task<Entry> GetEntryAsync(long entryId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<Entry>($"/v1/entries/{entryId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Entry with ID {entryId} not found");
    }

    /// <summary>
    /// Gets a single feed entry.
    /// </summary>
    public async Task<Entry> GetFeedEntryAsync(long feedId, long entryId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<Entry>($"/v1/feeds/{feedId}/entries/{entryId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Entry with ID {entryId} in feed {feedId} not found");
    }

    /// <summary>
    /// Gets a single category entry.
    /// </summary>
    public async Task<Entry> GetCategoryEntryAsync(long categoryId, long entryId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<Entry>($"/v1/categories/{categoryId}/entries/{entryId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Entry with ID {entryId} in category {categoryId} not found");
    }

    /// <summary>
    /// Updates multiple entries' status.
    /// </summary>
    public async Task UpdateEntriesStatusAsync(IEnumerable<long> entryIds, EntryStatus status, CancellationToken cancellationToken = default) {
        var entryIdList = entryIds.ToList();
        var statusString = status.ToString().ToLowerInvariant();
        _logger.LogInformation("Updating {Count} entries to status: {Status}", entryIdList.Count, statusString);
        var payload = new { entry_ids = entryIdList, status = statusString };
        await _httpClient.PutAsync<object>("/v1/entries", payload, cancellationToken);
    }

    /// <summary>
    /// Updates an entry.
    /// </summary>
    public async Task<Entry> UpdateEntryAsync(long entryId, EntryUpdateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        return await _httpClient.PutAsync<Entry>($"/v1/entries/{entryId}", request, cancellationToken)
            ?? throw new MinifluxException("Failed to update entry");
    }

    /// <summary>
    /// Toggles an entry's starred status.
    /// </summary>
    public async Task ToggleStarredAsync(long entryId, CancellationToken cancellationToken = default) {
        await _httpClient.PutAsync<object>($"/v1/entries/{entryId}/star", null, cancellationToken);
    }

    /// <summary>
    /// Saves an entry to a third-party service.
    /// </summary>
    public async Task SaveEntryAsync(long entryId, CancellationToken cancellationToken = default) {
        await _httpClient.PostAsync<object>($"/v1/entries/{entryId}/save", null, cancellationToken);
    }

    /// <summary>
    /// Fetches the original content of an entry.
    /// </summary>
    public async Task<string> FetchEntryOriginalContentAsync(long entryId, CancellationToken cancellationToken = default) {
        var response = await _httpClient.GetAsync<Dictionary<string, string>>($"/v1/entries/{entryId}/fetch-content", cancellationToken);
        return response?["content"] ?? string.Empty;
    }

    /// <summary>
    /// Flushes history (removes all read entries).
    /// </summary>
    public async Task FlushHistoryAsync(CancellationToken cancellationToken = default) {
        _logger.LogWarning("Flushing history - removing all read entries");
        await _httpClient.PutAsync<object>("/v1/flush-history", null, cancellationToken);
    }

    #endregion

    #region Discovery and Import/Export

    /// <summary>
    /// Discovers feeds from a URL.
    /// </summary>
    public async Task<List<Subscription>> DiscoverAsync(string url, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(url);
        var payload = new { url };
        return await _httpClient.PostAsync<List<Subscription>>("/v1/discover", payload, cancellationToken)
            ?? new List<Subscription>();
    }

    /// <summary>
    /// Exports feeds as OPML.
    /// </summary>
    public async Task<byte[]> ExportOpmlAsync(CancellationToken cancellationToken = default) {
        return await _httpClient.GetBytesAsync("/v1/export", cancellationToken);
    }

    /// <summary>
    /// Imports feeds from OPML.
    /// </summary>
    public async Task ImportOpmlAsync(Stream opmlStream, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(opmlStream);
        _logger.LogInformation("Importing OPML file");
        await _httpClient.PostFileAsync("/v1/import", opmlStream, cancellationToken);
        _logger.LogInformation("OPML import completed");
    }

    #endregion

    #region Other Operations

    /// <summary>
    /// Gets the status of integrations.
    /// </summary>
    public async Task<bool> GetIntegrationsStatusAsync(CancellationToken cancellationToken = default) {
        var response = await _httpClient.GetAsync<Dictionary<string, bool>>("/v1/integrations/status", cancellationToken);
        return response?["has_integrations"] ?? false;
    }

    /// <summary>
    /// Gets an icon by ID.
    /// </summary>
    public async Task<FeedIcon> GetIconAsync(long iconId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<FeedIcon>($"/v1/icons/{iconId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Icon with ID {iconId} not found");
    }

    /// <summary>
    /// Gets an enclosure by ID.
    /// </summary>
    public async Task<Enclosure> GetEnclosureAsync(long enclosureId, CancellationToken cancellationToken = default) {
        return await _httpClient.GetAsync<Enclosure>($"/v1/enclosures/{enclosureId}", cancellationToken)
            ?? throw new MinifluxNotFoundException($"Enclosure with ID {enclosureId} not found");
    }

    /// <summary>
    /// Updates an enclosure.
    /// </summary>
    public async Task UpdateEnclosureAsync(long enclosureId, EnclosureUpdateRequest request, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(request);
        await _httpClient.PutAsync<object>($"/v1/enclosures/{enclosureId}", request, cancellationToken);
    }

    #endregion

    #region Helper Methods

    private static string BuildFilterQuery(string basePath, EntryFilter? filter) {
        if (filter is null) {
            return basePath;
        }

        var query = HttpUtility.ParseQueryString(string.Empty);

        if (filter.Status.HasValue) {
            query["status"] = filter.Status.Value.ToString().ToLowerInvariant();
        }
        if (!string.IsNullOrEmpty(filter.Direction)) {
            query["direction"] = filter.Direction;
        }
        if (!string.IsNullOrEmpty(filter.Order)) {
            query["order"] = filter.Order;
        }
        if (filter.Limit.HasValue && filter.Limit >= 0) {
            query["limit"] = filter.Limit.Value.ToString();
        }
        if (filter.Offset.HasValue && filter.Offset >= 0) {
            query["offset"] = filter.Offset.Value.ToString();
        }
        if (filter.After.HasValue && filter.After > 0) {
            query["after"] = filter.After.Value.ToString();
        }
        if (filter.Before.HasValue && filter.Before > 0) {
            query["before"] = filter.Before.Value.ToString();
        }
        if (filter.PublishedAfter.HasValue && filter.PublishedAfter > 0) {
            query["published_after"] = filter.PublishedAfter.Value.ToString();
        }
        if (filter.PublishedBefore.HasValue && filter.PublishedBefore > 0) {
            query["published_before"] = filter.PublishedBefore.Value.ToString();
        }
        if (filter.ChangedAfter.HasValue && filter.ChangedAfter > 0) {
            query["changed_after"] = filter.ChangedAfter.Value.ToString();
        }
        if (filter.ChangedBefore.HasValue && filter.ChangedBefore > 0) {
            query["changed_before"] = filter.ChangedBefore.Value.ToString();
        }
        if (filter.AfterEntryId.HasValue && filter.AfterEntryId > 0) {
            query["after_entry_id"] = filter.AfterEntryId.Value.ToString();
        }
        if (filter.BeforeEntryId.HasValue && filter.BeforeEntryId > 0) {
            query["before_entry_id"] = filter.BeforeEntryId.Value.ToString();
        }
        var starredString = BoolToStringConverter.ToQueryString(filter.Starred);
        if (starredString is not null) {
            query["starred"] = starredString;
        }
        if (!string.IsNullOrEmpty(filter.Search)) {
            query["search"] = filter.Search;
        }
        if (filter.CategoryId.HasValue && filter.CategoryId > 0) {
            query["category_id"] = filter.CategoryId.Value.ToString();
        }
        if (filter.FeedId.HasValue && filter.FeedId > 0) {
            query["feed_id"] = filter.FeedId.Value.ToString();
        }
        if (filter.GloballyVisible) {
            query["globally_visible"] = "true";
        }

        if (filter.Statuses is not null) {
            foreach (var status in filter.Statuses) {
                query.Add("status", status.ToString().ToLowerInvariant());
            }
        }

        var queryString = query.ToString();
        return string.IsNullOrEmpty(queryString) ? basePath : $"{basePath}?{queryString}";
    }

    #endregion

    #region IDisposable

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }

    #endregion
}
