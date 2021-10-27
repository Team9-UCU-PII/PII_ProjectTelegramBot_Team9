using System.Collections.Generic;

namespace Importers {
    public class DatabaseMemoria : IDatabase {
        private static DatabaseMemoria instancia;
        public static IDatabase Instancia {
            get {
                if (DatabaseMemoria.instancia == null) {
                    DatabaseMemoria.instancia = new DatabaseMemoria();
                }
                return DatabaseMemoria.instancia;
            }
        }

        private DatabaseMemoria() {}

        public void Insertar<T>(T objeto) {

        }

        public void Actualizar<T>(T objeto) {

        }

        public List<T> Obtener<T>() {
            return null;
        }

        public void Eliminar<T>(T objeto) {

        }
    }
}