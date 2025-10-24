// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to update an existing category in Miniflux. All properties are optional.
/// </summary>
public class CategoryUpdateRequest {
    /// <summary>
    /// Gets or sets the title of the category.
    /// </summary>
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category should be hidden globally.
    /// </summary>
    [JsonPropertyName("hide_globally")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HideGlobally { get; set; }
}
