using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroDatosLogin
{
    public class HandlerNombre : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerNombre(IMessageHandler next = null)
        : base(new string[] {"Nombre"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                string nombre = message.TxtMensaje;
                int cantidadUsuarios = da.CantidadUsuariosPorNombre(nombre);
                if (cantidadUsuarios == 0)
                {
                    (this.ContainingForm as FrmRegistroDatosLogin).NombreUsuario = nombre;
                    response = "Ahora ingresa tu nueva contraseña.";
                    nextHandlerKeyword = "Contrasenia";
                }
                else
                {
                    response = "Ya existe un usuario con este nombre. Por favor elige otro.";
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