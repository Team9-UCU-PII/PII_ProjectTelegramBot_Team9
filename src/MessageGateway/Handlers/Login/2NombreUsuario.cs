using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.Login
{
    public class HandlerNombreUsuario : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerNombreUsuario(IMessageHandler next = null)
        : base(new string[] {"Nombre"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmLogin frm = this.ContainingForm as FrmLogin;
                frm.NombreUsuario = message.TxtMensaje;

                response = "Ingresa tu contraseña.";
                nextHandlerKeyword = "Contrasenia";
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