// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxNotFoundException: MinifluxException {
    public MinifluxNotFoundException() : base("The requested resource was not found.") {
    }

    public MinifluxNotFoundException(string message) : base(message) {
    }
}
