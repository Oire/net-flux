using FluentAssertions;
using Oire.NetFlux.Exceptions;

namespace Oire.NetFlux.Tests.Exceptions;

public class ExceptionTests
{
    [Fact]
    public void MinifluxException_Should_Support_All_Constructors()
    {
        // Parameterless constructor
        var ex1 = new MinifluxException();
        ex1.Should().BeOfType<MinifluxException>();
        ex1.Message.Should().NotBeNullOrEmpty();

        // Message constructor
        var ex2 = new MinifluxException("Custom error message");
        ex2.Message.Should().Be("Custom error message");

        // Message and inner exception constructor
        var innerEx = new InvalidOperationException("Inner exception");
        var ex3 = new MinifluxException("Outer message", innerEx);
        ex3.Message.Should().Be("Outer message");
        ex3.InnerException.Should().BeSameAs(innerEx);
    }

    [Fact]
    public void MinifluxAuthenticationException_Should_Have_Default_Message()
    {
        // Default constructor
        var ex1 = new MinifluxAuthenticationException();
        ex1.Message.Should().Be("Authentication failed. Please check your credentials.");

        // Custom message
        var ex2 = new MinifluxAuthenticationException("Invalid API key");
        ex2.Message.Should().Be("Invalid API key");
    }

    [Fact]
    public void MinifluxBadRequestException_Should_Have_Default_Message()
    {
        // Default constructor
        var ex1 = new MinifluxBadRequestException();
        ex1.Message.Should().Be("Bad request.");

        // Custom message
        var ex2 = new MinifluxBadRequestException("Invalid feed URL");
        ex2.Message.Should().Be("Invalid feed URL");
    }

    [Fact]
    public void MinifluxConfigurationException_Should_Have_Default_Message()
    {
        // Default constructor
        var ex1 = new MinifluxConfigurationException();
        ex1.Message.Should().Be("Invalid configuration.");

        // Custom message
        var ex2 = new MinifluxConfigurationException("Missing base URL");
        ex2.Message.Should().Be("Missing base URL");
    }

    [Fact]
    public void MinifluxForbiddenException_Should_Have_Default_Message()
    {
        // Default constructor
        var ex1 = new MinifluxForbiddenException();
        ex1.Message.Should().Be("Access forbidden.");

        // Custom message
        var ex2 = new MinifluxForbiddenException("Admin access required");
        ex2.Message.Should().Be("Admin access required");
    }

    [Fact]
    public void MinifluxNotFoundException_Should_Have_Default_Message()
    {
        // Default constructor
        var ex1 = new MinifluxNotFoundException();
        ex1.Message.Should().Be("The requested resource was not found.");

        // Custom message
        var ex2 = new MinifluxNotFoundException("Feed not found");
        ex2.Message.Should().Be("Feed not found");
    }

    [Fact]
    public void MinifluxServerException_Should_Have_Default_Message()
    {
        // Default constructor
        var ex1 = new MinifluxServerException();
        ex1.Message.Should().Be("Internal server error.");

        // Custom message
        var ex2 = new MinifluxServerException("Database connection failed");
        ex2.Message.Should().Be("Database connection failed");
    }

    [Fact]
    public void All_Exceptions_Should_Derive_From_MinifluxException()
    {
        // Verify inheritance hierarchy
        var authEx = new MinifluxAuthenticationException();
        authEx.Should().BeAssignableTo<MinifluxException>();

        var badRequestEx = new MinifluxBadRequestException();
        badRequestEx.Should().BeAssignableTo<MinifluxException>();

        var configEx = new MinifluxConfigurationException();
        configEx.Should().BeAssignableTo<MinifluxException>();

        var forbiddenEx = new MinifluxForbiddenException();
        forbiddenEx.Should().BeAssignableTo<MinifluxException>();

        var notFoundEx = new MinifluxNotFoundException();
        notFoundEx.Should().BeAssignableTo<MinifluxException>();

        var serverEx = new MinifluxServerException();
        serverEx.Should().BeAssignableTo<MinifluxException>();
    }

    [Fact]
    public void Exception_Messages_Should_Be_Descriptive()
    {
        // Verify default messages are helpful
        var exceptions = new Dictionary<Type, string>
        {
            { typeof(MinifluxAuthenticationException), "Authentication failed. Please check your credentials." },
            { typeof(MinifluxBadRequestException), "Bad request." },
            { typeof(MinifluxConfigurationException), "Invalid configuration." },
            { typeof(MinifluxForbiddenException), "Access forbidden." },
            { typeof(MinifluxNotFoundException), "The requested resource was not found." },
            { typeof(MinifluxServerException), "Internal server error." }
        };

        foreach (var kvp in exceptions)
        {
            var instance = Activator.CreateInstance(kvp.Key) as Exception;
            instance.Should().NotBeNull();
            instance!.Message.Should().Be(kvp.Value);
        }
    }
}