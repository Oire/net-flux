using System.Text.Json.Serialization;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents the read status of a feed entry.
/// </summary>
[JsonConverter(typeof(CamelCaseJsonStringEnumConverter))]
public enum EntryStatus {
    /// <summary>
    /// The entry has not been read by the user.
    /// </summary>
    Unread,
    /// <summary>
    /// The entry has been read by the user.
    /// </summary>
    Read,
    /// <summary>
    /// The entry has been removed/deleted.
    /// </summary>
    Removed
}
