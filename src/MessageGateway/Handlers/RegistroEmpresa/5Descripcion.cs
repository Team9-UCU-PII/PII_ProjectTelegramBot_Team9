using MessageGateway.Forms;
using BotCore.User;
using ClassLibrary.User;
using Importers;

namespace MessageGateway.Handlers.RegistroEmpresa
{
    public class HandlerDescripcion : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerDescripcion(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Descripcion}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmpresa frm = this.ContainingForm as FrmRegistroEmpresa;
                frm.Descripcion = message.TxtMensaje;

                response = "Por último, ingresa un número de teléfono para que posibles interesados puedan contactarte.";
                nextHandlerKeyword = PalabrasClaveHandlers.Contacto;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Descripcion;
                return false;
            }
        }
    }
}