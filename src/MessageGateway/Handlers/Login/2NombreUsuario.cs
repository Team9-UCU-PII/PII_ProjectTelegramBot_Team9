using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.Login
{
    public class HandlerNombreUsuario : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerNombreUsuario(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Nombre}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmLogin frm = this.ContainingForm as FrmLogin;
                frm.NombreUsuario = message.TxtMensaje;

                response = "Ingresa tu contraseña.";
                nextHandlerKeyword = PalabrasClaveHandlers.Contrasenia;
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