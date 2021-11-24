using MessageGateway.Forms;

namespace MessageGateway.Handlers.Bienvenida
{
    public class HandlerOpciones : MessageHandlerBase
    {
        public HandlerOpciones(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Opciones}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            response = string.Empty;
            if (this.CanHandle(message))
            {
                response = "";
                switch (message.TxtMensaje)
                {
                    case "login":
                        this.ContainingForm.ChangeForm(new FrmLogin(), message.ChatID);
                        break;
                    case "invitacion":
                        this.ContainingForm.ChangeForm(new FrmAceptarInvitacion(), message.ChatID);
                        break;
                    default:
                        nextHandlerKeyword = PalabrasClaveHandlers.Opciones;
                        return false;
                }
                nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
                return true;
            }
            else
            {
                nextHandlerKeyword = PalabrasClaveHandlers.Opciones;
                return false;
            }
        }
    }
}