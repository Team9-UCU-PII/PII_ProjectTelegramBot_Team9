namespace Importers.Json
{
    /// <summary>
    /// Interfaz que permite a la librería de clases marcar las clases que se persisten en formato JSON.
    /// Distribuye responsabilidades aprovechando el patrón Visitor, haciendo que cada clase visitada
    /// (aquellas que implementan esta interfaz) permita recibir como visitante un objeto de tipo
    /// JsonExporter, que es la clase experta en realizar la persistencia de los objetos.
    /// </summary>
    public interface IJsonConvertible : IPersistible
    {
        /// <summary>
        /// Persiste el objeto en un archivo en formato JSON.
        /// </summary>
        /// <param name="jsonExporter">el objeto que realiza la persistencia.</param>
        void JsonSave(JsonExporter jsonExporter);
    }
}