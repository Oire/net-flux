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

public class MinifluxAuthenticationException: MinifluxException {
    public MinifluxAuthenticationException() : base("Authentication failed. Please check your credentials.") {
    }

    public MinifluxAuthenticationException(string message) : base(message) {
    }
}

public class MinifluxForbiddenException: MinifluxException {
    public MinifluxForbiddenException() : base("Access forbidden.") {
    }

    public MinifluxForbiddenException(string message) : base(message) {
    }
}

public class MinifluxNotFoundException: MinifluxException {
    public MinifluxNotFoundException() : base("The requested resource was not found.") {
    }

    public MinifluxNotFoundException(string message) : base(message) {
    }
}

public class MinifluxBadRequestException: MinifluxException {
    public MinifluxBadRequestException() : base("Bad request.") {
    }

    public MinifluxBadRequestException(string message) : base(message) {
    }
}

public class MinifluxServerException: MinifluxException {
    public MinifluxServerException() : base("Internal server error.") {
    }

    public MinifluxServerException(string message) : base(message) {
    }
}

public class MinifluxConfigurationException: MinifluxException {
    public MinifluxConfigurationException() : base("Invalid configuration.") {
    }

    public MinifluxConfigurationException(string message) : base(message) {
    }
}
