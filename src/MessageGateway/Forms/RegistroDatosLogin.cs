using MessageGateway.Handlers.Escape;
using MessageGateway.Handlers.RegistroDatosLogin;
using ClassLibrary.User;

namespace MessageGateway.Forms
{
    public class FrmRegistroDatosLogin : FormularioBase
    {
        public IUsuario OrganizacionEnRegistro { get; private set; }
        public string NombreUsuario;
        public string Password;

        public FrmRegistroDatosLogin(IUsuario organizacion)
        //Esta sobrecarga permite registrar desde una invitacion.
        {
            this.OrganizacionEnRegistro = organizacion;
            this.messageHandler =
                new HandlerRegDatosLogin(
                    new HandlerEscape(null)
                );
        }

        public FrmRegistroDatosLogin()
        {
            this.messageHandler =
                new HandlerRegDatosLogin(
                    new HandlerEscape(null)
                );
        }

        public HandlerRegDatosLogin.faseRegDL CurrentState = HandlerRegDatosLogin.faseRegDL.Inicio;
    }
}