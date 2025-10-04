// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxForbiddenException: MinifluxException {
    public MinifluxForbiddenException() : base("Access forbidden.") {
    }

    public MinifluxForbiddenException(string message) : base(message) {
    }
}
