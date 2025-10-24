using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Oire.NetFlux.Helpers;

/// <summary>
/// A JSON converter factory that creates converters for enum types that handle camelCase JSON serialization.
/// </summary>
public class CamelCaseJsonStringEnumConverter: JsonConverterFactory {
    /// <summary>
    /// Determines whether the specified type can be converted by this converter factory.
    /// </summary>
    /// <param name="typeToConvert">The type to test for conversion support.</param>
    /// <returns>true if the type is an enum or nullable enum; otherwise, false.</returns>
    public override bool CanConvert(Type typeToConvert) {
        return typeToConvert.IsEnum || (Nullable.GetUnderlyingType(typeToConvert)?.IsEnum ?? false);
    }

    /// <summary>
    /// Creates a JsonConverter for the specified enum type.
    /// </summary>
    /// <param name="typeToConvert">The enum type to create a converter for.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>A JsonConverter instance for the specified enum type.</returns>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
        var enumType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;
        var converterType = typeof(CamelCaseEnumConverter<>).MakeGenericType(enumType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

/// <summary>
/// Internal converter that handles the actual enum to camelCase string conversion.
/// </summary>
/// <typeparam name="T">The enum type to convert.</typeparam>
internal sealed class CamelCaseEnumConverter<T>: JsonConverter<T> where T : struct, Enum {
    /// <summary>
    /// Reads and converts a JSON string to an enum value, handling both camelCase and PascalCase input.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The enum type to convert to.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The converted enum value.</returns>
    /// <exception cref="JsonException">Thrown when the string cannot be converted to the enum type.</exception>
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        var stringValue = reader.GetString();
        if (string.IsNullOrEmpty(stringValue))
            throw new JsonException($"Unable to convert null or empty string to {typeof(T)}");

        // Try exact match first
        if (Enum.TryParse<T>(stringValue, true, out var result))
            return result;

        // Try converting from camelCase to PascalCase
        var pascalCase = char.ToUpperInvariant(stringValue[0]) + stringValue.Substring(1);
        if (Enum.TryParse<T>(pascalCase, true, out result))
            return result;

        throw new JsonException($"Unable to convert \"{stringValue}\" to {typeof(T)}");
    }

    /// <summary>
    /// Writes an enum value as a camelCase JSON string.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The enum value to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {
        var stringValue = value.ToString();
        var camelCase = char.ToLowerInvariant(stringValue[0]) + stringValue.Substring(1);
        writer.WriteStringValue(camelCase);
    }
}
