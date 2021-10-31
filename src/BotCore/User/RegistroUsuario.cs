using Importers;

namespace BotCore.User
{
    /// <summary>
    /// Clase encargada de crear el usuario para inicio de sesión y su almacen en 
    /// <see cref = "DataAccess"/>.
    /// </summary>
    public class RegistroUsuario
    {
        /// <summary>
        /// Método registrador de usuario.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        /// <param name="usuario"></param>
        public static void RegistrarUsuario(string nombreUsuario, string contrasenia, IUsuario usuario)
        {
            DataAccess da = DataAccess.Instancia;
            DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia, usuario);

            da.Insertar(dl);
        }
    }
}