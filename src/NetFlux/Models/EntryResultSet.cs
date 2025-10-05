using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class EntryResultSet {
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("entries")]
    public List<Entry> Entries { get; set; } = new();
}
