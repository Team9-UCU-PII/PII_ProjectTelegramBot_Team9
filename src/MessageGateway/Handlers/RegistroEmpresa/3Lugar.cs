using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroEmpresa
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
                FrmRegistroEmpresa frm = this.ContainingForm as FrmRegistroEmpresa;
                frm.Lugar = message.TxtMensaje;

                response = "Ahora, ingresa el rubro al que tu empresa se dedica, para que los emprendedores puedan localizar tus publicaciones más fácilmente.";
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