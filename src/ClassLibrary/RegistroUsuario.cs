namespace ChatBot
{
    public class RegistrarUsuario
    {
        public static RegistrarUsuario(string nombreUsuario, string contrasenia, IUsuario usuario)
        {
            DataAccess da = DataAccess.Instancia;
            DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia, usuario);

            da.Insertar(dl);
        }
    }
}