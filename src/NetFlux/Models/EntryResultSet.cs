using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a paginated result set of entries returned from the Miniflux API.
/// </summary>
public class EntryResultSet {
    /// <summary>
    /// Gets or sets the total number of entries available (not just in this result set).
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Gets or sets the list of entries in this result set.
    /// </summary>
    [JsonPropertyName("entries")]
    public List<Entry> Entries { get; set; } = new();
}
