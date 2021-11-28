//--------------------------------------------------------------------------------
// <copyright file="HandlerInviteInicio.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.AceptarInvitacion
{

    /// <summary>
    /// Handler de invitaciones, inicio.
    /// </summary>
    public class HandlerInviteInicio : MessageHandlerBase
    {

        /// <summary>
        /// Constructor. Las palabras clave son nulas ya que es producto directo del menu de Bienvenida.
        /// </summary>
        /// <param name="next"></param>
        public HandlerInviteInicio(IMessageHandler next)
        : base(new string[] {/*en blanco a proposito*/}, next)
        {
        }

        /// <summary>
        /// Handle interno. Invita al usuario a ingresar su token de invitacion.
        /// </summary>
        /// <param name="message">IMessage pasado por el form.</param>
        /// <param name="response">Response.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmAceptarInvitacion).CurrentState == faseInvite.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Aceptar una invitación",
                "\n",
                "Ingresa el código de invitación que recibiste del administrador:");
                response = sb.ToString();
                (CurrentForm as FrmAceptarInvitacion).CurrentState = faseInvite.LeyendoToken;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Fases del checkeo de la invitacion.
        /// </summary>
        public enum faseInvite
        {
            ///Entrada a ingresar el token.
            Inicio,
            ///Chekeando Token en base de datos.
            LeyendoToken
        }
    }
}