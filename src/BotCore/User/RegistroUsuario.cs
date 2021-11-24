//--------------------------------------------------------------------------------
// <copyright file="RegistroUsuario.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patrón utilizado: Creator.
// Esta clase utiliza este patrón porque crea nuevos usuarios y los persiste en DataAccess.
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using Importers;

namespace BotCore.User
{
    /// <summary>
    /// Clase encargada de crear el usuario para inicio de sesión y su almacen en
    /// <see cref = "DataAccess"/>.
    /// </summary>
    public static class RegistroUsuario
    {
        private static DataAccess da = DataAccess.Instancia;

        /// <summary>
        /// Metodo que controla no repetir username.
        /// </summary>
        /// <param name="nombre">Username a revisarse.</param>
        /// <returns>True: si el username ya está tomado.</returns>
        public static bool UsuarioYaExiste(string nombre)
        {
            int cantidadUsuarios = da.CantidadUsuariosPorNombre(nombre);
            return cantidadUsuarios > 0;
        }

        /// <summary>
        /// Método registrador de usuario.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        /// <param name="usuario"></param>
        public static void RegistrarUsuario<T>(string nombreUsuario, string contrasenia, T usuario) where T : IUsuario
        {
            DataAccess da = DataAccess.Instancia;
            DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia, usuario);

            da.Insertar(usuario);
            da.Insertar(dl);
        }
    }
}