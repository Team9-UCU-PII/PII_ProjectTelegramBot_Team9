using System.Collections.Generic;

namespace Importers {
    /// <summary>
    /// Esta clase manejara la logica cercana al acceso a la base de datos.
    /// </summary>
    public class DatabaseMemoria : IDatabase {
        private static DatabaseMemoria instancia;
        /// <summary>
        /// Acceso al singleton.
        /// </summary>
        /// <value><see iref = "IDatabase"/></value>
        public static IDatabase Instancia {
            get {
                if (DatabaseMemoria.instancia == null) {
                    DatabaseMemoria.instancia = new DatabaseMemoria();
                }
                return DatabaseMemoria.instancia;
            }
        }

        private DatabaseMemoria() {}
        /// <summary>
        /// Guardar un objeto en memoria.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        public void Insertar<T>(T objeto) {

        }
        /// <summary>
        /// Actualiza un objeto en memoria.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        public void Actualizar<T>(T objeto) {

        }
        /// <summary>
        /// Retorna instancia/s de la base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> Obtener<T>() {
            return null;
        }
        /// <summary>
        /// Borra instancias de la memoria.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        public void Eliminar<T>(T objeto) {

        }
    }
}