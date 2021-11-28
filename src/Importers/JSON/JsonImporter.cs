using System.Text.Json;
using System.IO;

namespace Importers.Json
{
    /// <summary>
    /// Clase que realiza la deserialización de objetos en formato JSON.
    /// </summary>
    /// <typeparam name="T">El tipo de los objetos a deserializar.</typeparam>
    public class JsonImporter<T> where T : IPersistible
    {
        private static JsonSerializerOptions config = new JsonSerializerOptions()
        {
            ReferenceHandler = MyReferenceHandler.Instance,
            WriteIndented = true
        };

        /// <summary>
        /// Inicializa una nueva instancia de JsonImporter.
        /// </summary>
        public JsonImporter()
        {
            
        }

        /// <summary>
        /// Genera un objeto desde el archivo proporcionado.
        /// </summary>
        /// <param name="filePath">El archivo desde el cual se lee el objeto JSON.</param>
        /// <returns>El objeto deserializado.</returns>
        public T Get(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}