using System;
using BotCore.User;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.AceptarInvitacion
{
    public class HandlerValidarCodigo : MessageHandlerBase
    {
        private GestorInvitaciones gi = GestorInvitaciones.Instancia;

        public HandlerValidarCodigo(IMessageHandler next)
        : base(new string[] {}, next)
        {
        }

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
                    response = "¡Gracias por aceptar la invitación a unirte a #Nombre del bot#!";
                }
                else
                {
                    response = "No se ha podido verificar el código. Por favor, reingrésalo.";
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