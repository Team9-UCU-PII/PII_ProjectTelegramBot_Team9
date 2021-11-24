using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroEmprendedor
{
    public class HandlerRubro : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerRubro(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Rubro}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmprendedor frm = this.ContainingForm as FrmRegistroEmprendedor;
                frm.Rubro = message.TxtMensaje;

                response = "Ingresa cual es tu especialización como emprendedor.";
                nextHandlerKeyword = PalabrasClaveHandlers.Especializacion;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Rubro;
                return false;
            }
        }
    }
}