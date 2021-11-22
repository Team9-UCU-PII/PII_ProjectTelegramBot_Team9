using System.Collections.Generic;
using MessageGateway.Handlers.Bienvenida;

namespace MessageGateway.Forms
{
    public class FrmBienvenida : FormularioBase
    {
        public FrmBienvenida()
        : base(new Dictionary<string, string>
        {
            {"1", "login"},
            {"2", "invitacion"}
        })
        {
            this.messageHandler =
                new HandlerBienvenida(
                new HandlerOpciones());
        }
    }
}