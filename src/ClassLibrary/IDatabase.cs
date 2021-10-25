namespace ChatBot {
    public interface IDatabase {
        void Insertar<T>(T objeto);
        void Actualizar<T>(T objeto);
        T[] Obtener<T>();
        void Eliminar<T>(T objeto);
    }
}