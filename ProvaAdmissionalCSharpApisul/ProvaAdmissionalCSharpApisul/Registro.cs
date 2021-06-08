using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProvaAdmissionalCSharpApisul
{
    /// <summary>
    /// Entrada class
    /// </summary>
    public partial class Entrada
    {
        [JsonProperty("andar")]
        public long Andar { get; set; }

        [JsonProperty("elevador")]
        public string Elevador { get; set; }

        [JsonProperty("turno")]
        public Turno Turno { get; set; }
    }

    /// <summary>
    /// Enum type Turno
    /// </summary>
    public enum Turno { M, N, V };

    /// <summary>
    /// Converter
    /// </summary>
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TurnoConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TurnoConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(Turno) || t == typeof(Turno?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "M":
                    return Turno.M;
                case "N":
                    return Turno.N;
                case "V":
                    return Turno.V;
            }
            throw new Exception("Cannot unmarshal type Turno");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Turno)untypedValue;
            switch (value)
            {
                case Turno.M:
                    serializer.Serialize(writer, "M");
                    return;
                case Turno.N:
                    serializer.Serialize(writer, "N");
                    return;
                case Turno.V:
                    serializer.Serialize(writer, "V");
                    return;
            }
            throw new Exception("Cannot marshal type Turno");
        }

        public static readonly TurnoConverter Singleton = new TurnoConverter();
    }
}
