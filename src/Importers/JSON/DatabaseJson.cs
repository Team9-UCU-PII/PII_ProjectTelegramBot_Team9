using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json;

namespace Importers.Json
{
    internal class DatabaseJson : IDatabase
    {
        private static IDatabase instancia;
        public static IDatabase Instancia
        {
            get
            {
                if (DatabaseJson.instancia == null)
                {
                    DatabaseJson.instancia = new DatabaseJson();
                }
                return DatabaseJson.instancia;
            }
        }

        private DatabaseJson()
        {
            this.generatedFolderPaths = new Dictionary<Type, string>();
            this.objectReferences = new Dictionary<object, string>();
            this.nextTypeId = new Dictionary<Type, int>();
        }

        private Dictionary<Type, string> generatedFolderPaths;
        private Dictionary<object, string> objectReferences;
        private Dictionary<Type, int> nextTypeId;

        public void Insertar<T>(T objeto) where T : IPersistible
        {
            if (! (objeto is IJsonConvertible))
            {
                throw new JsonException("El objeto enviado no implementa IJsonConvertible.");
            }

            if (! this.generatedFolderPaths.ContainsKey(typeof(T)))
            {
                this.generatedFolderPaths.Add(typeof(T), objeto.GetType().ToString());
                this.nextTypeId.Add(typeof(T), 1);
            }

            string filePath = this.generatedFolderPaths[typeof(T)] + '\\' + (this.nextTypeId[typeof(T)]);
            this.nextTypeId[typeof(T)] += 1;
            this.objectReferences.Add(objeto, filePath);
            (objeto as IJsonConvertible).JsonSave(new JsonExporter(filePath));
        }

        public void Actualizar<T>(T objetoOriginal, T objetoModificado) where T : IPersistible
        {
            if (! (objetoOriginal is IJsonConvertible))
            {
                throw new JsonException("El objeto enviado no implementa IJsonConvertible.");
            }

            if (! this.objectReferences.ContainsKey(objetoOriginal))
            {
                throw new JsonException("El objeto nunca fue persistido. Insértelo antes de intentar guardar una modificación.");
            }

            string filePath = this.objectReferences[objetoOriginal];
            this.objectReferences.Remove(objetoOriginal);
            this.objectReferences.Add(objetoModificado, filePath);
            (objetoModificado as IJsonConvertible).JsonSave(new JsonExporter(filePath));
        }

        public List<T> Obtener<T>() where T : class, IPersistible
        {
            if (! (typeof(T) is IJsonConvertible))
            {
                throw new JsonException("El tipo solicitado no implementa IJsonConvertible.");
            }

            List<T> result = new List<T>();

            if (this.generatedFolderPaths.ContainsKey(typeof(T)))
            {
                foreach (KeyValuePair<object, string> pair in this.objectReferences)
                {
                    string filePath = pair.Value;
                    result.Add(new JsonImporter(filePath).Get<T>());
                }
            }
            return result;
        }

        public void Eliminar<T>(T objeto) where T : IPersistible
        {
            if (! (typeof(T) is IJsonConvertible))
            {
                throw new JsonException("El tipo solicitado no implementa IJsonConvertible.");
            }

            if (! this.generatedFolderPaths.ContainsKey(typeof(T)))
            {
                throw new JsonException("No existen objetos persistidos de este tipo.");
            }

            string filePath = this.objectReferences[objeto];
            File.Delete(filePath);
        }
    }
}