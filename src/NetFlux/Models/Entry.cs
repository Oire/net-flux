using System.Text.Json.Serialization;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Models;

public class Entry {
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
    [JsonConverter(typeof(CamelCaseJsonStringEnumConverter))]
    public EntryStatus Status { get; set; } = EntryStatus.Unread;

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
