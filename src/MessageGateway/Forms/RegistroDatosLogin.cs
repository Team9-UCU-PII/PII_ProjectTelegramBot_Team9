using System.Collections.Generic;
using MessageGateway.Handlers.RegistroDatosLogin;
using ClassLibrary.User;

namespace MessageGateway.Forms
{
    public class FrmRegistroDatosLogin : FormularioBase
    {
        public IUsuario OrganizacionEnRegistro { get; private set; }
        public string NombreUsuario;

        public FrmRegistroDatosLogin(IUsuario organizacion)
        : base(new Dictionary<string, string> {})
        {
            this.OrganizacionEnRegistro = organizacion;
            this.messageHandler =
                new HandlerInicio(
                new HandlerNombre(
                new HandlerContrasenia()));
        }
    }
}