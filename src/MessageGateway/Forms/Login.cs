using MessageGateway.Handlers.Escape;
using MessageGateway.Handlers.Login;
using ClassLibrary.User;

namespace MessageGateway.Forms
{
    public class FrmLogin : FormularioBase
    {
        public IUsuario userLoggeado;

        public FrmLogin()
        {
            this.messageHandler =
                new HandlerLogin(
                    new HandlerEscape(null)
                );
        }

        public HandlerLogin.fasesLogin CurrentState = HandlerLogin.fasesLogin.Inicio;
    }
}