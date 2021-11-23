using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroEmpresa
{
    public class HandlerRubro : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerRubro(IMessageHandler next = null)
        : base(new string[] {"Rubro"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmpresa frm = this.ContainingForm as FrmRegistroEmpresa;
                frm.Rubro = message.TxtMensaje;

                response = "Si lo deseas, agrega una breve descripción de tu empresa, o envía \".\" para omitir este paso.";
                nextHandlerKeyword = "Descripcion";
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "Rubro";
                return false;
            }
        }
    }
}