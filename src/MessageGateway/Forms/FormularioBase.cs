//--------------------------------------------------------------------------------
// <copyright file="FormularioBase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Superclase para todos los formularios.
    /// </summary>
    public abstract class FormularioBase : IFormulario
    {
        private IGateway gateway = AdaptadorTelegram.Instancia;

        private IMessageHandler _messageHandler;

        /// <summary>
        /// Propiedad protegida que asigna al formulario como CurrentForm de todos los handlers que determine
        /// dentro de si.
        /// </summary>
        /// <value>IMessageHandler</value>
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

        /// <summary>
        /// Constructor base de Formulario.
        /// </summary>
        protected FormularioBase( ){}

        /// <summary>
        /// Metodo que pasa el mensaje recibido por todos los handlers contenidos en el formulario
        /// y devuelve la respuesta dada por los handlers.
        /// </summary>
        /// <param name="message">IMessage.</param>
        /// <returns>String.</returns>
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

        /// <summary>
        /// Metodo que finaliza un formulario y abre uno nuevo.
        /// </summary>
        /// <param name="next">IFormulario.</param>
        /// <param name="chatID">String.</param>
        public void ChangeForm(IFormulario next, string chatID)
        {
            this.gateway.CambiarFormulario(next, chatID);
        }
    }
}