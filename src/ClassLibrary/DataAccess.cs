using System;
using System.Text;

namespace ChatBot {
    public class DataAccess {
        private IDatabase db;

        private static IDatabase dbAUsar;
        private static bool readyToInstantiate {
            get {
                return dbAUsar != null;
            }
        }
        private static DataAccess instancia;
        public static DataAccess Instancia {
            get {
                if (DataAccess.instancia == null) {
                    if (DataAccess.readyToInstantiate) {
                        DataAccess.instancia = new DataAccess(dbAUsar);
                    }
                    else {
                        StringBuilder msjExcepcion = new StringBuilder();
                        msjExcepcion.Append("No se ha realizado la configuración de la clase DataAccess.");
                        msjExcepcion.Append("Debe llamar primero al método estático Config.");
                        throw new InvalidOperationException(msjExcepcion.ToString());
                    }
                }
                return DataAccess.instancia;
            }
        }

        private DataAccess(IDatabase db) {
            this.db = db;
        }

        // Debe encontrarse una mejor forma, ya que DataAccess debería instanciarse al llamarse
        // el getter Instancia por primera vez desde el Core de la aplicación.
        // Este no debería conocer la BD, únicamente la clase DataAccess, pero al pedirle que
        // previo a la instanciación de DataAccess llame este método pasándole un IDatabase,
        // estamos obligando al Core a conocer IDatabase.
        public static void Config(IDatabase dbAUsar) {
            if (DataAccess.dbAUsar == null) {
                DataAccess.dbAUsar = dbAUsar;
            }
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