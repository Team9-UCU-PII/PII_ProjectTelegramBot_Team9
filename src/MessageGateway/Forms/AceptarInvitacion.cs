using System.Collections.Generic;
using MessageGateway.Handlers.AceptarInvitacion;

namespace MessageGateway.Forms
{
    public class FrmAceptarInvitacion : FormularioBase
    {
        public FrmAceptarInvitacion()
        {
            this.messageHandler =
                new HandlerInviteInicio(
                new HandlerValidarCodigo());
        }

        public HandlerInviteInicio.faseInvite CurrentState = HandlerInviteInicio.faseInvite.Inicio;
    }
}