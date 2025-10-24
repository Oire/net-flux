// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a feed's favicon or icon image data.
/// </summary>
public class FeedIcon {
    /// <summary>
    /// Gets or sets the unique identifier of the feed icon.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the MIME type of the icon image (e.g., "image/png", "image/ico").
    /// </summary>
    [JsonPropertyName("mime_type")]
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base64-encoded image data of the feed icon.
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;
}
