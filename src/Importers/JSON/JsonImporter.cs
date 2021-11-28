using System.Text.Json;
using System.IO;

namespace Importers.Json
{
    public class JsonImporter<T> where T : IPersistible
    {
        private static JsonSerializerOptions config = new JsonSerializerOptions()
        {
            ReferenceHandler = MyReferenceHandler.Instance,
            WriteIndented = true
        };

        public JsonImporter()
        {
            
        }

        /// <summary>
        /// Obtiene un objeto del tipo especificado de un archivo en formato JSON.
        /// </summary>
        /// <typeparam name="T">el tipo del objeto obtenido.</typeparam>
        /// <returns></returns>
        public T Get<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}