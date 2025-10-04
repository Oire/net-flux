// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxServerException: MinifluxException {
    public MinifluxServerException() : base("Internal server error.") {
    }

    public MinifluxServerException(string message) : base(message) {
    }
}
