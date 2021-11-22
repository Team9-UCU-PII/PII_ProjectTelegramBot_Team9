using System.Text;
using BotCore.User;
using MessageGateway.Forms;
using ClassLibrary.User;

namespace MessageGateway.Handlers.RegistroDatosLogin
{
    public class HandlerContrasenia : MessageHandlerBase
    {
        private string primeraContrasenia;

        public HandlerContrasenia(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Contrasenia}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                if (string.IsNullOrEmpty(this.primeraContrasenia))
                {
                    this.primeraContrasenia = message.TxtMensaje;
                    response = "Por último, confirma tu contraseña ingresándola nuevamente.";
                    nextHandlerKeyword = PalabrasClaveHandlers.Contrasenia;
                    return true;
                }
                else if (message.TxtMensaje == this.primeraContrasenia)
                {
                    FrmRegistroDatosLogin frm = this.ContainingForm as FrmRegistroDatosLogin;
                    IUsuario organizacion = frm.OrganizacionEnRegistro;

                    switch (organizacion)
                    {
                        case Empresa e:
                            this.ContainingForm.Next = new FrmRegistroEmpresa(
                                frm.NombreUsuario,
                                this.primeraContrasenia,
                                e
                            );
                            break;
                        case Emprendedor e:
                        /*
                            this.containingForm.Next = new FrmRegistroEmprendedor(
                                frm.NombreUsuario,
                                this.primeraContrasenia,
                                e
                            );
                            break;
                        */
                        default:
                            throw new System.Exception();
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendJoin('\n',
                    "Se ha creado tu usuario con los siguientes datos: ",
                    $"Nombre de usuario: pepe",
                    $"Contraseña: {message.TxtMensaje}");
                    response = sb.ToString();
                    nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
                }
                else
                {
                    response = "Error: las contraseñas no coinciden. Vuelve a ingresar tu nueva contraseña.";
                    this.primeraContrasenia = string.Empty;
                    nextHandlerKeyword = PalabrasClaveHandlers.Contrasenia;
                }
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Contrasenia;
                return false;
            }
        }
    }
}