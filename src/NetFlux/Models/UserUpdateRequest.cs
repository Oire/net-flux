// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to update an existing user in Miniflux. All properties are optional.
/// </summary>
public class UserUpdateRequest {
    /// <summary>
    /// Gets or sets the username for the user.
    /// </summary>
    [JsonPropertyName("username")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user should have administrator privileges.
    /// </summary>
    [JsonPropertyName("is_admin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets the UI theme preference for the user.
    /// </summary>
    [JsonPropertyName("theme")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Theme { get; set; }

    /// <summary>
    /// Gets or sets the language preference for the user.
    /// </summary>
    [JsonPropertyName("language")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Language { get; set; }

    /// <summary>
    /// Gets or sets the timezone preference for the user.
    /// </summary>
    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; set; }

    /// <summary>
    /// Gets or sets the entry sorting direction preference (e.g., "asc" or "desc").
    /// </summary>
    [JsonPropertyName("entry_sorting_direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EntrySortingDirection { get; set; }

    /// <summary>
    /// Gets or sets the entry sorting order preference (e.g., "published_at", "title").
    /// </summary>
    [JsonPropertyName("entry_sorting_order")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EntrySortingOrder { get; set; }

    /// <summary>
    /// Gets or sets the custom CSS stylesheet for the user.
    /// </summary>
    [JsonPropertyName("stylesheet")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Stylesheet { get; set; }

    /// <summary>
    /// Gets or sets the custom JavaScript code for the user.
    /// </summary>
    [JsonPropertyName("custom_js")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CustomJs { get; set; }

    /// <summary>
    /// Gets or sets the Google ID for the user, if using Google authentication.
    /// </summary>
    [JsonPropertyName("google_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GoogleId { get; set; }

    /// <summary>
    /// Gets or sets the OpenID Connect ID for the user, if using OpenID Connect authentication.
    /// </summary>
    [JsonPropertyName("openid_connect_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OpenIdConnectId { get; set; }

    /// <summary>
    /// Gets or sets the number of entries to display per page.
    /// </summary>
    [JsonPropertyName("entries_per_page")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EntriesPerPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether keyboard shortcuts are enabled.
    /// </summary>
    [JsonPropertyName("keyboard_shortcuts")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? KeyboardShortcuts { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show reading time estimates.
    /// </summary>
    [JsonPropertyName("show_reading_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowReadingTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether entry swipe gestures are enabled.
    /// </summary>
    [JsonPropertyName("entry_swipe")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EntrySwipe { get; set; }

    /// <summary>
    /// Gets or sets the gesture navigation preference.
    /// </summary>
    [JsonPropertyName("gesture_nav")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GestureNav { get; set; }

    /// <summary>
    /// Gets or sets the display mode preference for the user interface.
    /// </summary>
    [JsonPropertyName("display_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DisplayMode { get; set; }

    /// <summary>
    /// Gets or sets the default reading speed in words per minute for calculating reading time.
    /// </summary>
    [JsonPropertyName("default_reading_speed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DefaultReadingSpeed { get; set; }

    /// <summary>
    /// Gets or sets the reading speed for CJK (Chinese, Japanese, Korean) languages in characters per minute.
    /// </summary>
    [JsonPropertyName("cjk_reading_speed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CjkReadingSpeed { get; set; }

    /// <summary>
    /// Gets or sets the default home page for the user.
    /// </summary>
    [JsonPropertyName("default_home_page")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DefaultHomePage { get; set; }

    /// <summary>
    /// Gets or sets the sorting order for categories.
    /// </summary>
    [JsonPropertyName("categories_sorting_order")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CategoriesSortingOrder { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether entries should be marked as read when viewed.
    /// </summary>
    [JsonPropertyName("mark_read_on_view")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MarkReadOnView { get; set; }

    /// <summary>
    /// Gets or sets the media playback rate for audio and video content.
    /// </summary>
    [JsonPropertyName("media_playback_rate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MediaPlaybackRate { get; set; }

    /// <summary>
    /// Gets or sets the block filter entry rules for filtering entries.
    /// </summary>
    [JsonPropertyName("block_filter_entry_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BlockFilterEntryRules { get; set; }

    /// <summary>
    /// Gets or sets the keep filter entry rules for retaining specific entries.
    /// </summary>
    [JsonPropertyName("keep_filter_entry_rules")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeepFilterEntryRules { get; set; }

    /// <summary>
    /// Gets or sets the allowed external font hosts for web fonts.
    /// </summary>
    [JsonPropertyName("external_font_hosts")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalFontHosts { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether external links should always be opened.
    /// </summary>
    [JsonPropertyName("always_open_external_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AlwaysOpenExternalLinks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether external links should be opened in a new tab.
    /// </summary>
    [JsonPropertyName("open_external_links_in_new_tab")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? OpenExternalLinksInNewTab { get; set; }
}
