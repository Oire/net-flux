// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The exception that is thrown when the server returns a 400 Bad Request status code, indicating the request was malformed or invalid.
/// </summary>
public class MinifluxBadRequestException: MinifluxException {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxBadRequestException"/> class with a default error message.
    /// </summary>
    public MinifluxBadRequestException() : base("Bad request.") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxBadRequestException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the bad request error.</param>
    public MinifluxBadRequestException(string message) : base(message) {
    }
}
