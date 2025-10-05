// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxException: Exception {
    public MinifluxException() : base() {
    }

    public MinifluxException(string message) : base(message) {
    }

    public MinifluxException(string message, Exception innerException) : base(message, innerException) {
    }
}
