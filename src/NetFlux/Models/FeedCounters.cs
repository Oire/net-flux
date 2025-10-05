// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class FeedCounters {
    [JsonPropertyName("reads")]
    public Dictionary<long, int> ReadCounters { get; set; } = new();

    [JsonPropertyName("unreads")]
    public Dictionary<long, int> UnreadCounters { get; set; } = new();
}
