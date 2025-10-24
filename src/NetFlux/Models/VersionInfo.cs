// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

using System.Text.Json.Serialization;

namespace Oire.NetFlux.Models;

/// <summary>
/// Represents version information about the Miniflux server instance.
/// </summary>
public class VersionInfo {
    /// <summary>
    /// Gets or sets the version number of the Miniflux server.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Git commit hash of the Miniflux server build.
    /// </summary>
    [JsonPropertyName("commit")]
    public string Commit { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the build date of the Miniflux server.
    /// </summary>
    [JsonPropertyName("build_date")]
    public string BuildDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Go programming language version used to build the server.
    /// </summary>
    [JsonPropertyName("go_version")]
    public string GoVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the compiler used to build the Miniflux server.
    /// </summary>
    [JsonPropertyName("compiler")]
    public string Compiler { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the CPU architecture of the Miniflux server (e.g., "amd64", "arm64").
    /// </summary>
    [JsonPropertyName("arch")]
    public string Arch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operating system of the Miniflux server (e.g., "linux", "darwin").
    /// </summary>
    [JsonPropertyName("os")]
    public string Os { get; set; } = string.Empty;
}
