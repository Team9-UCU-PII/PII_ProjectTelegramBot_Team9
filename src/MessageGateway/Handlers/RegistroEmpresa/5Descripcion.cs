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
        : base(new string[] {"Descripcion"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmpresa frm = this.ContainingForm as FrmRegistroEmpresa;
                frm.Descripcion = message.TxtMensaje;

                response = "Por último, ingresa un número de teléfono para que posibles interesados puedan contactarte.";
                nextHandlerKeyword = "Contacto";
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "Descripcion";
                return false;
            }
        }
    }
}