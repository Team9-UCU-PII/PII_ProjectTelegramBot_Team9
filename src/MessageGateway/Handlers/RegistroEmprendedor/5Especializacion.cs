using MessageGateway.Forms;
using BotCore.User;
using ClassLibrary.User;
using Importers;

namespace MessageGateway.Handlers.RegistroEmprendedor
{
    public class HandlerEspecializacion : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerEspecializacion(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Especializacion}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmprendedor frm = this.ContainingForm as FrmRegistroEmprendedor;
                frm.Especializacion = message.TxtMensaje;

                response = "Por último, ingresa tu lista de habilitaciones.";
                nextHandlerKeyword = PalabrasClaveHandlers.Habilitaciones;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Especializacion;
                return false;
            }
        }
    }
}