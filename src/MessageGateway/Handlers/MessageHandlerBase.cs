//--------------------------------------------------------------------------------
// <copyright file="MessageHandlerBase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Linq;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    /// <summary>
    /// Superclase a todos los handlers.
    /// </summary>
    public abstract class MessageHandlerBase : IMessageHandler
    {

        /// <summary>
        /// Obtiene o establece el siguiente handler.
        /// </summary>
        /// <value>IMessageHandler</value>
        public IMessageHandler Next { get; set; }

        /// <summary>
        /// Obtiene o establece el formulario donde se está parado.
        /// </summary>
        /// <value>IFormulario</value>
        public IFormulario CurrentForm { get; set; }
        private string[] keywords { get; }

        /// <summary>
        /// Constructor base de Handler.
        /// </summary>
        /// <param name="keywords">Palabras clave que se queiren que el handler atrape.</param>
        /// <param name="next">Siguiente Handler.</param>
        protected MessageHandlerBase(string[] keywords, IMessageHandler next)
        {
            this.keywords = keywords;
            this.Next = next;
        }

        /// <summary>
        /// Determina si se puede manejar la palabra (EN MINUSCULA) desde las palabras clave.
        /// </summary>
        /// <param name="message">Meensaje a Corroborar.</param>
        /// <returns>True: si lo puede manejar/ es su responsabilidad.</returns>
        protected bool CanHandle(IMessage message)
        {
            return this.keywords.Contains(message.TxtMensaje.ToLower());
        }

        /// <summary>
        /// Intenta manejar el mensaje, si no puede lo manda al siguiente, si no existe un siguiente
        /// retorna null.
        /// </summary>
        /// <param name="message">IMessage a procesar.</param>
        /// <param name="response">String respuesta al User.</param>
        /// <returns></returns>
        public IMessageHandler Handle(IMessage message, out string response)
        {
            if (this.InternalHandle(message, out response))
            {
                return this;
            }
            else if (this.Next != null)
            {
                return this.Next.Handle(message, out response);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Especificación y desarrollo del proceso de manejo.
        /// </summary>
        /// <param name="message">Mensaje a intentar procesar.</param>
        /// <param name="response">String: Respuesta despeus de procesado.</param>
        /// <returns>True: si se pudo procesar.</returns>
        protected abstract bool InternalHandle(IMessage message, out string response);
    }
}