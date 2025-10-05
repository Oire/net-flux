using System.Text.Json;
using System.Text.Json.Serialization;

namespace Oire.NetFlux.Helpers;

public class BoolToStringConverter: JsonConverter<bool?> {
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
