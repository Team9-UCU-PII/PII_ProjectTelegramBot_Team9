//--------------------------------------------------------------------------------
// <copyright file="Bienvenida.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers.Escape;
using MessageGateway.Handlers.Bienvenida;

namespace MessageGateway.Forms
{

    /// <summary>
    /// Primer formulario del chat, lleva al inicio de sesion, invitacion o registro.
    /// </summary>
    public class FrmBienvenida : FormularioBase
    {

        /// <summary>
        /// Constructor del formulario y sus handlers.
        /// </summary>
        public FrmBienvenida()
        {
            this.messageHandler =
                new HandlerBienvenida(
                new HandlerOpciones(
                    new HandlerEscape(null)
                ));
        }

        /// <summary>
        /// Estado del formulario dado por su Handler Principal HandlerBienvenida.
        /// </summary>
        public HandlerBienvenida.faseWelcome CurrentState = HandlerBienvenida.faseWelcome.Inicio;
    }
}