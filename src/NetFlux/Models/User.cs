// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a Miniflux user account with all associated settings and preferences.
/// </summary>
public class User {
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's password. Only used for user creation or update operations.
    /// </summary>
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has administrative privileges.
    /// </summary>
    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets the UI theme (e.g., "light", "dark", "auto").
    /// </summary>
    [JsonPropertyName("theme")]
    public string Theme { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user interface language code (e.g., "en_US", "fr_FR").
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's timezone (e.g., "America/New_York", "Europe/Paris").
    /// </summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sorting direction for entries ("asc" or "desc").
    /// </summary>
    [JsonPropertyName("entry_sorting_direction")]
    public string EntrySortingDirection { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sorting order for entries (e.g., "published_at", "created_at").
    /// </summary>
    [JsonPropertyName("entry_sorting_order")]
    public string EntrySortingOrder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets custom CSS stylesheet for the user interface.
    /// </summary>
    [JsonPropertyName("stylesheet")]
    public string Stylesheet { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets custom JavaScript code to inject into the UI.
    /// </summary>
    [JsonPropertyName("custom_js")]
    public string CustomJs { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Google account identifier for OAuth authentication.
    /// </summary>
    [JsonPropertyName("google_id")]
    public string GoogleId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the OpenID Connect identifier for SSO authentication.
    /// </summary>
    [JsonPropertyName("openid_connect_id")]
    public string OpenIdConnectId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of entries to display per page.
    /// </summary>
    [JsonPropertyName("entries_per_page")]
    public int EntriesPerPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether keyboard shortcuts are enabled.
    /// </summary>
    [JsonPropertyName("keyboard_shortcuts")]
    public bool KeyboardShortcuts { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show estimated reading time for entries.
    /// </summary>
    [JsonPropertyName("show_reading_time")]
    public bool ShowReadingTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether swipe gestures are enabled for entries.
    /// </summary>
    [JsonPropertyName("entry_swipe")]
    public bool EntrySwipe { get; set; }

    /// <summary>
    /// Gets or sets the gesture navigation mode.
    /// </summary>
    [JsonPropertyName("gesture_nav")]
    public string GestureNav { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp of the user's last login.
    /// </summary>
    [JsonPropertyName("last_login_at")]
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Gets or sets the display mode for entries (e.g., "standalone", "fullscreen").
    /// </summary>
    [JsonPropertyName("display_mode")]
    public string DisplayMode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the default reading speed in words per minute for Latin scripts.
    /// </summary>
    [JsonPropertyName("default_reading_speed")]
    public int DefaultReadingSpeed { get; set; }

    /// <summary>
    /// Gets or sets the reading speed in characters per minute for CJK (Chinese, Japanese, Korean) scripts.
    /// </summary>
    [JsonPropertyName("cjk_reading_speed")]
    public int CjkReadingSpeed { get; set; }

    /// <summary>
    /// Gets or sets the default home page to display on login.
    /// </summary>
    [JsonPropertyName("default_home_page")]
    public string DefaultHomePage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sorting order for categories.
    /// </summary>
    [JsonPropertyName("categories_sorting_order")]
    public string CategoriesSortingOrder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether to automatically mark entries as read when viewed.
    /// </summary>
    [JsonPropertyName("mark_read_on_view")]
    public bool MarkReadOnView { get; set; }

    /// <summary>
    /// Gets or sets the media playback rate multiplier (e.g., 1.0 for normal speed, 2.0 for 2x speed).
    /// </summary>
    [JsonPropertyName("media_playback_rate")]
    public double MediaPlaybackRate { get; set; }

    /// <summary>
    /// Gets or sets the filter rules for blocking entries (regex patterns, one per line).
    /// </summary>
    [JsonPropertyName("block_filter_entry_rules")]
    public string BlockFilterEntryRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filter rules for keeping entries (regex patterns, one per line).
    /// </summary>
    [JsonPropertyName("keep_filter_entry_rules")]
    public string KeepFilterEntryRules { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the allowed external font hosts for custom fonts.
    /// </summary>
    [JsonPropertyName("external_font_hosts")]
    public string ExternalFontHosts { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether to always open external links directly.
    /// </summary>
    [JsonPropertyName("always_open_external_links")]
    public bool AlwaysOpenExternalLinks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to open external links in a new browser tab.
    /// </summary>
    [JsonPropertyName("open_external_links_in_new_tab")]
    public bool OpenExternalLinksInNewTab { get; set; }

    /// <summary>
    /// Returns a string representation of the user.
    /// </summary>
    /// <returns>A string containing the user ID, username, and admin status.</returns>
    public override string ToString() => $"#{Id} - {Username} (admin={IsAdmin})";
}
