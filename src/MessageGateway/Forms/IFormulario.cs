//--------------------------------------------------------------------------------
// <copyright file="IFormulario.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Pricnipio utilizado: DIP
// La interfaz IFormulario se genera como abstracción para ser
// implementada por la clase FormularioBase.
//--------------------------------------------------------------------------------

namespace MessageGateway.Forms
{
    /// <summary>
    /// Interfaz que engloba todos los forularios y sus metodos principales.
    /// </summary>
    public interface IFormulario
    {
        /// <summary>
        /// Metodo que pase el mensaje por los handlers y devuelva una respuesta para el user
        /// </summary>
        /// <param name="mensaje">IMessage.</param>
        /// <returns>String.</returns>
        string ReceiveMessage(IMessage mensaje);

        /// <summary>
        /// Metodo que cambia el formulario donde esta parado el usuario.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="chatID"></param>
        void ChangeForm(IFormulario next, string chatID);
        
    }
}