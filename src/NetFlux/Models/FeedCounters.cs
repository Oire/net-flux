// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents read and unread entry counters for feeds, used for displaying count information in the UI.
/// </summary>
public class FeedCounters {
    /// <summary>
    /// Gets or sets a dictionary mapping feed IDs to the number of read entries in each feed.
    /// </summary>
    [JsonPropertyName("reads")]
    public Dictionary<long, int> ReadCounters { get; set; } = new();

    /// <summary>
    /// Gets or sets a dictionary mapping feed IDs to the number of unread entries in each feed.
    /// </summary>
    [JsonPropertyName("unreads")]
    public Dictionary<long, int> UnreadCounters { get; set; } = new();
}
