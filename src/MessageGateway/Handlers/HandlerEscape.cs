//--------------------------------------------------------------------------------
// <copyright file="HandlerEscape.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.Escape
{

    /// <summary>
    /// Handler que permite en cualquier estado de conversación que se encuentre, cancelar el proceso y volver al ultimo menu.
    /// </summary>
    public class HandlerEscape : MessageHandlerBase, IMessageHandler
    {

        /// <summary>
        /// Constructor de palabra clave "Abortar" que puede ser invocada en cualquier momento.
        /// </summary>
        /// <param name="next">IHandler siguiente.</param>
        public HandlerEscape(IMessageHandler next)
        : base(new string[] {"/abortar"}, next)
        {
        }

        /// <summary>
        /// Internal Handle que revisa donde esta parado el user y 
        /// devuelve al ultimo form generico/menu pricipal acorde.
        /// </summary>
        /// <param name="message">IMessage traido del Form.</param>
        /// <param name="response">String Respuesta al user.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message))
            {
                response = "Cancelando y volviendo...";
                if 
                (
                    this.CurrentForm is FrmAceptarInvitacion ||
                    this.CurrentForm is FrmBienvenida ||
                    this.CurrentForm is FrmRegistroDatosLogin ||
                    this.CurrentForm is FrmRegistroEmpresa ||
                    this.CurrentForm is FrmRegistroEmprendedor ||
                    this.CurrentForm is FrmLogin 
                )
                {
                    this.CurrentForm.ChangeForm( new FrmBienvenida(), message.ChatID);
                }
                else
                {
                    if (CurrentForm is IPostLogin && (CurrentForm as IPostLogin).InstanciaLoggeada is Empresa)
                    {
                     this.CurrentForm.ChangeForm( new FrmMenuEmpresa((CurrentForm as IPostLogin).InstanciaLoggeada), message.ChatID);   
                    }
                    else
                    {
                        this.CurrentForm.ChangeForm ( new FrmMenuEmprendedor((CurrentForm as IPostLogin).InstanciaLoggeada), message.ChatID);
                    }
                }

                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }
    }
}