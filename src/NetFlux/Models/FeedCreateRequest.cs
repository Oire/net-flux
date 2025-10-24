// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to create a new feed in Miniflux with all configuration options.
/// </summary>
public class FeedCreateRequest {
    /// <summary>
    /// Gets or sets the URL of the RSS/Atom feed to subscribe to.
    /// </summary>
    [JsonPropertyName("feed_url")]
    public required string FeedUrl { get; set; }

    /// <summary>
    /// Gets or sets the ID of the category to place this feed in.
    /// </summary>
    [JsonPropertyName("category_id")]
    public long CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the custom User-Agent string to use when fetching the feed.
    /// </summary>
    [JsonPropertyName("user_agent")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the custom cookie string to use when fetching the feed.
    /// </summary>
    [JsonPropertyName("cookie")]
    public string? Cookie { get; set; }

    /// <summary>
    /// Gets or sets the username for HTTP basic authentication when fetching the feed.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password for HTTP basic authentication when fetching the feed.
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to enable web crawling for this feed.
    /// </summary>
    [JsonPropertyName("crawler")]
    public bool Crawler { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the feed is disabled and should not be updated.
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to ignore HTTP cache when fetching the feed.
    /// </summary>
    [JsonPropertyName("ignore_http_cache")]
    public bool IgnoreHttpCache { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow self-signed SSL certificates when fetching the feed.
    /// </summary>
    [JsonPropertyName("allow_self_signed_certificates")]
    public bool AllowSelfSignedCertificates { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to fetch the feed via proxy.
    /// </summary>
    [JsonPropertyName("fetch_via_proxy")]
    public bool FetchViaProxy { get; set; }

    /// <summary>
    /// Gets or sets the scraper rules for extracting content from web pages.
    /// </summary>
    [JsonPropertyName("scraper_rules")]
    public string? ScraperRules { get; set; }

    /// <summary>
    /// Gets or sets the rewrite rules for modifying feed content.
    /// </summary>
    [JsonPropertyName("rewrite_rules")]
    public string? RewriteRules { get; set; }

    /// <summary>
    /// Gets or sets the URL rewrite rules for modifying feed URLs.
    /// </summary>
    [JsonPropertyName("urlrewrite_rules")]
    public string? UrlRewriteRules { get; set; }

    /// <summary>
    /// Gets or sets the blocklist rules for filtering out unwanted content.
    /// </summary>
    [JsonPropertyName("blocklist_rules")]
    public string? BlocklistRules { get; set; }

    /// <summary>
    /// Gets or sets the keeplist rules for ensuring specific content is retained.
    /// </summary>
    [JsonPropertyName("keeplist_rules")]
    public string? KeeplistRules { get; set; }

    /// <summary>
    /// Gets or sets the block filter entry rules for filtering entries.
    /// </summary>
    [JsonPropertyName("block_filter_entry_rules")]
    public string? BlockFilterEntryRules { get; set; }

    /// <summary>
    /// Gets or sets the keep filter entry rules for retaining specific entries.
    /// </summary>
    [JsonPropertyName("keep_filter_entry_rules")]
    public string? KeepFilterEntryRules { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the feed should be hidden globally.
    /// </summary>
    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to disable HTTP/2 when fetching the feed.
    /// </summary>
    [JsonPropertyName("disable_http2")]
    public bool DisableHttp2 { get; set; }

    /// <summary>
    /// Gets or sets the proxy URL to use when fetching the feed.
    /// </summary>
    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; set; }
}
