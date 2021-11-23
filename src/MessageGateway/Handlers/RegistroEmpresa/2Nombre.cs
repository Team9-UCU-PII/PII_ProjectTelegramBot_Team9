using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroEmpresa
{
    public class HandlerNombre : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerNombre(IMessageHandler next = null)
        : base(new string[] {"Nombre"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmpresa frm = this.ContainingForm as FrmRegistroEmpresa;
                frm.NombrePublico = message.TxtMensaje;

                response = "Ahora, ingresa la dirección de tu empresa.";
                nextHandlerKeyword = "Lugar";
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "Nombre";
                return false;
            }
        }
    }
}