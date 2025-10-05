using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

public class EnclosureUpdateRequest {
    [JsonPropertyName("media_progression")]
    public long MediaProgression { get; set; }
}
