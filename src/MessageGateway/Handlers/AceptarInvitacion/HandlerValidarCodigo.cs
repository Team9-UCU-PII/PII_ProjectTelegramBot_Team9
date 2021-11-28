//--------------------------------------------------------------------------------
// <copyright file="HandlerValidarCodigo.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using BotCore.User;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.AceptarInvitacion
{

    /// <summary>
    /// Handler que tomara un token y revisara en base de datos si es valido.
    /// </summary>
    public class HandlerValidarCodigo : MessageHandlerBase
    {
        private GestorInvitaciones gi = GestorInvitaciones.Instancia;

        /// <summary>
        /// Cosntructor con palabrasclave vacio porque viene directo del handler anterior.
        /// </summary>
        /// <param name="next"></param>
        public HandlerValidarCodigo(IMessageHandler next)
        : base(new string[] {}, next)
        {
        }

        /// <summary>
        /// Internal handle que revisa si existe el token y redirecciona acordemente.
        /// </summary>
        /// <param name="message">IMessage desde el form.</param>
        /// <param name="response">String de respuesta al User.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmAceptarInvitacion).CurrentState == HandlerInviteInicio.faseInvite.LeyendoToken)
            {
                Invitacion invite;
                if (this.gi.ValidarInvitacion(message.TxtMensaje, out invite))
                {
                    this.CurrentForm.ChangeForm(
                        new FrmRegistroDatosLogin(invite.OrganizacionInvitada),
                        message.ChatID
                    );
                    response = $"¡Gracias por aceptar la invitación a unirte a {(AdaptadorTelegram.Instancia.TelegramBot.BotName)}";
                }
                else
                {
                    response = "No se ha podido verificar el código o no existe. Por favor, reingrésalo.";
                }
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }
    }
}