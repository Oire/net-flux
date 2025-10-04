// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class User
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("theme")]
    public string Theme { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    [JsonPropertyName("entry_sorting_direction")]
    public string EntrySortingDirection { get; set; } = string.Empty;

    [JsonPropertyName("entry_sorting_order")]
    public string EntrySortingOrder { get; set; } = string.Empty;

    [JsonPropertyName("stylesheet")]
    public string Stylesheet { get; set; } = string.Empty;

    [JsonPropertyName("custom_js")]
    public string CustomJs { get; set; } = string.Empty;

    [JsonPropertyName("google_id")]
    public string GoogleId { get; set; } = string.Empty;

    [JsonPropertyName("openid_connect_id")]
    public string OpenIdConnectId { get; set; } = string.Empty;

    [JsonPropertyName("entries_per_page")]
    public int EntriesPerPage { get; set; }

    [JsonPropertyName("keyboard_shortcuts")]
    public bool KeyboardShortcuts { get; set; }

    [JsonPropertyName("show_reading_time")]
    public bool ShowReadingTime { get; set; }

    [JsonPropertyName("entry_swipe")]
    public bool EntrySwipe { get; set; }

    [JsonPropertyName("gesture_nav")]
    public string GestureNav { get; set; } = string.Empty;

    [JsonPropertyName("last_login_at")]
    public DateTime? LastLoginAt { get; set; }

    [JsonPropertyName("display_mode")]
    public string DisplayMode { get; set; } = string.Empty;

    [JsonPropertyName("default_reading_speed")]
    public int DefaultReadingSpeed { get; set; }

    [JsonPropertyName("cjk_reading_speed")]
    public int CjkReadingSpeed { get; set; }

    [JsonPropertyName("default_home_page")]
    public string DefaultHomePage { get; set; } = string.Empty;

    [JsonPropertyName("categories_sorting_order")]
    public string CategoriesSortingOrder { get; set; } = string.Empty;

    [JsonPropertyName("mark_read_on_view")]
    public bool MarkReadOnView { get; set; }

    [JsonPropertyName("media_playback_rate")]
    public double MediaPlaybackRate { get; set; }

    [JsonPropertyName("block_filter_entry_rules")]
    public string BlockFilterEntryRules { get; set; } = string.Empty;

    [JsonPropertyName("keep_filter_entry_rules")]
    public string KeepFilterEntryRules { get; set; } = string.Empty;

    [JsonPropertyName("external_font_hosts")]
    public string ExternalFontHosts { get; set; } = string.Empty;

    [JsonPropertyName("always_open_external_links")]
    public bool AlwaysOpenExternalLinks { get; set; }

    [JsonPropertyName("open_external_links_in_new_tab")]
    public bool OpenExternalLinksInNewTab { get; set; }

    public override string ToString() => $"#{Id} - {Username} (admin={IsAdmin})";
}

public class UserCreateRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("google_id")]
    public string? GoogleId { get; set; }

    [JsonPropertyName("openid_connect_id")]
    public string? OpenIdConnectId { get; set; }
}

public class UserUpdateRequest
{
    [JsonPropertyName("username")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }

    [JsonPropertyName("is_admin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsAdmin { get; set; }

    [JsonPropertyName("theme")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Theme { get; set; }

    [JsonPropertyName("language")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Language { get; set; }

    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; set; }

    [JsonPropertyName("entry_sorting_direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EntrySortingDirection { get; set; }

    [JsonPropertyName("entry_sorting_order")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EntrySortingOrder { get; set; }

    [JsonPropertyName("stylesheet")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Stylesheet { get; set; }

    [JsonPropertyName("custom_js")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CustomJs { get; set; }

    [JsonPropertyName("google_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GoogleId { get; set; }

    [JsonPropertyName("openid_connect_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OpenIdConnectId { get; set; }

    [JsonPropertyName("entries_per_page")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EntriesPerPage { get; set; }

    [JsonPropertyName("keyboard_shortcuts")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? KeyboardShortcuts { get; set; }

    [JsonPropertyName("show_reading_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowReadingTime { get; set; }

    [JsonPropertyName("entry_swipe")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EntrySwipe { get; set; }

    [JsonPropertyName("gesture_nav")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GestureNav { get; set; }

    [JsonPropertyName("display_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DisplayMode { get; set; }

    [JsonPropertyName("default_reading_speed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DefaultReadingSpeed { get; set; }

    [JsonPropertyName("cjk_reading_speed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CjkReadingSpeed { get; set; }

    [JsonPropertyName("default_home_page")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DefaultHomePage { get; set; }

    [JsonPropertyName("categories_sorting_order")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CategoriesSortingOrder { get; set; }

    [JsonPropertyName("mark_read_on_view")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MarkReadOnView { get; set; }

    [JsonPropertyName("media_playback_rate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MediaPlaybackRate { get; set; }

    [JsonPropertyName("block_filter_entry_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BlockFilterEntryRules { get; set; }

    [JsonPropertyName("keep_filter_entry_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeepFilterEntryRules { get; set; }

    [JsonPropertyName("external_font_hosts")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalFontHosts { get; set; }

    [JsonPropertyName("always_open_external_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AlwaysOpenExternalLinks { get; set; }

    [JsonPropertyName("open_external_links_in_new_tab")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? OpenExternalLinksInNewTab { get; set; }
}