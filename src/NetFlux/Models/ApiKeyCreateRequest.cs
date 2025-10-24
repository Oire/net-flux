// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to create a new API key in Miniflux.
/// </summary>
public class ApiKeyCreateRequest {
    /// <summary>
    /// Gets or sets the description for the API key to help identify its purpose.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }
}
