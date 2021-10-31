using System.Collections.Generic;

namespace Importers {
    /// <summary>
    /// Interaz de acceso a la base de datos.
    /// </summary>
    public interface IDatabase {
        /// <summary>
        /// Retorno del singleton.
        /// </summary>
        /// <value></value>
        static IDatabase Instancia {get;}
        /// <summary>
        /// Guardar.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        void Insertar<T>(T objeto);
        /// <summary>
        /// Sobreescribir.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        void Actualizar<T>(T objeto);
        /// <summary>
        /// Leer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Obtener<T>();
        /// <summary>
        /// Borrar.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        void Eliminar<T>(T objeto);
    }
}