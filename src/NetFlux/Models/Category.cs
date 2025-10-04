// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class Category
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? UserId { get; set; }

    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }

    [JsonPropertyName("feed_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? FeedCount { get; set; }

    [JsonPropertyName("total_unread")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalUnread { get; set; }

    public override string ToString() => $"#{Id} {Title}";
}

public class CategoryCreateRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("hide_globally")]
    public bool HideGlobally { get; set; }
}

public class CategoryUpdateRequest
{
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonPropertyName("hide_globally")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HideGlobally { get; set; }
}