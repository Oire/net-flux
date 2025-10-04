// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public static class EntryStatus
{
    public const string Unread = "unread";
    public const string Read = "read";
    public const string Removed = "removed";
}

public class Entry
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonPropertyName("changed_at")]
    public DateTime ChangedAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("feed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Feed? Feed { get; set; }

    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("comments_url")]
    public string CommentsUrl { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = EntryStatus.Unread;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    [JsonPropertyName("share_code")]
    public string ShareCode { get; set; } = string.Empty;

    [JsonPropertyName("enclosures")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Enclosure>? Enclosures { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("reading_time")]
    public int ReadingTime { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("feed_id")]
    public long FeedId { get; set; }

    [JsonPropertyName("starred")]
    public bool Starred { get; set; }
}

public class EntryUpdateRequest
{
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Content { get; set; }
}

public class Enclosure
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("entry_id")]
    public long EntryId { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("mime_type")]
    public string MimeType { get; set; } = string.Empty;

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("media_progression")]
    public long MediaProgression { get; set; }
}

public class EnclosureUpdateRequest
{
    [JsonPropertyName("media_progression")]
    public long MediaProgression { get; set; }
}

public class EntryFilter
{
    public string? Status { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? Order { get; set; }
    public string? Direction { get; set; }
    public string? Starred { get; set; }
    public long? Before { get; set; }
    public long? After { get; set; }
    public long? PublishedBefore { get; set; }
    public long? PublishedAfter { get; set; }
    public long? ChangedBefore { get; set; }
    public long? ChangedAfter { get; set; }
    public long? BeforeEntryId { get; set; }
    public long? AfterEntryId { get; set; }
    public string? Search { get; set; }
    public long? CategoryId { get; set; }
    public long? FeedId { get; set; }
    public List<string>? Statuses { get; set; }
    public bool GloballyVisible { get; set; }
}

public static class EntryFilterConstants
{
    public const string FilterNotStarred = "0";
    public const string FilterOnlyStarred = "1";
}

public class EntryResultSet
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("entries")]
    public List<Entry> Entries { get; set; } = new();
}