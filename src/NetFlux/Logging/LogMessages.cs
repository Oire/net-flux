using Microsoft.Extensions.Logging;

namespace Oire.NetFlux.Logging;

internal static partial class LogMessages {
    // MinifluxClient logs
    [LoggerMessage(Level = LogLevel.Debug, Message = "Performing healthcheck")]
    internal static partial void LogHealthcheck(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Healthcheck result: {IsHealthy}")]
    internal static partial void LogHealthcheckResult(this ILogger logger, bool isHealthy);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Healthcheck failed")]
    internal static partial void LogHealthcheckFailed(this ILogger logger, Exception exception);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Retrieving current user")]
    internal static partial void LogRetrievingCurrentUser(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Creating new user: {Username}")]
    internal static partial void LogCreatingUser(this ILogger logger, string username);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Deleting user with ID: {UserId}")]
    internal static partial void LogDeletingUser(this ILogger logger, long userId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Marking all entries as read for user: {UserId}")]
    internal static partial void LogMarkingAllAsRead(this ILogger logger, long userId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Importing feeds from OPML for user: {UserId}")]
    internal static partial void LogImportingOpml(this ILogger logger, long userId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Imported {Count} feeds from OPML")]
    internal static partial void LogOpmlImported(this ILogger logger, int count);

    [LoggerMessage(Level = LogLevel.Information, Message = "Refreshing category: {CategoryId}")]
    internal static partial void LogRefreshingCategory(this ILogger logger, long categoryId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Refreshing feed: {FeedId}")]
    internal static partial void LogRefreshingFeed(this ILogger logger, long feedId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Marking entry as read: {EntryId}")]
    internal static partial void LogMarkingEntryAsRead(this ILogger logger, long entryId);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Saving entry to third-party service: {Service}")]
    internal static partial void LogSavingToThirdParty(this ILogger logger, string service);

    [LoggerMessage(Level = LogLevel.Information, Message = "Cleaning up old entries (days: {Days})")]
    internal static partial void LogCleaningUpEntries(this ILogger logger, int days);

    [LoggerMessage(Level = LogLevel.Information, Message = "Creating new feed: {FeedUrl}")]
    internal static partial void LogCreatingFeed(this ILogger logger, string feedUrl);

    [LoggerMessage(Level = LogLevel.Information, Message = "Feed created successfully with ID: {FeedId}")]
    internal static partial void LogFeedCreated(this ILogger logger, long feedId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Refreshing all feeds")]
    internal static partial void LogRefreshingAllFeeds(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Updating {Count} entries to status: {Status}")]
    internal static partial void LogUpdatingEntriesStatus(this ILogger logger, int count, string status);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Flushing history - removing all read entries")]
    internal static partial void LogFlushingHistory(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Importing OPML file")]
    internal static partial void LogImportingOpmlFile(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "OPML import completed")]
    internal static partial void LogOpmlImportCompleted(this ILogger logger);

    // MinifluxHttpClient logs
    [LoggerMessage(Level = LogLevel.Debug, Message = "MinifluxHttpClient initialized with endpoint: {Endpoint}, authentication: {AuthType}")]
    internal static partial void LogHttpClientInitialized(this ILogger logger, string endpoint, string authType);

    [LoggerMessage(Level = LogLevel.Debug, Message = "GET request to {Path}")]
    internal static partial void LogGetRequest(this ILogger logger, string path);

    [LoggerMessage(Level = LogLevel.Debug, Message = "POST request to {Path}")]
    internal static partial void LogPostRequest(this ILogger logger, string path);

    [LoggerMessage(Level = LogLevel.Debug, Message = "PUT request to {Path}")]
    internal static partial void LogPutRequest(this ILogger logger, string path);

    [LoggerMessage(Level = LogLevel.Debug, Message = "DELETE request to {Path}")]
    internal static partial void LogDeleteRequest(this ILogger logger, string path);

    [LoggerMessage(Level = LogLevel.Debug, Message = "{Method} {Url} returned {StatusCode}")]
    internal static partial void LogHttpResponse(this ILogger logger, string method, string url, int statusCode);

    [LoggerMessage(Level = LogLevel.Error, Message = "HTTP request failed with status {StatusCode}: {Error}")]
    internal static partial void LogHttpError(this ILogger logger, int statusCode, string error);
}
