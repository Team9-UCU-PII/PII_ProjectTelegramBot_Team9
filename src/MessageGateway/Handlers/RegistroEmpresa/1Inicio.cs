using System.Text;

namespace MessageGateway.Handlers.RegistroEmpresa
{
    public class HandlerInicio : MessageHandlerBase
    {
        public HandlerInicio(IMessageHandler next = null)
        : base(new string[] {"Inicio"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "A continuación, te pediremos que ingreses los datos necesarios para que tu empresa sea visible en la plataforma.",
                "\n",
                "Primero, ingresa el nombre de tu empresa. Ten en cuenta que este nombre será público para todos los usuarios.");
                response = sb.ToString();
                nextHandlerKeyword = "Nombre";
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "Inicio";
                return false;
            }
        }
    }
}