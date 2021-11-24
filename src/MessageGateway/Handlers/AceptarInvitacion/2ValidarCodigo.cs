using System;
using BotCore.User;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.AceptarInvitacion
{
    public class HandlerValidarCodigo : MessageHandlerBase
    {
        private GestorInvitaciones gi = GestorInvitaciones.Instancia;

        public HandlerValidarCodigo(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.CodigoInvitacion}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                Invitacion invite;
                if (this.gi.ValidarInvitacion(message.TxtMensaje, out invite))
                {
                    this.ContainingForm.ChangeForm(
                        new FrmRegistroDatosLogin(invite.OrganizacionInvitada),
                        message.ChatID
                    );
                    response = "¡Gracias por aceptar la invitación a unirte a #Nombre del bot#!";
                    nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
                }
                else
                {
                    response = "No se ha podido verificar el código. Por favor, reingrésalo.";
                    nextHandlerKeyword = PalabrasClaveHandlers.CodigoInvitacion;
                }
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.CodigoInvitacion;
                return false;
            }
        }
    }
}