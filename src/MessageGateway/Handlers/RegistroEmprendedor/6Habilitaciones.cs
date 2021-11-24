using MessageGateway.Forms;
using BotCore.User;
using ClassLibrary.User;
using Importers;
using System.Text;

namespace MessageGateway.Handlers.RegistroEmprendedor
{
    public class HandlerHabilitaciones : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerHabilitaciones(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Habilitaciones}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmprendedor frm = this.ContainingForm as FrmRegistroEmprendedor;
                frm.Habilitaciones = message.TxtMensaje;

                Emprendedor emprendedor = new Emprendedor(
                    frm.NombrePublico,
                    frm.Lugar,
                    frm.Rubro,
                    frm.Especializacion,
                    frm.Habilitaciones
                );

                RegistroUsuario.RegistrarUsuario<Emprendedor>(
                    frm.NombreUsuario,
                    frm.Contrasenia,
                    emprendedor
                );

                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Te has registrado satisfactoriamente. Los datos registrados son:",
                $"Nombre: {emprendedor.Nombre}",
                $"Lugar: {emprendedor.Lugar.FormattedAddress}",
                $"Rubro: {emprendedor.Rubro}",
                $"Especialización: {emprendedor.Especializacion}",
                $"Habilitaciones: {emprendedor.Habilitaciones}",
                "\n",
                $"También se ha creado credenciales para administrar tu cuenta:",
                $"Nombre de usuario: {frm.NombreUsuario}",
                $"Contraseña: {frm.Contrasenia}");

                // this.ContainingForm = new FrmRegistroEmprendedor();
                response = sb.ToString();
                nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Contacto;
                return false;
            }
        }
    }
}