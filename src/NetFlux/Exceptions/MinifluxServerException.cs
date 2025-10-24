// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The exception that is thrown when the server returns a 500 Internal Server Error status code, indicating a server-side error.
/// </summary>
public class MinifluxServerException: MinifluxException {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxServerException"/> class with a default error message.
    /// </summary>
    public MinifluxServerException() : base("Internal server error.") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxServerException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the server error.</param>
    public MinifluxServerException(string message) : base(message) {
    }
}
