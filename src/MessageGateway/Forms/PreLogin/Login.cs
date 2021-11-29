//--------------------------------------------------------------------------------
// <copyright file="Login.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers.Login;
using ClassLibrary.User;

namespace MessageGateway.Forms
{

    /// <summary>
    /// Formulario que engloba el inicio de sesión.
    /// </summary>
    public class FrmLogin : FormularioBase
    {

        /// <summary>
        /// Username que se tratara de loggear.
        /// </summary>
        public string NombreUsuario;

        /// <summary>
        /// Constructor de formulario y sus handlers necesarios.
        /// </summary>
        public FrmLogin()
        {
            this.messageHandler =
                new HandlerLogin(null);
        }

        /// <summary>
        /// Estado del handlerLogin.
        /// </summary>
        public HandlerLogin.fasesLogin CurrentState = HandlerLogin.fasesLogin.Inicio;
    }
}