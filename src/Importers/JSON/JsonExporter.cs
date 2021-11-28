using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Importers.Json
{
    /// <summary>
    /// Clase que exporta objetos de tipo IJsonConvertible a strings en formato JSON.
    /// </summary>
    public class JsonExporter : IJsonUtil
    {
        private static JsonSerializerOptions config = new JsonSerializerOptions()
        {
            //ReferenceHandler = MyReferenceHandler.Instance,
            WriteIndented = true
        };

        private string filePath;
        public string FilePath { get; set; }

        public JsonExporter()
        {

        }

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
            //string typeName = obj.GetType().ToString();
            //string folderName = typeName.Substring(typeName.LastIndexOf('.') + 1);
            File.WriteAllText(this.filePath, json);
        }

        /*
        public void Visit(Categoria c)
        {
            this.ExportResult = JsonSerializer.Serialize(c);
        }

        public void Visit(Publicacion p)
        {
            this.ExportResult = JsonSerializer.Serialize(p);
        }

        public void Visit(PublicacionRecurrente pr)
        {
            this.ExportResult = JsonSerializer.Serialize(pr);
        }

        public void Visit(Residuo r)
        {
            this.ExportResult = JsonSerializer.Serialize(r);
        }

        public void Visit(Venta v)
        {
            this.ExportResult = JsonSerializer.Serialize(v);
        }

        public void Visit(DatosLogin dl)
        {
            this.ExportResult = JsonSerializer.Serialize(dl);
        }

        public void Visit(Empresa e)
        {
            this.ExportResult = JsonSerializer.Serialize(e);
        }

        public void Visit(Emprendedor e)
        {
            this.ExportResult = JsonSerializer.Serialize(e);
        }

        public void Visit(Habilitacion h)
        {
            this.ExportResult = JsonSerializer.Serialize(h);
        }

        public void Visit(Invitacion i)
        {
            this.ExportResult = JsonSerializer.Serialize(i);
        }
        */
    }
}