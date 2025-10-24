using System.Text.Json.Serialization;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents filtering and pagination options for retrieving entries from the Miniflux API.
/// </summary>
public class EntryFilter {
    /// <summary>
    /// Gets or sets the entry status to filter by (Unread, Read, or Removed).
    /// </summary>
    public EntryStatus? Status { get; set; }
    /// <summary>
    /// Gets or sets the number of entries to skip for pagination.
    /// </summary>
    public int? Offset { get; set; }
    /// <summary>
    /// Gets or sets the maximum number of entries to return.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the field to order results by (e.g., "published_at", "created_at").
    /// </summary>
    public string? Order { get; set; }
    /// <summary>
    /// Gets or sets the sorting direction ("asc" for ascending, "desc" for descending).
    /// </summary>
    public string? Direction { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to filter by starred entries only.
    /// </summary>
    [JsonConverter(typeof(BoolToStringConverter))]
    public bool? Starred { get; set; }
    /// <summary>
    /// Gets or sets the timestamp to filter entries created before this Unix timestamp.
    /// </summary>
    public long? Before { get; set; }
    /// <summary>
    /// Gets or sets the timestamp to filter entries created after this Unix timestamp.
    /// </summary>
    public long? After { get; set; }
    /// <summary>
    /// Gets or sets the timestamp to filter entries published before this Unix timestamp.
    /// </summary>
    public long? PublishedBefore { get; set; }
    /// <summary>
    /// Gets or sets the timestamp to filter entries published after this Unix timestamp.
    /// </summary>
    public long? PublishedAfter { get; set; }
    /// <summary>
    /// Gets or sets the timestamp to filter entries last changed before this Unix timestamp.
    /// </summary>
    public long? ChangedBefore { get; set; }
    /// <summary>
    /// Gets or sets the timestamp to filter entries last changed after this Unix timestamp.
    /// </summary>
    public long? ChangedAfter { get; set; }
    /// <summary>
    /// Gets or sets the entry ID to filter entries created before this entry ID.
    /// </summary>
    public long? BeforeEntryId { get; set; }
    /// <summary>
    /// Gets or sets the entry ID to filter entries created after this entry ID.
    /// </summary>
    public long? AfterEntryId { get; set; }
    /// <summary>
    /// Gets or sets the search query to filter entries by title or content.
    /// </summary>
    public string? Search { get; set; }
    /// <summary>
    /// Gets or sets the category ID to filter entries by category.
    /// </summary>
    public long? CategoryId { get; set; }
    /// <summary>
    /// Gets or sets the feed ID to filter entries by feed.
    /// </summary>
    public long? FeedId { get; set; }
    /// <summary>
    /// Gets or sets the list of entry statuses to filter by (alternative to single Status).
    /// </summary>
    public List<EntryStatus>? Statuses { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether to include globally visible entries only.
    /// </summary>
    public bool GloballyVisible { get; set; }
}
