//--------------------------------------------------------------------------------
// <copyright file="HanlderOpciones.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Forms;

namespace MessageGateway.Handlers.Bienvenida
{

    /// <summary>
    /// Handler que redirecciona de la bienvenida a los formularios correspondientes.
    /// </summary>
    public class HandlerOpciones : MessageHandlerBase
    {

        /// <summary>
        /// Cosntructor, las palabras clave corresponden a las opciones del BIenvenida.
        /// </summary>
        /// <param name="next"></param>
        public HandlerOpciones(IMessageHandler next)
        : base(new string[] {"1", "2", "3"}, next)
        {
        }

        /// <summary>
        /// InternalHandle que genera y redirecciona a nuevos forms segun lo elegido.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            response = string.Empty;
            if (this.CanHandle(message) && (CurrentForm as FrmBienvenida).CurrentState == HandlerBienvenida.faseWelcome.Eligiendo)
            {
                response = string.Empty;
                switch (message.TxtMensaje)
                {
                    case "1":
                        (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.Inicio;
                        this.CurrentForm.ChangeForm(new FrmLogin(), message.ChatID);
                        break;
                    case "2":
                        (CurrentForm as FrmBienvenida).ChangeForm((new FrmRegistroDatosLogin()), message.ChatID);
                        break;
                    case "3":
                        (CurrentForm as FrmBienvenida).CurrentState = HandlerBienvenida.faseWelcome.Inicio;
                        this.CurrentForm.ChangeForm(new FrmAceptarInvitacion(), message.ChatID);
                        break;
                    default:
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}