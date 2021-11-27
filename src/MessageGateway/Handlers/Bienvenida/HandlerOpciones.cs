using MessageGateway.Forms;

namespace MessageGateway.Handlers.Bienvenida
{
    public class HandlerOpciones : MessageHandlerBase
    {
        public HandlerOpciones(IMessageHandler next)
        : base(new string[] {"1", "2", "3"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            response = string.Empty;
            if (this.CanHandle(message) && (CurrentForm as FrmBienvenida).CurrentState == HandlerBienvenida.faseWelcome.Eligiendo)
            {
                response = string.Empty;
                switch (message.TxtMensaje)
                {
                    case "1":
                        (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.Inicio;
                        this.CurrentForm.ChangeForm(new FrmLogin(), message.ChatID);
                        break;
                    case "2":
                        response = "Registrarse como \"Empresa\" o \"Emprendedor\"";
                        (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.choosingRegister;
                        break;
                    case "3":
                        (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.Inicio;
                        this.CurrentForm.ChangeForm(new FrmAceptarInvitacion(), message.ChatID);
                        break;
                    default:
                        return false;
                }
                return true;
            }
            else if ((CurrentForm as FrmBienvenida).CurrentState == HandlerBienvenida.faseWelcome.choosingRegister)
            {
                if (message.TxtMensaje.ToLower() == "empresa")
                {
                    (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.Inicio;
                    this.CurrentForm.ChangeForm((new FrmRegistroEmpresa()), message.ChatID);
                    return true;
                }
                else
                {
                    (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.Inicio;
                    this.CurrentForm.ChangeForm((new FrmRegistroEmpresa()), message.ChatID);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}