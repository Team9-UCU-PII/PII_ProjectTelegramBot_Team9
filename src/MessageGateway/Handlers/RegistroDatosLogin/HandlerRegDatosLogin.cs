//--------------------------------------------------------------------------------
// <copyright file="HandlerRegDatosLogin.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------


using System.Text;
using System.Collections.Generic;
using ClassLibrary.User;
using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroDatosLogin
{

    /// <summary>
    /// Handler que permite crear un nuevo datosLogin.
    /// </summary>
    public class HandlerRegDatosLogin : MessageHandlerBase
    {

        /// <summary>
        /// Constructor de palabras clave vacia por ser traido directamente.
        /// </summary>
        /// <param name="next"></param>
        public HandlerRegDatosLogin(IMessageHandler next)
        : base(new string[] {}, next)
        {
        }

        /// <summary>
        /// Internal Hnadle que revisa no repetir el username con base de datos y toma y confirma la
        /// contraseña.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Registro de nuevo usuario",
                "\n",
                "Ingresa tu nombre de usuario.");
                response = sb.ToString();

                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.tomandoUser;
                return true;
            }
            else if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.tomandoUser)
            {
                List<IUsuario> usersExistentes = database.Obtener<IUsuario>();
                StringBuilder sb = new StringBuilder();
                foreach (IUsuario user in usersExistentes)
                {
                    if (user.DatosLogin.NombreUsuario == message.TxtMensaje)
                    {
                        sb.Append("Este nombre de usuario ya esta tomado");
                        response = sb.ToString();
                        return true;
                    }
                }
                
                sb.Append("Muy bien, ahora escribe una contraseña.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroDatosLogin).NombreUsuario = message.TxtMensaje;
                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.tomandoPass;
                return true;
            }
            else if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.tomandoPass)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Confirma tu contraseña.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroDatosLogin).passChkr = message.TxtMensaje;

                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.confirmandoPass;
                return true;
            }
            else if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.confirmandoPass)
            {
                StringBuilder sb = new StringBuilder();
                
                if ((CurrentForm as FrmRegistroDatosLogin).passChkr != message.TxtMensaje)
                {
                    sb.Append("Error al confirmar, intenta de nuevo.");
                    response = sb.ToString();
                    return true; 
                }
                sb.Append("¡Contraseña Confirmada!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroDatosLogin).Password = (CurrentForm as FrmRegistroDatosLogin).passChkr;

                if((CurrentForm as FrmRegistroDatosLogin).OrganizacionEnRegistro != null)
                {
                    CurrentForm.ChangeForm(new FrmRegistroEmpresa((CurrentForm as FrmRegistroDatosLogin).OrganizacionEnRegistro), message.ChatID);
                }
                else
                {
                    CurrentForm.ChangeForm(new FrmRegistroEmprendedor(), message.ChatID);
                }
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Fases necesarias para registrarse.
        /// </summary>
        public enum faseRegDL
        {

            /// <summary>
            /// Iniciado el registro.
            /// </summary>
            Inicio,

            /// <summary>
            /// Tomando el username y comprobando no esté tomado.
            /// </summary>
            tomandoUser,

            /// <summary>
            /// Esperando una contraseña.
            /// </summary>
            tomandoPass,

            /// <summary>
            /// Repitiendo la contraseña.
            /// </summary>
            confirmandoPass,

            /// <summary>
            /// Finalizado el registro.
            /// </summary>
            Done
        }
        private DataAccess database = DataAccess.Instancia;
    }
}