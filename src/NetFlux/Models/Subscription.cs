// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a discovered feed subscription from URL discovery operations.
/// </summary>
public class Subscription {
    /// <summary>
    /// Gets or sets the title of the discovered feed.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the discovered feed.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the discovered feed (e.g., "rss", "atom", "json").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Returns a string representation of the subscription.
    /// </summary>
    /// <returns>A string containing the subscription title, URL, and type.</returns>
    public override string ToString() => $"Title=\"{Title}\", URL=\"{Url}\", Type=\"{Type}\"";
}
