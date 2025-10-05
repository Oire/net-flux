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
        var converterType = typeof(JsonStringEnumConverter);
        // Use default JsonStringEnumConverter but with camelCase naming policy
        var namingPolicy = JsonNamingPolicy.CamelCase;

        return (JsonConverter)Activator.CreateInstance(typeof(JsonStringEnumConverter), new object[] { namingPolicy, false })!;
    }
}
