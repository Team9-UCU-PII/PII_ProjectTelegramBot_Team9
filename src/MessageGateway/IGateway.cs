//--------------------------------------------------------------------------------
// <copyright file="IGateway.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

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
        /// Propiedad que permite leer el mensaje recibido.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        IMessage MensajeRecibido { get; }
    }
}