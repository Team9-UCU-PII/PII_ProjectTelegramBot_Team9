using MessageGateway.Forms;

namespace MessageGateway.Handlers.Bienvenida
{
    public class HandlerOpciones : MessageHandlerBase
    {
        public HandlerOpciones(IMessageHandler next = null)
        : base(new string[] {"Opciones"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            response = string.Empty;
            if (this.CanHandle(message))
            {
                response = "";
                switch (message.TxtMensaje)
                {
                    case "login":
                        this.ContainingForm.Next = new FrmLogin();
                        break;
                    case "invitacion":
                        this.ContainingForm.Next = new FrmAceptarInvitacion();
                        break;
                    default:
                        nextHandlerKeyword = "Opciones";
                        return false;
                }
                nextHandlerKeyword = "Inicio";
                return true;
            }
            else
            {
                nextHandlerKeyword = "Opciones";
                return false;
            }
        }
    }
}