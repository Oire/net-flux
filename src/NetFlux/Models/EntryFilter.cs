using System.Text.Json.Serialization;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Models;

public class EntryFilter {
    public EntryStatus? Status { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? Order { get; set; }
    public string? Direction { get; set; }

    [JsonConverter(typeof(BoolToStringConverter))]
    public bool? Starred { get; set; }
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
    public List<EntryStatus>? Statuses { get; set; }
    public bool GloballyVisible { get; set; }
}
