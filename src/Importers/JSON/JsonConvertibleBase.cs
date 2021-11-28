namespace Importers.Json
{
    public abstract class JsonConvertibleBase : IJsonConvertible
    {
        public int SerializationID { get; set; }

        public abstract void JsonSave(JsonExporter exporter);

        public static bool operator ==(JsonConvertibleBase a, JsonConvertibleBase b)
        {
            return a.SerializationID == b.SerializationID;
        }

        public static bool operator !=(JsonConvertibleBase a, JsonConvertibleBase b)
        {
            return a.SerializationID != b.SerializationID;
        }
    }
}