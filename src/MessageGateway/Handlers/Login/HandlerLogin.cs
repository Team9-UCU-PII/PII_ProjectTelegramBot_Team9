//--------------------------------------------------------------------------------
// <copyright file="HandlerLogin.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using System.Collections.Generic;
using System.Text;
using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.Login
{

    /// <summary>
    /// Handler encargado de tomar inicio de sesion y corroborar datos en base de datos.
    /// </summary>
    public class HandlerLogin : MessageHandlerBase
    {

        /// <summary>
        /// Constructor con palabras clave en blanco por ser traido directo.
        /// </summary>
        /// <param name="next"></param>
        public HandlerLogin(IMessageHandler next)
        : base(new string[] {/*Intencionalmente en blanco.*/}, next)
        {
        }

        /// <summary>
        /// Internal Handle que Toma username, lo busca en base de datos, y despues comprueba si la
        /// contraseña coincide.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmLogin).CurrentState == fasesLogin.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Inicio de sesión",
                "\n",
                "Ingresa tu Nombre de Usuario.");
                response = sb.ToString();

                (CurrentForm as FrmLogin).CurrentState = fasesLogin.tomandoUser;
                return true;
            }
            else if ((CurrentForm as FrmLogin).CurrentState == fasesLogin.tomandoUser)
            {
                StringBuilder sb = new StringBuilder();
                List <IUsuario> currentUsers = dataBase.Obtener<IUsuario>();
                foreach (IUsuario user in currentUsers)
                {
                    if (user.DatosLogin.NombreUsuario == message.TxtMensaje)
                    {
                        (CurrentForm as FrmLogin).supuestoUser = user;
                    }
                }
                if ((CurrentForm as FrmLogin).supuestoUser == null)
                {
                    sb.Append("No se encontro a nadie con ese nombre de usuario.");
                    response = sb.ToString();
                    return true;
                }
                else
                {
                sb.Append($"Ahora ingresa tu contraseña");
                response = sb.ToString();
                (CurrentForm as FrmLogin).CurrentState = fasesLogin.tomandoPass;
                return true;
                }
            }
            else if ((CurrentForm as FrmLogin).CurrentState == fasesLogin.tomandoPass)
            {
                StringBuilder sb = new StringBuilder();
                if ((CurrentForm as FrmLogin).supuestoUser.DatosLogin.Contrasenia == message.TxtMensaje)
                {
                    sb.Append($"Iniciada Sesión Correctamente!");
                    response = sb.ToString();
                    (CurrentForm as FrmLogin).userLoggeado = (CurrentForm as FrmLogin).supuestoUser;
                    CurrentForm.ChangeForm(new FrmAltaOferta(), message.ChatID);
                    return true;
                }
                else
                {
                    sb.Append($"Nombre de usuario o contraseña errónea.");
                    response = sb.ToString();
                    (CurrentForm as FrmLogin).CurrentState = fasesLogin.tomandoPass;
                    return true;
                }
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Fases necesarias para iniciar sesión.
        /// </summary>
        public enum fasesLogin
        {
            /// <summary>
            /// Se inicio el handler.
            /// </summary>
            Inicio,

            /// <summary>
            /// Se esta esperando el nombre de usuario.
            /// </summary>
            tomandoUser,

            /// <summary>
            /// Se esta tomando la contraseña.
            /// </summary>
            tomandoPass
        }
        private DataAccess dataBase = DataAccess.Instancia;
    }
}