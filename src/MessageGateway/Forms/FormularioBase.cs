using System.Collections.Generic;
using MessageGateway.Handlers;
using MessageGateway;

namespace MessageGateway.Forms
{
    public abstract class FormularioBase : IFormulario
    {
        private IGateway gateway = AdaptadorTelegram.Instancia;

        private Dictionary<string, string> referenciaComandos;

        private IMessageHandler _messageHandler;
        protected IMessageHandler messageHandler {
            get
            {
                return _messageHandler;
            }
            set
            {
                this._messageHandler = value;
                this._messageHandler.ContainingForm = this;
                IMessageHandler singleHandler = value;
                do
                {
                    singleHandler.ContainingForm = this;
                    singleHandler = singleHandler.Next;
                }
                while (singleHandler != null);
            }
        }

        public PalabrasClaveHandlers NextMessageKeyword { private get; set; }

        protected FormularioBase(Dictionary<string, string> referenciaComandos)
        {
            this.referenciaComandos = referenciaComandos;
            this.NextMessageKeyword = PalabrasClaveHandlers.Inicio;
        }


        public string ReceiveMessage(IMessage message)
        {
            IMessage mensajeTraducido = new MessageBase(
                message.ChatID,
                this.TraducirCodigo(message.TxtMensaje),
                this.NextMessageKeyword
            );

            string respuesta;
            PalabrasClaveHandlers palabraClaveSiguienteManejador;
            IMessageHandler manejadorUtilizado = this.messageHandler.Handle(
                mensajeTraducido,
                out respuesta,
                out palabraClaveSiguienteManejador
            );

            if (manejadorUtilizado != null)
            {
                this.NextMessageKeyword = palabraClaveSiguienteManejador;
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

        private string TraducirCodigo(string codigo)
        {
            if (this.referenciaComandos.ContainsKey(codigo))
            {
                return this.referenciaComandos[codigo];
            }
            else
            {
                return codigo;
            }
        }
    }
}