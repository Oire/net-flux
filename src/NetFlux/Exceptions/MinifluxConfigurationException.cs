// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The exception that is thrown when there is an invalid configuration or setup error with the Miniflux client.
/// </summary>
public class MinifluxConfigurationException: MinifluxException {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxConfigurationException"/> class with a default error message.
    /// </summary>
    public MinifluxConfigurationException() : base("Invalid configuration.") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxConfigurationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the configuration error.</param>
    public MinifluxConfigurationException(string message) : base(message) {
    }
}
