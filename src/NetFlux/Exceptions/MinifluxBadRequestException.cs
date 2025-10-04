// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxBadRequestException: MinifluxException {
    public MinifluxBadRequestException() : base("Bad request.") {
    }

    public MinifluxBadRequestException(string message) : base(message) {
    }
}
