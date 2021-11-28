using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public class HandlerRegEmpresa : MessageHandlerBase
    {
        public HandlerRegEmpresa(IMessageHandler next)
        : base(new string[] {}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm is FrmRegistroEmpresa) && (CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "A continuación, te pediremos que ingreses los datos necesarios para que tu empresa sea visible en la plataforma.",
                "\n",
                "Primero, ingresa el nombre de tu empresa. Ten en cuenta que este nombre será público para todos los usuarios.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoNombre;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoNombre)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).NombrePublico = message.TxtMensaje;
                sb.Append("¿A que rubro se dedica la empresa?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoRubro;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoRubro)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).Rubro = message.TxtMensaje;
                sb.Append("¿Como describirías la empresa?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoDescripcion;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).Descripcion = message.TxtMensaje;
                sb.Append("¿Por que medios se puede contactar?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoContacto;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoContacto)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).Contacto = message.TxtMensaje;
                sb.Append("¿La empresa se ubica en Montevideo?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.ArmandoLocation;
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = HandlerLocation.faseLocation.tomandoMvdeo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }
        public enum fasesRegEmpresa
        {
            Inicio,
            tomandoNombre,
            tomandoRubro,
            tomandoDescripcion,
            tomandoContacto,
            ArmandoLocation,
            

        }
    }
}