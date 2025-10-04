// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class Feed
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("feed_url")]
    public string FeedUrl { get; set; } = string.Empty;

    [JsonPropertyName("site_url")]
    public string SiteUrl { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("checked_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime CheckedAt { get; set; }

    [JsonPropertyName("etag_header")]
    public string EtagHeader { get; set; } = string.Empty;

    [JsonPropertyName("last_modified_header")]
    public string LastModifiedHeader { get; set; } = string.Empty;

    [JsonPropertyName("parsing_error_message")]
    public string ParsingErrorMessage { get; set; } = string.Empty;

    [JsonPropertyName("parsing_error_count")]
    public int ParsingErrorCount { get; set; }

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    [JsonPropertyName("ignore_http_cache")]
    public bool IgnoreHttpCache { get; set; }

    [JsonPropertyName("allow_self_signed_certificates")]
    public bool AllowSelfSignedCertificates { get; set; }

    [JsonPropertyName("fetch_via_proxy")]
    public bool FetchViaProxy { get; set; }

    [JsonPropertyName("scraper_rules")]
    public string ScraperRules { get; set; } = string.Empty;

    [JsonPropertyName("rewrite_rules")]
    public string RewriteRules { get; set; } = string.Empty;

    [JsonPropertyName("urlrewrite_rules")]
    public string UrlRewriteRules { get; set; } = string.Empty;

    [JsonPropertyName("blocklist_rules")]
    public string BlocklistRules { get; set; } = string.Empty;

    [JsonPropertyName("keeplist_rules")]
    public string KeeplistRules { get; set; } = string.Empty;

    [JsonPropertyName("block_filter_entry_rules")]
    public string BlockFilterEntryRules { get; set; } = string.Empty;

    [JsonPropertyName("keep_filter_entry_rules")]
    public string KeepFilterEntryRules { get; set; } = string.Empty;

    [JsonPropertyName("crawler")]
    public bool Crawler { get; set; }

    [JsonPropertyName("user_agent")]
    public string UserAgent { get; set; } = string.Empty;

    [JsonPropertyName("cookie")]
    public string Cookie { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Category? Category { get; set; }

    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }

    [JsonPropertyName("disable_http2")]
    public bool DisableHttp2 { get; set; }

    [JsonPropertyName("proxy_url")]
    public string ProxyUrl { get; set; } = string.Empty;
}

public class FeedCreateRequest
{
    [JsonPropertyName("feed_url")]
    public required string FeedUrl { get; set; }

    [JsonPropertyName("category_id")]
    public long CategoryId { get; set; }

    [JsonPropertyName("user_agent")]
    public string? UserAgent { get; set; }

    [JsonPropertyName("cookie")]
    public string? Cookie { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("crawler")]
    public bool Crawler { get; set; }

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    [JsonPropertyName("ignore_http_cache")]
    public bool IgnoreHttpCache { get; set; }

    [JsonPropertyName("allow_self_signed_certificates")]
    public bool AllowSelfSignedCertificates { get; set; }

    [JsonPropertyName("fetch_via_proxy")]
    public bool FetchViaProxy { get; set; }

    [JsonPropertyName("scraper_rules")]
    public string? ScraperRules { get; set; }

    [JsonPropertyName("rewrite_rules")]
    public string? RewriteRules { get; set; }

    [JsonPropertyName("urlrewrite_rules")]
    public string? UrlRewriteRules { get; set; }

    [JsonPropertyName("blocklist_rules")]
    public string? BlocklistRules { get; set; }

    [JsonPropertyName("keeplist_rules")]
    public string? KeeplistRules { get; set; }

    [JsonPropertyName("block_filter_entry_rules")]
    public string? BlockFilterEntryRules { get; set; }

    [JsonPropertyName("keep_filter_entry_rules")]
    public string? KeepFilterEntryRules { get; set; }

    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }

    [JsonPropertyName("disable_http2")]
    public bool DisableHttp2 { get; set; }

    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; set; }
}

public class FeedUpdateRequest
{
    [JsonPropertyName("feed_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FeedUrl { get; set; }

    [JsonPropertyName("site_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SiteUrl { get; set; }

    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonPropertyName("scraper_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ScraperRules { get; set; }

    [JsonPropertyName("rewrite_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RewriteRules { get; set; }

    [JsonPropertyName("urlrewrite_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UrlRewriteRules { get; set; }

    [JsonPropertyName("blocklist_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BlocklistRules { get; set; }

    [JsonPropertyName("keeplist_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeeplistRules { get; set; }

    [JsonPropertyName("block_filter_entry_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BlockFilterEntryRules { get; set; }

    [JsonPropertyName("keep_filter_entry_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeepFilterEntryRules { get; set; }

    [JsonPropertyName("crawler")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Crawler { get; set; }

    [JsonPropertyName("user_agent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UserAgent { get; set; }

    [JsonPropertyName("cookie")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Cookie { get; set; }

    [JsonPropertyName("username")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }

    [JsonPropertyName("category_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? CategoryId { get; set; }

    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }

    [JsonPropertyName("ignore_http_cache")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IgnoreHttpCache { get; set; }

    [JsonPropertyName("allow_self_signed_certificates")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AllowSelfSignedCertificates { get; set; }

    [JsonPropertyName("fetch_via_proxy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FetchViaProxy { get; set; }

    [JsonPropertyName("hide_globally")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HideGlobally { get; set; }

    [JsonPropertyName("disable_http2")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableHttp2 { get; set; }

    [JsonPropertyName("proxy_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProxyUrl { get; set; }
}

public class FeedIcon
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("mime_type")]
    public string MimeType { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;
}

public class FeedCounters
{
    [JsonPropertyName("reads")]
    public Dictionary<long, int> ReadCounters { get; set; } = new();

    [JsonPropertyName("unreads")]
    public Dictionary<long, int> UnreadCounters { get; set; } = new();
}