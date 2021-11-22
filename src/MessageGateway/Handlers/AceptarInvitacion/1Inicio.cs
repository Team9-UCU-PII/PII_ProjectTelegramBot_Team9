using System.Text;

namespace MessageGateway.Handlers.AceptarInvitacion
{
    public class HandlerInicio : MessageHandlerBase
    {
        public HandlerInicio(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Inicio}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Aceptar una invitación",
                "\n",
                "Ingresa el código de invitación que recibiste del administrador:");
                response = sb.ToString();
                nextHandlerKeyword = PalabrasClaveHandlers.CodigoInvitacion;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
                return false;
            }
        }
    }
}