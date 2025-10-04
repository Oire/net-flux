// Copyright © 2025 Oire Software. All rights reserved.
// Licensed under the Apache License, Version 2.0

namespace Oire.NetFlux.Exceptions;

public class MinifluxConfigurationException: MinifluxException {
    public MinifluxConfigurationException() : base("Invalid configuration.") {
    }

    public MinifluxConfigurationException(string message) : base(message) {
    }
}
