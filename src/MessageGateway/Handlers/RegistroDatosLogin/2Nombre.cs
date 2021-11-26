using MessageGateway.Forms;
using Importers;
using ClassLibrary.User;
using System.Linq;

namespace MessageGateway.Handlers.RegistroDatosLogin
{
    public class HandlerNombre : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerNombre(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Nombre}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                string nombre = message.TxtMensaje;
                int cantidadUsuarios = da.Obtener<DatosLogin>().Where(dl => dl.NombreUsuario == nombre).Count();
                if (cantidadUsuarios == 0)
                {
                    (this.ContainingForm as FrmRegistroDatosLogin).NombreUsuario = nombre;
                    response = "Ahora ingresa tu nueva contraseña.";
                    nextHandlerKeyword = PalabrasClaveHandlers.Contrasenia;
                }
                else
                {
                    response = "Ya existe un usuario con este nombre. Por favor elige otro.";
                    nextHandlerKeyword = PalabrasClaveHandlers.Nombre;
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