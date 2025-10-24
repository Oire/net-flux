using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Oire.NetFlux.Helpers;

public class CamelCaseJsonStringEnumConverter: JsonConverterFactory {
    public override bool CanConvert(Type typeToConvert) {
        return typeToConvert.IsEnum || (Nullable.GetUnderlyingType(typeToConvert)?.IsEnum ?? false);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
        var enumType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;
        var converterType = typeof(CamelCaseEnumConverter<>).MakeGenericType(enumType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

internal sealed class CamelCaseEnumConverter<T>: JsonConverter<T> where T : struct, Enum {
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

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {
        var stringValue = value.ToString();
        var camelCase = char.ToLowerInvariant(stringValue[0]) + stringValue.Substring(1);
        writer.WriteStringValue(camelCase);
    }
}
