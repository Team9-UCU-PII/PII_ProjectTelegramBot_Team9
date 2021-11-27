using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.Escape
{

    /// <summary>
    /// Handler que permite en cualquier estado de conversación que se encuentre, cancelar el proceso y volver al ultimo menu.
    /// </summary>
    public class HandlerEscape : MessageHandlerBase, IMessageHandler
    {
        public HandlerEscape(IMessageHandler next)
        : base(new string[] {"Abortar"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message))
            {
                response = "Cancelando y volviendo...";
                if 
                (
                    this.CurrentForm is FrmAceptarInvitacion ||
                    this.CurrentForm is FrmBienvenida ||
                    this.CurrentForm is FrmRegistroDatosLogin ||
                    this.CurrentForm is FrmRegistroEmpresa ||
                    this.CurrentForm is FrmLogin 
                )
                {
                    this.CurrentForm.ChangeForm( new FrmBienvenida(), message.ChatID);
                }
                else
                {
                    //dejo placeholder para lo que encompasará el menu principal de acciones
                    //segun si es una empresa o emprendedor
                    this.CurrentForm.ChangeForm( new FrmBienvenida(), message.ChatID);
                }

                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }
    }
}