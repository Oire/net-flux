// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a category for organizing feeds in Miniflux.
/// </summary>
public class Category {
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the category.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the user who owns this category.
    /// </summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this category is hidden from global views.
    /// </summary>
    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }

    /// <summary>
    /// Gets or sets the number of feeds in this category. Only populated when retrieving categories with counters.
    /// </summary>
    [JsonPropertyName("feed_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? FeedCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of unread entries in this category. Only populated when retrieving categories with counters.
    /// </summary>
    [JsonPropertyName("total_unread")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalUnread { get; set; }

    /// <summary>
    /// Returns a string representation of the category.
    /// </summary>
    /// <returns>A string containing the category ID and title.</returns>
    public override string ToString() => $"#{Id} {Title}";
}
