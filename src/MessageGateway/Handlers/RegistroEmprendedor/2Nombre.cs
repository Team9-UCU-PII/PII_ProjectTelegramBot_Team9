using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroEmprendedor
{
    public class HandlerNombre : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerNombre(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Nombre}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmprendedor frm = this.ContainingForm as FrmRegistroEmprendedor;
                frm.NombrePublico = message.TxtMensaje;

                response = "Ahora, ingresa tu dirección.";
                nextHandlerKeyword = PalabrasClaveHandlers.Lugar;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Nombre;
                return false;
            }
        }
    }
}