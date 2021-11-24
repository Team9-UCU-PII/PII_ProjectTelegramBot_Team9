using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroEmprendedor
{
    public class HandlerLugar : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerLugar(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Lugar}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmprendedor frm = this.ContainingForm as FrmRegistroEmprendedor;
                frm.Lugar = message.TxtMensaje;

                response = "Ahora, ingresa el rubro te dedicas como emprendedor.";
                nextHandlerKeyword = PalabrasClaveHandlers.Rubro;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Lugar;
                return false;
            }
        }
    }
}