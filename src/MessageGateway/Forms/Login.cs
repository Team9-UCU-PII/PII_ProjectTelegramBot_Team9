using System.Collections.Generic;
using MessageGateway.Handlers.Login;
using ClassLibrary.User;

namespace MessageGateway.Forms
{
    public class FrmLogin : FormularioBase
    {
        public string NombreUsuario;
        public string Contrasenia;

        public FrmLogin()
        : base(new Dictionary<string, string> {})
        {
            this.messageHandler =
                new HandlerInicio(
                new HandlerNombreUsuario(
                new HandlerContrasenia()));
        }
    }
}