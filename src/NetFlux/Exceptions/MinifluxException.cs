// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

/// <summary>
/// The base exception class for all Miniflux client-related errors.
/// </summary>
public class MinifluxException: Exception {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxException"/> class.
    /// </summary>
    public MinifluxException() : base() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public MinifluxException(string message) : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinifluxException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public MinifluxException(string message, Exception innerException) : base(message, innerException) {
    }
}
