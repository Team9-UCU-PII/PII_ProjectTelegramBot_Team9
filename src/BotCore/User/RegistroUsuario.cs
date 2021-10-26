using Importers;

namespace BotCore.User
{
    public class RegistroUsuario
    {
        public static void RegistrarUsuario(string nombreUsuario, string contrasenia, IUsuario usuario)
        {
            DataAccess da = DataAccess.Instancia;
            DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia, usuario);

            da.Insertar(dl);
        }
    }
}