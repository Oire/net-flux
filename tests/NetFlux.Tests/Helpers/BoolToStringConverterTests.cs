using System.Text;
using System.Text.Json;
using FluentAssertions;
using Oire.NetFlux.Helpers;

namespace Oire.NetFlux.Tests.Helpers;

public class BoolToStringConverterTests {
    private readonly JsonSerializerOptions _jsonOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new BoolToStringConverter() }
    };

    [Theory]
    [InlineData(true, "\"1\"")]
    [InlineData(false, "\"0\"")]
    [InlineData(null, "null")]
    public void Should_Serialize_Boolean_To_String(bool? value, string expected) {
        // Arrange
        var obj = new { Value = value };

        // Act
        var json = JsonSerializer.Serialize(obj, _jsonOptions);

        // Assert
        json.Should().Contain($"\"value\":{expected}");
    }

    [Theory]
    [InlineData("\"1\"", true)]
    [InlineData("\"0\"", false)]
    [InlineData("null", null)]
    public void Should_Deserialize_String_To_Boolean(string jsonValue, bool? expected) {
        // Arrange
        var json = $"{{\"value\":{jsonValue}}}";

        // Act
        var obj = JsonSerializer.Deserialize<TestObject>(json, _jsonOptions);

        // Assert
        obj.Should().NotBeNull();
        obj!.Value.Should().Be(expected);
    }

    [Fact]
    public void Should_Handle_Direct_Write_And_Read() {
        // Arrange
        var converter = new BoolToStringConverter();
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);

        // Act - Write true
        converter.Write(writer, true, _jsonOptions);
        writer.Flush();
        var json = Encoding.UTF8.GetString(stream.ToArray());

        // Assert
        json.Should().Be("\"1\"");

        // Act - Read the value back
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("\"1\""));
        reader.Read(); // Move to the value
        var result = converter.Read(ref reader, typeof(bool?), _jsonOptions);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ToQueryString_Should_Convert_Correctly() {
        // Act & Assert
        BoolToStringConverter.ToQueryString(true).Should().Be("1");
        BoolToStringConverter.ToQueryString(false).Should().Be("0");
        BoolToStringConverter.ToQueryString(null).Should().BeNull();
    }

    [Fact]
    public void FromString_Should_Convert_Correctly() {
        // Act & Assert
        BoolToStringConverter.FromString("1").Should().BeTrue();
        BoolToStringConverter.FromString("0").Should().BeFalse();
        BoolToStringConverter.FromString(null).Should().BeNull();
        BoolToStringConverter.FromString("").Should().BeNull();
        BoolToStringConverter.FromString("invalid").Should().BeNull();
    }

    [Fact]
    public void Should_Only_Handle_Nullable_Boolean() {
        // Arrange
        var obj = new NonNullableTestObject { Value = true };

        // Act - The converter only handles nullable booleans, so non-nullable should serialize as normal
        var json = JsonSerializer.Serialize(obj, _jsonOptions);
        var deserialized = JsonSerializer.Deserialize<NonNullableTestObject>(json, _jsonOptions);

        // Assert
        json.Should().Contain("\"value\":true");
        deserialized.Should().NotBeNull();
        deserialized!.Value.Should().BeTrue();
    }

    [Fact]
    public void Should_Handle_Array_Of_Booleans() {
        // Arrange
        var obj = new { Values = new bool?[] { true, false, null, true } };

        // Act
        var json = JsonSerializer.Serialize(obj, _jsonOptions);

        // Assert
        json.Should().Contain("\"values\":[\"1\",\"0\",null,\"1\"]");
    }

    [Fact]
    public void Should_Handle_Invalid_Json_Value_As_Null() {
        // Arrange
        var json = "{\"value\":123}"; // Invalid: not a string or null

        // Act - The converter handles invalid values by returning null
        var result = JsonSerializer.Deserialize<TestObject>(json, _jsonOptions);

        // Assert
        result.Should().NotBeNull();
        result!.Value.Should().BeNull();
    }

    private sealed class TestObject {
        public bool? Value { get; set; }
    }

    private sealed class NonNullableTestObject {
        public bool Value { get; set; }
    }
}
