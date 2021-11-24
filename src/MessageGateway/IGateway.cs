//--------------------------------------------------------------------------------
// <copyright file="IGateway.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using MessageGateway.Forms;
using System.Collections.Generic;

namespace MessageGateway
{
    /// <summary>
    /// Interfaz que engloba las diferentes salidas y entradas posibles de mensaje de invitación al bot (mail, chat de telegram, etc).
    /// </summary>
    public interface IGateway
    {
        /// <summary>
        /// Obtiene acceso al singleton.
        /// </summary>
        /// <value>Instancia <see iref ="IGateway"/>.</value>
        static IGateway Instancia { get; }

        /// <summary>
        /// Método de envío de mensaje.
        /// </summary>
        /// <param name="texto"><see langword = "string"/>: El mensaje en sí.</param>
        void EnviarMensaje(IMessage texto);

        /// <summary>
        /// Método que permite enviar foto (especialmente para api de localización).
        /// </summary>
        void EnviarUbicacionEnMapa(IMessage mensaje, float latitud, float longitud);

        /// <summary>
        /// Método para generar la invitacion a traves del medio.
        /// </summary>
        string ObtenerLinkInvitacion { get; }

        /// <summary>
        /// Método que retorna el formulario en el que se ubica actualmente la conversación con un usuario.
        /// </summary>
        /// <param name="chatID">el ID del chat del usuario</param>
        /// <returns>El formulario que procesa actualmente los mensajes del usuario.</returns>
        IFormulario ObtenerFormularioActual(string chatID);

        /// <summary>
        /// Cambia el formulario que procesa los mensajes de un usuario.
        /// </summary>
        /// <param name="next">el nuevo formulario que procesa los mensajes.</param>
        /// <param name="chatID">el ID del chat al cual se le cambia el formulario.</param>
        void CambiarFormulario(IFormulario next, string chatID);
    }
}