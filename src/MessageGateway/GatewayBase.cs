//--------------------------------------------------------------------------------
// <copyright file="GatewayBase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Forms;
using System;
using System.Collections.Generic;

namespace MessageGateway
{

    /// <summary>
    ///Clase base que al implementar la interfaz IGateway sigue el principio de inversión de dependencias.
    /// </summary>
    public abstract class GatewayBase : IGateway
    {
        protected readonly DateTime startupTime;

        /// <summary>
        /// Para reducir el acoplamiento, usamos composición para delegar las peticiones de cambio
        /// de estado de las conversaciones en la instancia de la clase ChatManager.
        /// </summary>
        private ChatManager chatManager;

        /// <summary>
        /// Constructor vacío.
        /// </summary>
        protected GatewayBase()
        {
            this.chatManager = ChatManager.Instancia;
            this.startupTime = DateTime.Now.ToUniversalTime();
        }

        /// <summary>
        /// Envía un mensaje de texto a través del Gateway actual.
        /// </summary>
        /// <param name="mensaje">el IMessage a enviar.</param>
        public abstract void EnviarMensaje(IMessage mensaje);

        /// <summary>
        /// Envía una ubicación a usando el Gateway y servicio de localización actuales.
        /// </summary>
        /// <param name="mensaje">el texto que acompaña la ubicación.</param>
        /// <param name="latitud">latitud de la ubicación enviada.</param>
        /// <param name="longitud">longitud de la ubicación enviada.</param>
        public abstract void EnviarUbicacionEnMapa(IMessage mensaje, float latitud, float longitud);

        /// <summary>
        /// Obtiene el enlace al bot en la plataforma actual.
        /// </summary>
        /// <value>El enlace al chatbot en la plataforma.</value>
        public abstract string ObtenerLinkInvitacion { get; }

        /// <summary>
        /// Crea una nueva conversación con un usuario.
        /// </summary>
        /// <param name="chatID">el ID del usuario para el cual se crea la conversación.</param>
        public void CrearConversacion(string chatID)
        {
            this.chatManager.CrearConversacion(chatID);
        }

        /// <summary>
        /// Método que retorna el formulario en el que se ubica actualmente la conversación con un usuario.
        /// </summary>
        /// <param name="chatID">el ID del chat del usuario</param>
        /// <returns>El formulario que procesa actualmente los mensajes del usuario.</returns>
        public IFormulario ObtenerFormularioActual(string chatID)
        {
            return this.chatManager.ObtenerFormularioActual(chatID);
        }

        /// <summary>
        /// Cambia el formulario que procesa los mensajes de un usuario.
        /// </summary>
        /// <param name="next">el nuevo formulario que procesa los mensajes.</param>
        /// <param name="chatID">el ID del chat al cual se le cambia el formulario.</param>
        public void CambiarFormulario(IFormulario next, string chatID)
        {
            this.chatManager.CambiarFormulario(next, chatID);
        }
    }
}