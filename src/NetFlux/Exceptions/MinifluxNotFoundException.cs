// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The exception that is thrown when the server returns a 404 Not Found status code, indicating the requested resource does not exist.
/// </summary>
public class MinifluxNotFoundException: MinifluxException {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxNotFoundException"/> class with a default error message.
    /// </summary>
    public MinifluxNotFoundException() : base("The requested resource was not found.") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the not found error.</param>
    public MinifluxNotFoundException(string message) : base(message) {
    }
}
