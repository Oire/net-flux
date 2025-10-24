// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to create a new user in Miniflux.
/// </summary>
public class UserCreateRequest {
    /// <summary>
    /// Gets or sets the username for the new user.
    /// </summary>
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the password for the new user.
    /// </summary>
    [JsonPropertyName("password")]
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the new user should have administrator privileges.
    /// </summary>
    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets the Google ID for the user, if using Google authentication.
    /// </summary>
    [JsonPropertyName("google_id")]
    public string? GoogleId { get; set; }

    /// <summary>
    /// Gets or sets the OpenID Connect ID for the user, if using OpenID Connect authentication.
    /// </summary>
    [JsonPropertyName("openid_connect_id")]
    public string? OpenIdConnectId { get; set; }
}
