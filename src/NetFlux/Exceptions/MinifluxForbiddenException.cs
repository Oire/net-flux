// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The exception that is thrown when the server returns a 403 Forbidden status code, indicating access is denied.
/// </summary>
public class MinifluxForbiddenException: MinifluxException {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxForbiddenException"/> class with a default error message.
    /// </summary>
    public MinifluxForbiddenException() : base("Access forbidden.") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxForbiddenException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the forbidden access error.</param>
    public MinifluxForbiddenException(string message) : base(message) {
    }
}
