// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class FeedCreateRequest {
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
