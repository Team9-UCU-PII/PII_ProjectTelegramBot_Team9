using MessageGateway.Handlers.Escape;
using MessageGateway.Handlers.Bienvenida;

namespace MessageGateway.Forms
{
    public class FrmBienvenida : FormularioBase
    {
        public FrmBienvenida()
        {
            this.messageHandler =
                new HandlerBienvenida(
                new HandlerOpciones(
                    new HandlerEscape(null)
                ));
        }
        public HandlerBienvenida.faseWelcome CurrentState = HandlerBienvenida.faseWelcome.Inicio;
    }
}