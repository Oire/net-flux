// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a RSS/Atom feed in Miniflux with all its configuration settings and metadata.
/// </summary>
public class Feed {
    /// <summary>
    /// Gets or sets the unique identifier of the feed.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns this feed.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the URL of the RSS/Atom feed.
    /// </summary>
    [JsonPropertyName("feed_url")]
    public string FeedUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the website associated with this feed.
    /// </summary>
    [JsonPropertyName("site_url")]
    public string SiteUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the title of the feed.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the feed was last checked for updates.
    /// </summary>
    [JsonPropertyName("checked_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime CheckedAt { get; set; }

    /// <summary>
    /// Gets or sets the ETag header value from the last HTTP response for caching purposes.
    /// </summary>
    [JsonPropertyName("etag_header")]
    public string EtagHeader { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Last-Modified header value from the last HTTP response for caching purposes.
    /// </summary>
    [JsonPropertyName("last_modified_header")]
    public string LastModifiedHeader { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error message from the last parsing attempt, if any.
    /// </summary>
    [JsonPropertyName("parsing_error_message")]
    public string ParsingErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of consecutive parsing errors encountered.
    /// </summary>
    [JsonPropertyName("parsing_error_count")]
    public int ParsingErrorCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the feed is disabled and should not be refreshed.
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to ignore HTTP caching headers when fetching the feed.
    /// </summary>
    [JsonPropertyName("ignore_http_cache")]
    public bool IgnoreHttpCache { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow self-signed SSL certificates when fetching the feed.
    /// </summary>
    [JsonPropertyName("allow_self_signed_certificates")]
    public bool AllowSelfSignedCertificates { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to fetch the feed through a proxy server.
    /// </summary>
    [JsonPropertyName("fetch_via_proxy")]
    public bool FetchViaProxy { get; set; }

    /// <summary>
    /// Gets or sets the custom scraper rules for content extraction from web pages.
    /// </summary>
    [JsonPropertyName("scraper_rules")]
    public string ScraperRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rewrite rules for modifying entry content.
    /// </summary>
    [JsonPropertyName("rewrite_rules")]
    public string RewriteRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL rewrite rules for modifying entry URLs.
    /// </summary>
    [JsonPropertyName("urlrewrite_rules")]
    public string UrlRewriteRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the blocklist rules for filtering out unwanted entries.
    /// </summary>
    [JsonPropertyName("blocklist_rules")]
    public string BlocklistRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the keeplist rules for preserving specific entries from removal.
    /// </summary>
    [JsonPropertyName("keeplist_rules")]
    public string KeeplistRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the block filter rules for excluding entries based on content patterns.
    /// </summary>
    [JsonPropertyName("block_filter_entry_rules")]
    public string BlockFilterEntryRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the keep filter rules for preserving entries based on content patterns.
    /// </summary>
    [JsonPropertyName("keep_filter_entry_rules")]
    public string KeepFilterEntryRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether to enable web crawler mode to fetch full content.
    /// </summary>
    [JsonPropertyName("crawler")]
    public bool Crawler { get; set; }

    /// <summary>
    /// Gets or sets the custom User-Agent string to use when fetching the feed.
    /// </summary>
    [JsonPropertyName("user_agent")]
    public string UserAgent { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the custom cookie string to send with HTTP requests.
    /// </summary>
    [JsonPropertyName("cookie")]
    public string Cookie { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username for HTTP basic authentication when fetching the feed.
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for HTTP basic authentication when fetching the feed.
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category this feed belongs to. Only populated when retrieving feeds with category information.
    /// </summary>
    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Category? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this feed is hidden from global entry views.
    /// </summary>
    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to disable HTTP/2 protocol when fetching the feed.
    /// </summary>
    [JsonPropertyName("disable_http2")]
    public bool DisableHttp2 { get; set; }

    /// <summary>
    /// Gets or sets the proxy URL to use when fetching this feed.
    /// </summary>
    [JsonPropertyName("proxy_url")]
    public string ProxyUrl { get; set; } = string.Empty;
}
