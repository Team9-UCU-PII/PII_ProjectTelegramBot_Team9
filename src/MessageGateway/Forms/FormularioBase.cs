using System.Collections.Generic;
using MessageGateway.Handlers;
using MessageGateway;

namespace MessageGateway.Forms
{
    public abstract class FormularioBase : IFormulario
    {
        private IGateway gateway = AdaptadorTelegram.Instancia;

        private IMessageHandler _messageHandler;
        protected IMessageHandler messageHandler {
            get
            {
                return _messageHandler;
            }
            set
            {
                this._messageHandler = value;
                this._messageHandler.CurrentForm = this;
                IMessageHandler singleHandler = value;
                do
                {
                    singleHandler.CurrentForm = this;
                    singleHandler = singleHandler.Next;
                }
                while (singleHandler != null);
            }
        }

        protected FormularioBase( )
        {
        }


        public string ReceiveMessage(IMessage message)
        {

            string respuesta;
            IMessageHandler manejadorUtilizado = this.messageHandler.Handle(
                message,
                out respuesta
            );

            if (manejadorUtilizado != null)
            {
                return respuesta;
            }
            else
            {
                return "El mensaje no pudo ser procesado.";
            }
        }

        public void ChangeForm(IFormulario next, string chatID)
        {
            this.gateway.CambiarFormulario(next, chatID);
        }
    }
}