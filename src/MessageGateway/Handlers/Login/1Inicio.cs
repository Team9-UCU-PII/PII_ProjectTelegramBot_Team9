using System.Text;

namespace MessageGateway.Handlers.Login
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
                "Inicio de sesión",
                "\n",
                "Ingresa el nombre de usuario que utilizarás para iniciar sesión en la plataforma.");
                response = sb.ToString();
                nextHandlerKeyword = PalabrasClaveHandlers.Nombre;
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