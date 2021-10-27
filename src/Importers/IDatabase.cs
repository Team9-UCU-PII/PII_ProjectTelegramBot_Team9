using System.Collections.Generic;

namespace Importers {
    public interface IDatabase {
        static IDatabase Instancia {get;}
        void Insertar<T>(T objeto);
        void Actualizar<T>(T objeto);
        List<T> Obtener<T>();
        void Eliminar<T>(T objeto);
    }
}