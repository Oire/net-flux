// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents an API key for authenticating with the Miniflux API.
/// </summary>
public class ApiKey {
    /// <summary>
    /// Gets or sets the unique identifier of the API key.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns this API key.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the API token value used for authentication.
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the API key for identification purposes.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when this API key was last used for authentication.
    /// </summary>
    [JsonPropertyName("last_used_at")]
    public DateTime? LastUsedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this API key was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
