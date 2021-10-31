//--------------------------------------------------------------------------------
// <copyright file="DatosLogin.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace BotCore.User
{
    /// <summary>
    /// Las instancias de esta clase representan los usuarios creados en el bot,
    /// almacendando la empresa o emprendedor al que estan vinculados y su información
    /// de cuenta.
    /// </summary>
    public class DatosLogin
    {
        /// <summary>
        /// Username de la cuenta.
        /// </summary>
        /// <value></value>
        public string NombreUsuario { get; set; }
    /// <summary>
    /// Password de la cuenta.
    /// </summary>
    /// <value></value>
        public string Contrasenia { get; }
        /// <summary>
        /// <see cref = "Emprendedor"/> o <see cref = "Empresa"/> a la que se vincula esta cuenta.
        /// </summary>
        /// <value></value>
        public IUsuario Usuario { get;}
/// <summary>
/// Método constructor del usuario.
/// </summary>
/// <param name="nombreUsuario"></param>
/// <param name="contrasenia"></param>
/// <param name="usuario"></param>
        public DatosLogin(string nombreUsuario, string contrasenia, IUsuario usuario)
        {
            this.NombreUsuario = nombreUsuario;
            this.Contrasenia = contrasenia;
            this.Usuario = usuario;
        }
    }
}