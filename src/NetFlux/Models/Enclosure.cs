using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents a media enclosure (attachment) associated with a feed entry, such as audio, video, or image files.
/// </summary>
public class Enclosure {
    /// <summary>
    /// Gets or sets the unique identifier of the enclosure.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns this enclosure.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the entry this enclosure belongs to.
    /// </summary>
    [JsonPropertyName("entry_id")]
    public long EntryId { get; set; }

    /// <summary>
    /// Gets or sets the URL of the media file.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the MIME type of the media file (e.g., "audio/mpeg", "video/mp4").
    /// </summary>
    [JsonPropertyName("mime_type")]
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the size of the media file in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the media playback progression in milliseconds for tracking playback position.
    /// </summary>
    [JsonPropertyName("media_progression")]
    public long MediaProgression { get; set; }
}
