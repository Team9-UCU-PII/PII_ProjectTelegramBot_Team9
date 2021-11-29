//--------------------------------------------------------------------------------
// <copyright file="MessageBase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers;

namespace MessageGateway
{

    /// <summary>
    /// Superclase de los mensajes a recibirse.
    /// </summary>
    public class MessageBase : IMessage
    {
        /// <summary>
        /// Obtiene o Establece la Id del chat relacionado al mensaje.
        /// </summary>
        /// <value></value>
        public string ChatID { get; set; }

        /// <summary>
        /// Obtiene o Establece el string con el texto del mensaje.
        /// </summary>
        /// <value>String.</value>
        public string TxtMensaje { get; set; }

        /// <summary>
        /// Contructor genérico del mensaje para sus herederos.
        /// </summary>
        /// <param name="chatID">Id del chat a crear el mensaje.</param>
        /// <param name="txtMensaje">Texto a mensajear.</param>
        public MessageBase(string chatID, string txtMensaje)
        {
            this.ChatID = chatID;
            this.TxtMensaje = txtMensaje;
        }
        
        /*
        public IMessage CrearRespuesta(string texto)
        {
            return new MessageBase(this.ChatID, texto);
        }
        */
    }
}