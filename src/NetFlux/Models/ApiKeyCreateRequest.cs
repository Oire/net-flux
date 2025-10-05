// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class ApiKeyCreateRequest {
    [JsonPropertyName("description")]
    public required string Description { get; set; }
}
