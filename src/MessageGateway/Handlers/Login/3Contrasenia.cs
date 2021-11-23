using MessageGateway.Forms;
using BotCore.User;
using ClassLibrary.User;
using Importers;

namespace MessageGateway.Handlers.Login
{
    public class HandlerContrasenia : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerContrasenia(IMessageHandler next = null)
        : base(new string[] {"Contrasenia"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmLogin frm = this.ContainingForm as FrmLogin;
                frm.Contrasenia = message.TxtMensaje;

                IUsuario organizacion;
                bool credencialesCorrectas = Autenticacion.ValidarUsuario(
                    frm.NombreUsuario,
                    frm.Contrasenia,
                    out organizacion
                );
                if (credencialesCorrectas)
                {
                    response = $"Sesión iniciada correctamente como {organizacion.Nombre}.";
                    switch (organizacion)
                    {
                        case Empresa e:
                            //this.containingForm.Next = new FrmMenuEmpresa();
                            break;
                        case Emprendedor e:
                            //this.containingForm.Next = new FrmMenuEmprendedor();
                            break;
                        default:
                            throw new System.Exception();
                    }
                    nextHandlerKeyword = "Inicio";
                }
                else
                {
                    response = "Credenciales incorrectas, por favor reintente.";
                    nextHandlerKeyword = "Nombre";
                }

                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "Contrasenia";
                return false;
            }
        }
    }
}