//--------------------------------------------------------------------------------
// <copyright file="AceptarInvitacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patrón utilizado: Cadena de responsabilidad.
// En clase FrmAceptarInvitacion se va pasando el mensaje por una cadena de handlers,
// donde cada uno decide si el mensaje recibido lo procesa o lo manda al siguiente.
//--------------------------------------------------------------------------------

using MessageGateway.Handlers.AceptarInvitacion;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que regula las invitaciones hechas por los admins.
    /// </summary>
    public class FrmAceptarInvitacion : FormularioBase
    {

        /// <summary>
        /// Constructor del formulario con sus handler correspondientes.
        /// </summary>
        public FrmAceptarInvitacion()
        {
            this.messageHandler =
                new HandlerInviteInicio(
                    new HandlerValidarCodigo(null)
                );
        }

        /// <summary>
        /// El estado local del formulario, regulado por su handler principal HandlerInviteInicio.
        /// </summary>
        public HandlerInviteInicio.faseInvite CurrentState = HandlerInviteInicio.faseInvite.Inicio;
    }
}