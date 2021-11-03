//--------------------------------------------------------------------------------
// <copyright file="IGateway.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace MessageGateway
{
    /// <summary>
    /// Interfaz que engloba las diferentes salidas posibles de mensaje de invitación al bot (mail, chat de telegram, etc).
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
        /// <param name="destinatario">
        /// <see langword = "string"/> identificador necesario para el envío.
        /// </param>
        /// <param name="texto"><see langword = "string"/>: El mensaje en sí.</param>
        void EnviarMensaje(string destinatario, string texto);

        /// <summary>
        /// Método para enviar la invitacion a traves del medio.
        /// </summary>
        /// <param name="destinatario">
        /// <see langword = "string"/> identificador necesario para el envío de la invitación.
        /// </param>
        /// <param name="texto">La invitación como <see langword = "string"/>.</param>
        void EnviarInvitacion(string destinatario, string texto);
    }
}