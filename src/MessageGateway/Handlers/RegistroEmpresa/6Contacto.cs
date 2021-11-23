using MessageGateway.Forms;
using BotCore.User;
using ClassLibrary.User;
using Importers;
using System.Text;

namespace MessageGateway.Handlers.RegistroEmpresa
{
    public class HandlerContacto : MessageHandlerBase
    {
        private DataAccess da = DataAccess.Instancia;

        public HandlerContacto(IMessageHandler next = null)
        : base(new string[] {"Contacto"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                FrmRegistroEmpresa frm = this.ContainingForm as FrmRegistroEmpresa;
                frm.Contacto = message.TxtMensaje;

                Empresa empresa = new Empresa(
                    frm.NombrePublico,
                    frm.Lugar,
                    frm.Rubro,
                    frm.Descripcion != "." ? frm.Descripcion : string.Empty,
                    frm.Contacto
                );

                RegistroUsuario.RegistrarUsuario<Empresa>(
                    frm.NombreUsuario,
                    frm.Contrasenia,
                    empresa
                );

                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Tu empresa fue registrada satisfactoriamente. Los datos registrados son:",
                $"Nombre: {empresa.Nombre}",
                $"Lugar: {empresa.Lugar.FormattedAddress}",
                $"Rubro: {empresa.Rubro}",
                $"Descripción: {empresa.Descripcion}",
                $"Contacto: {empresa.Contacto}",
                "\n",
                $"También se ha creado credenciales para administrar tu cuenta:",
                $"Nombre de usuario: {frm.NombreUsuario}",
                $"Contraseña: {frm.Contrasenia}");

                // this.ContainingForm = new FrmMenuEmpresa();
                response = sb.ToString();
                nextHandlerKeyword = "Inicio";
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "Contacto";
                return false;
            }
        }
    }
}