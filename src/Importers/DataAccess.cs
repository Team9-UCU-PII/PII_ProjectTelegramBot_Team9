using System;
using System.Text;

namespace Importers {
    public class DataAccess {
        private IDatabase db;

        private static DataAccess instancia;
        public static DataAccess Instancia {
            get {
                if (DataAccess.instancia == null) {
                    DataAccess.instancia = new DataAccess(DatabaseMemoria.Instancia);
                }
                return DataAccess.instancia;
            }
        }

        private DataAccess(IDatabase db) {
            this.db = db;
        }

        public void Insertar<T>(T objeto) {
            this.db.Insertar(objeto);
        }

        public void Actualizar<T>(T objeto) {
            this.db.Actualizar(objeto);
        }

        public T[] Obtener<T>() {
            return this.db.Obtener<T>();
        }

        public void Eliminar<T>(T objeto) {
            this.db.Eliminar(objeto);
        }
    }
}