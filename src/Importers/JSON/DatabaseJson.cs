using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text.Json;

namespace Importers.Json
{
    /// <summary>
    /// Esta clase es una implementación de IDatabase, y define todos los métodos necesarios para 
    /// implementar todas las responsabilidades definidas en la interfaz. El código cliente depende de
    /// esta forma de los métodos definidos en la interfaz, y no de los detalles de cada implementación.
    /// </summary>
    internal sealed class DatabaseJson : IDatabase
    {
        private const string K_StorageFolderRoot = "Data\\";
        private Dictionary<Type, string> generatedFolderPaths;
        private Dictionary<Type, Dictionary<int, string>> objectReferences;
        private Dictionary<Type, int> nextTypeId;
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
            this.objectReferences = new Dictionary<Type, Dictionary<int, string>>();
            this.nextTypeId = new Dictionary<Type, int>();

            if (! Directory.Exists(K_StorageFolderRoot))
            {
                Directory.CreateDirectory(K_StorageFolderRoot);
            }
            else
            {
                foreach (string folderName in Directory.GetDirectories(K_StorageFolderRoot))
                {
                    // Obtengo el tipo correspondiente a la carpeta
                    Type tipoEntidad = Type.GetType(folderName.Substring(folderName.LastIndexOf('\\') + 1)+", ClassLibrary");

                    // Creo el tipo genérico de JsonImporter, pasando como parámetro de tipo el tipo de la entidad
                    Type tipoImportadorDeT = typeof(JsonImporter<>).MakeGenericType(tipoEntidad);
                    var importer = Activator.CreateInstance(tipoImportadorDeT);

                    // Construyo el método genérico, tomando el método Get y agregándole el tipo de la entidad
                    // como parámetro de tipo
                    MethodInfo getter = importer.GetType().GetMethod("Get");

                    this.generatedFolderPaths.Add(tipoEntidad, folderName);
                    this.objectReferences.Add(tipoEntidad, new Dictionary<int, string>());
                    string[] files = Directory.GetFiles(folderName);

                    // Para cada archivo en la carpeta, construyo la ruta completa e invoco el método construido
                    foreach (string fileName in files)
                    {
                        object objetoDeserializado = getter.Invoke(importer, new object[] {fileName});
                        this.objectReferences[tipoEntidad].Add((objetoDeserializado as IJsonConvertible).SerializationID, fileName);
                    }

                    int lastId = (files.Select(x => Convert.ToInt32(x.Substring(x.LastIndexOf('\\')+1)))).Max();
                    this.nextTypeId.Add(tipoEntidad, lastId + 1);
                }
            }
        }

        public void Insertar<T>(T objeto) where T : IPersistible
        {
            if (! (typeof(T).GetTypeInfo().IsAssignableTo(typeof(IJsonConvertible).GetTypeInfo())))
            {
                throw new JsonException("El objeto enviado no implementa IJsonConvertible.");
            }

            if (! this.generatedFolderPaths.ContainsKey(typeof(T)))
            {
                string fullyQualifiedTypeName = objeto.GetType().ToString();
                Directory.CreateDirectory(K_StorageFolderRoot + fullyQualifiedTypeName);
                this.generatedFolderPaths.Add(typeof(T), fullyQualifiedTypeName);
                this.nextTypeId.Add(typeof(T), 1);
                this.objectReferences.Add(typeof(T), new Dictionary<int, string>());
            }

            string storageFolder = K_StorageFolderRoot + this.generatedFolderPaths[typeof(T)];
            string filePath = storageFolder + "\\" + (this.nextTypeId[typeof(T)]);
            (objeto as IJsonConvertible).SerializationID = this.nextTypeId[typeof(T)];
            this.nextTypeId[typeof(T)] += 1;
            this.objectReferences[typeof(T)].Add((objeto as IJsonConvertible).SerializationID, filePath);
            (objeto as IJsonConvertible).JsonSave(new JsonExporter(filePath));
        }

        public void Actualizar<T>(T objetoOriginal, T objetoModificado) where T : IPersistible
        {
            if (! (typeof(T).GetTypeInfo().IsAssignableTo(typeof(IJsonConvertible).GetTypeInfo())))
            {
                throw new JsonException("El objeto enviado no implementa IJsonConvertible.");
            }

            if (! this.objectReferences[typeof(T)].ContainsKey((objetoOriginal as IJsonConvertible).SerializationID))
            {
                throw new JsonException("El objeto nunca fue persistido. Insértelo antes de intentar guardar una modificación.");
            }

            string filePath = this.objectReferences[typeof(T)][(objetoOriginal as IJsonConvertible).SerializationID];
            (objetoModificado as IJsonConvertible).JsonSave(new JsonExporter(filePath));
        }

        public List<T> Obtener<T>() where T : class, IPersistible
        {
            if (! (typeof(T).GetTypeInfo().IsAssignableTo(typeof(IJsonConvertible).GetTypeInfo())))
            {
                throw new JsonException("El tipo solicitado no implementa IJsonConvertible.");
            }

            List<T> result = new List<T>();

            if (this.generatedFolderPaths.ContainsKey(typeof(T)))
            {
                IEnumerable<int> thisTypeSerializationIDs = this.objectReferences[typeof(T)].Keys;
                IEnumerable<string> thisTypeReferences = thisTypeSerializationIDs.Select(id => this.objectReferences[typeof(T)][id]);
                foreach (string filePath in thisTypeReferences)
                {
                    result.Add(new JsonImporter<T>().Get(filePath));
                }
            }
            return result;
        }

        public void Eliminar<T>(T objeto) where T : IPersistible
        {
            if (! (typeof(T).GetTypeInfo().IsAssignableTo(typeof(IJsonConvertible).GetTypeInfo())))
            {
                throw new JsonException("El tipo solicitado no implementa IJsonConvertible.");
            }

            if (! this.generatedFolderPaths.ContainsKey(typeof(T)))
            {
                throw new JsonException("No existen objetos persistidos de este tipo.");
            }

            string filePath = this.objectReferences[typeof(T)][(objeto as IJsonConvertible).SerializationID];
            this.objectReferences[typeof(T)].Remove((objeto as IJsonConvertible).SerializationID);
            File.Delete(filePath);
        }
    }
}