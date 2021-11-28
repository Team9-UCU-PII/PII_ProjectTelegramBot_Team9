//--------------------------------------------------------------------------------
// <copyright file="IMessageHandler.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------


using MessageGateway.Forms;

namespace MessageGateway.Handlers
{

    /// <summary>
    /// Interfaz que engloba a los handlers y sus metodos y propiedades basicos.
    /// </summary>
    public interface IMessageHandler
    {

        /// <summary>
        /// El formulario que buscan completar.
        /// </summary>
        /// <value>IFormulario.</value>
        IFormulario CurrentForm { get; set; }

        /// <summary>
        /// El siguiente handler en caso de no poder procesar el mensaje.
        /// </summary>
        /// <value>IMessageHandler</value>
        IMessageHandler Next { get; set; }

        /// <summary>
        /// Intentar manejar el mensaje y sacar una respuesta o pasar al siguiente.
        /// </summary>
        /// <param name="mensaje">IMessage desde un Form.</param>
        /// <param name="response">String respuesta para el user.</param>
        /// <returns>IMessageHandler.</returns>
        IMessageHandler Handle(IMessage mensaje, out string response);
    }
}