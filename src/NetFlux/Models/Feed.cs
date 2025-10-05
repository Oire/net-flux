// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class Feed {
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
