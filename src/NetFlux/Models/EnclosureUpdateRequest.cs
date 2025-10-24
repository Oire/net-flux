using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a request to update an enclosure's media progression.
/// </summary>
public class EnclosureUpdateRequest {
    /// <summary>
    /// Gets or sets the media progression in seconds for audio/video enclosures.
    /// </summary>
    [JsonPropertyName("media_progression")]
    public long MediaProgression { get; set; }
}
