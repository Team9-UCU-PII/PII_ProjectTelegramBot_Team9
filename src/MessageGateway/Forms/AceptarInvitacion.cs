using System.Collections.Generic;
using MessageGateway.Handlers.AceptarInvitacion;

namespace MessageGateway.Forms
{
    public class FrmAceptarInvitacion : FormularioBase
    {
        public FrmAceptarInvitacion()
        : base(new Dictionary<string, string> {})
        {
            this.messageHandler =
                new HandlerInicio(
                new HandlerValidarCodigo());
        }
    }
}