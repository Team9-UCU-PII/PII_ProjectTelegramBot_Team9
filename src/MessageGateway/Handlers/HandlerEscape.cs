using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{

    /// <summary>
    /// Handler que permite en cualquier estado de conversación que se encuentre, cancelar el proceso y volver al ultimo menu.
    /// </summary>
    public class HandlerEscape : MessageHandlerBase
    {
        public HandlerEscape(IMessageHandler next)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Abortar}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                response = "Cancelando y volviendo...";
                if 
                (
                    this.ContainingForm is FrmAceptarInvitacion ||
                    this.ContainingForm is FrmBienvenida ||
                    this.ContainingForm is FrmRegistroDatosLogin ||
                    this.ContainingForm is FrmRegistroEmpresa ||
                    this.ContainingForm is FrmLogin 
                )
                {
                    this.ContainingForm.ChangeForm( new FrmBienvenida(), message.ChatID);
                }
                else
                {
                    //dejo placeholder para lo que encompasará el menu principal de acciones
                    //segun si es una empresa o emprendedor
                    this.ContainingForm.ChangeForm( new FrmBienvenida(), message.ChatID);
                }

                nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
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