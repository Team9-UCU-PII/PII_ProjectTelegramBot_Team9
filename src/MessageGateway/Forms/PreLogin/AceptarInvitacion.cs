//--------------------------------------------------------------------------------
// <copyright file="AceptarInvitacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
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