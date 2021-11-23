//--------------------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using MessageGateway.Handlers;

namespace MessageGateway
{

    /// <summary>
    /// Interfaz que engloba los tipos mensaje de las diferentes plataformas de mensaje.
    /// </summary>
    public interface IMessage
    {

        /// <summary>
        /// Obtiene un string con un identificador del chat en curso.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        string ChatID { get; }

        /// <summary>
        /// Obtiene un string con el texto del mensaje que se recibió o envía.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        string TxtMensaje { get; set; }

        string Keyword { get; set; }

        /*
        /// <summary>
        /// Crea un tipo mensaje apto para la respuesta de un mensaje recibido.
        /// </summary>
        /// <param name="txt">El texto a enviarse.</param>
        /// <returns><see iref = "IMessage"/>.</returns>
        IMessage CrearRespuesta(string txt);
        */
    }
}