using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Importers.Json
{
    /// <summary>
    /// Clase que exporta objetos de tipo IJsonConvertible a strings en formato JSON.
    /// </summary>
    public class JsonExporter
    {
        private static JsonSerializerOptions config = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        private string filePath;

        /// <summary>
        /// Inicializa una nueva clase de JsonExporter que guardará los objetos en la ruta proporcionada.
        /// </summary>
        /// <param name="filePath">La ruta al archivo donde se persisten los objetos.</param>
        public JsonExporter(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Guarda un objeto que implemente IJsonConvertible en formato JSON.
        /// </summary>
        /// <param name="obj">El objeto a exportar.</param>
        /// <typeparam name="T">Tipo del objeto que implementa IJsonConvertible.</typeparam>
        /// <returns>El string JSON que representa este objeto.</returns>
        public void Save<T>(T obj) where T : IJsonConvertible
        {
            string json = JsonSerializer.Serialize(obj, obj.GetType(), config);
            File.WriteAllText(this.filePath, json);
        }
    }
}