using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.AceptarInvitacion
{
    public class HandlerInviteInicio : MessageHandlerBase
    {
        public HandlerInviteInicio(IMessageHandler next)
        : base(new string[] {/*en blanco a proposito*/}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmAceptarInvitacion).CurrentState == faseInvite.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Aceptar una invitación",
                "\n",
                "Ingresa el código de invitación que recibiste del administrador:");
                response = sb.ToString();
                (CurrentForm as FrmAceptarInvitacion).CurrentState = faseInvite.LeyendoToken;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        public enum faseInvite
        {
            Inicio,
            LeyendoToken,
            ValidandoToken
        }
    }
}