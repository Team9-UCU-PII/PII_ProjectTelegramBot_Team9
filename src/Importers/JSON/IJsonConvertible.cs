namespace Importers.Json
{
    public interface IJsonConvertible
    {
        /// <summary>
        /// Persiste el objeto en un archivo en formato JSON.
        /// </summary>
        /// <param name="jsonExporter">el objeto que realiza la persistencia.</param>
        void JsonSave(JsonExporter jsonExporter);
    }
}