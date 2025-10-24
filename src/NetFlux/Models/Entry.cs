using System.Text.Json.Serialization;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a feed entry (article/post) in Miniflux with all its content and metadata.
/// </summary>
public class Entry {
    /// <summary>
    /// Gets or sets the unique identifier of the entry.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the publication date and time of the entry.
    /// </summary>
    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entry was last modified.
    /// </summary>
    [JsonPropertyName("changed_at")]
    public DateTime ChangedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entry was first created in Miniflux.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the feed this entry belongs to. Only populated when retrieving entries with feed information.
    /// </summary>
    [JsonPropertyName("feed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Feed? Feed { get; set; }

    /// <summary>
    /// Gets or sets the unique hash of the entry content used for duplicate detection.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the original entry on the source website.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL to the comments section for this entry.
    /// </summary>
    [JsonPropertyName("comments_url")]
    public string CommentsUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the title of the entry.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the read status of the entry (Unread, Read, or Removed).
    /// </summary>
    [JsonPropertyName("status")]
    [JsonConverter(typeof(CamelCaseJsonStringEnumConverter))]
    public EntryStatus Status { get; set; } = EntryStatus.Unread;

    /// <summary>
    /// Gets or sets the HTML content of the entry.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author name of the entry.
    /// </summary>
    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique share code for publicly sharing this entry.
    /// </summary>
    [JsonPropertyName("share_code")]
    public string ShareCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of media enclosures (attachments) associated with this entry.
    /// </summary>
    [JsonPropertyName("enclosures")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Enclosure>? Enclosures { get; set; }

    /// <summary>
    /// Gets or sets the list of tags associated with this entry.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Gets or sets the estimated reading time in minutes for this entry.
    /// </summary>
    [JsonPropertyName("reading_time")]
    public int ReadingTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns this entry.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the feed this entry belongs to.
    /// </summary>
    [JsonPropertyName("feed_id")]
    public long FeedId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this entry is starred (bookmarked) by the user.
    /// </summary>
    [JsonPropertyName("starred")]
    public bool Starred { get; set; }
}
