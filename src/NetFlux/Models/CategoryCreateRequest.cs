// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to create a new category in Miniflux.
/// </summary>
public class CategoryCreateRequest {
    /// <summary>
    /// Gets or sets the title of the category.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category should be hidden globally.
    /// </summary>
    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }
}
