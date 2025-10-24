using System.Text.Json;
using System.Text.Json.Serialization;

namespace Oire.NetFlux.Helpers;

/// <summary>
/// A JSON converter that handles boolean to string conversions for the Miniflux API.
/// Converts boolean values to "1"/"0" strings and vice versa.
/// </summary>
public class BoolToStringConverter: JsonConverter<bool?> {
    /// <summary>
    /// Reads and converts a JSON value to a nullable boolean.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert to.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The converted boolean value, or null if the JSON value is null.</returns>
    public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType == JsonTokenType.String) {
            var value = reader.GetString();
            return value switch {
                "0" => false,
                "1" => true,
                _ => null
            };
        }

        if (reader.TokenType == JsonTokenType.True) {
            return true;
        }

        if (reader.TokenType == JsonTokenType.False) {
            return false;
        }

        return null;
    }

    /// <summary>
    /// Writes a nullable boolean value as a JSON string.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The boolean value to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options) {
        if (value is null) {
            writer.WriteNullValue();
        } else {
            writer.WriteStringValue(value.Value ? "1" : "0");
        }
    }

    /// <summary>
    /// Converts a nullable boolean to its string representation for query parameters.
    /// </summary>
    public static string? ToQueryString(bool? value) {
        return value.HasValue ? (value.Value ? "1" : "0") : null;
    }

    /// <summary>
    /// Converts a string to a nullable boolean.
    /// </summary>
    public static bool? FromString(string? value) {
        return value switch {
            "0" => false,
            "1" => true,
            "false" => false,
            "true" => true,
            _ => null
        };
    }
}
