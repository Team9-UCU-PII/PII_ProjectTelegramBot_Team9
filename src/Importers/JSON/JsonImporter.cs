using System.Text.Json;
using System.IO;

namespace Importers.Json
{
    public class JsonImporter
    {
        private static JsonSerializerOptions config = new JsonSerializerOptions()
        {
            ReferenceHandler = MyReferenceHandler.Instance,
            WriteIndented = true
        };

        private string filePath;

        public JsonImporter(string filePath)
        {
            this.filePath = filePath;
        }

        

        /// <summary>
        /// Obtiene un objeto del tipo especificado de un archivo en formato JSON.
        /// </summary>
        /// <typeparam name="T">el tipo del objeto obtenido.</typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IPersistible
        {
            string json = File.ReadAllText(this.filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}