using System.Text;

namespace MessageGateway.Handlers.RegistroEmprendedor
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
                "A continuación, te pediremos que ingreses los datos necesarios para que como emprendedor seas visible en la plataforma.",
                "\n",
                "Primero, ingresa tu nombre. Ten en cuenta que este nombre será público para todos los usuarios.");
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