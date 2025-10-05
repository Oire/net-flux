// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class CategoryCreateRequest {
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }
}
