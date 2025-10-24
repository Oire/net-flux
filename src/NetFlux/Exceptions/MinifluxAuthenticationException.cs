// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The exception that is thrown when authentication with the Miniflux server fails.
/// </summary>
public class MinifluxAuthenticationException: MinifluxException {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxAuthenticationException"/> class with a default error message.
    /// </summary>
    public MinifluxAuthenticationException() : base("Authentication failed. Please check your credentials.") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxAuthenticationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the authentication error.</param>
    public MinifluxAuthenticationException(string message) : base(message) {
    }
}
