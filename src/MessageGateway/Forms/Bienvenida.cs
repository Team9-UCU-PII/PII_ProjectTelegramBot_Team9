using System.Collections.Generic;
using MessageGateway.Handlers.Bienvenida;

namespace MessageGateway.Forms
{
    public class FrmBienvenida : FormularioBase
    {
        public FrmBienvenida()
        {
            this.messageHandler =
                new HandlerBienvenida(
                new HandlerOpciones());
        }
        public HandlerBienvenida.faseWelcome CurrentState = HandlerBienvenida.faseWelcome.Inicio;
    }
}