using MessageGateway.Handlers.Escape;
using MessageGateway.Handlers.AceptarInvitacion;

namespace MessageGateway.Forms
{
    public class FrmAceptarInvitacion : FormularioBase
    {
        public FrmAceptarInvitacion()
        {
            this.messageHandler =
                new HandlerInviteInicio(
                    new HandlerValidarCodigo(
                        new HandlerEscape(null)
                    )
                );
        }

        public HandlerInviteInicio.faseInvite CurrentState = HandlerInviteInicio.faseInvite.Inicio;
    }
}