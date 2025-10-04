// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxAuthenticationException: MinifluxException {
    public MinifluxAuthenticationException() : base("Authentication failed. Please check your credentials.") {
    }

    public MinifluxAuthenticationException(string message) : base(message) {
    }
}
