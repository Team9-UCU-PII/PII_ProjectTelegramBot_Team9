using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.Login
{
    public class HandlerEscape : MessageHandlerBase
    {
        public HandlerEscape(IMessageHandler next = null)
        : base(new string[] {"abortar", "escapar"}, next)
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
                    this.ContainingForm.Next = new FrmBienvenida();
                }
                else
                {
                    //dejo placeholder para lo que encompasará el menu principal de acciones
                    //segun si es una empresa o emprendedor
                    this.ContainingForm.Next = new FrmMainMenu();
                }

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