using System.Text.Json.Serialization;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Models;

[JsonConverter(typeof(CamelCaseJsonStringEnumConverter))]
public enum EntryStatus {
    Unread,
    Read,
    Removed
}
