using System;
using System.Collections.Generic;
using MessageGateway.Forms;

namespace MessageGateway
{
    /// <summary>
    /// Creamos esta clase basándonos en el principio de responsabilidad única, ya que el Gateway
    /// debería ser responsable únicamente del envío y recepción de mensajes, y no del almacenamiento
    /// del estado de las conversaciones, responsabilidad que es delegada en esta clase.
    /// </summary>
    public sealed class ChatManager
    {
        private static ChatManager instancia;
        public static ChatManager Instancia
        {
            get
            {
                if (ChatManager.instancia == null)
                {
                    ChatManager.instancia = new ChatManager();
                }
                return instancia;
            }
        }
        
        private Dictionary<string, IFormulario> Conversaciones;

        private ChatManager()
        {
            this.Conversaciones = new Dictionary<string, IFormulario>();
        }

        
        /// <summary>
        /// Método que retorna el formulario en el que se ubica actualmente la conversación con un usuario.
        /// </summary>
        /// <param name="chatID">el ID del chat del usuario</param>
        /// <returns>El formulario que procesa actualmente los mensajes del usuario.</returns>
        public IFormulario ObtenerFormularioActual(string chatID)
        {
            if (this.Conversaciones.ContainsKey(chatID))
            {
                return Conversaciones[chatID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Cambia el formulario que procesa los mensajes de un usuario.
        /// </summary>
        /// <param name="next">el nuevo formulario que procesa los mensajes.</param>
        /// <param name="chatID">el ID del chat al cual se le cambia el formulario.</param>
        public void CambiarFormulario(IFormulario next, string chatID)
        {
            if (this.Conversaciones.ContainsKey(chatID))
            {
                this.Conversaciones[chatID] = next;
            }
            else
            {
                throw new ArgumentException("No se encontró un chat con este ID.", nameof(chatID));
            }
        }

        /// <summary>
        /// Crea una nueva conversación con un usuario.
        /// </summary>
        /// <param name="chatID">el ID del usuario para el cual se crea la conversación.</param>
        public void CrearConversacion(string chatID)
        {
            if (! this.Conversaciones.TryAdd(chatID, new FrmBienvenida()))
            {
                throw new InvalidOperationException("Ya existe una conversación activa con este usuario.");
            }
        }
    }
}