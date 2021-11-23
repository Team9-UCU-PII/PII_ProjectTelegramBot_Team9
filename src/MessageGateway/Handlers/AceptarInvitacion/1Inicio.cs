using System.Text;

namespace MessageGateway.Handlers.AceptarInvitacion
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
                "Aceptar una invitación",
                "\n",
                "Ingresa el código de invitación que recibiste del administrador:");
                response = sb.ToString();
                nextHandlerKeyword = "CodigoInvitacion";
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