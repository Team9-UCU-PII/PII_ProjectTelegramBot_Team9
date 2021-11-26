using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.Bienvenida
{
    public class HandlerBienvenida : MessageHandlerBase
    {
        public HandlerBienvenida(IMessageHandler next = null)
        : base(new string[] {"/start"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmBienvenida).CurrentState == faseWelcome.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                $"¡Bienvenido a {AdaptadorTelegram.Instancia.TelegramBot.BotName}, nuestra plataforma de economía circular!",
                "Por favor, selecciona una de las opciones para ingresar: ",
                "\n",
                "1. Iniciar sesión",
                "2. Tengo un link de invitación");
                response = sb.ToString();

                (CurrentForm as FrmBienvenida).CurrentState = faseWelcome.Eligiendo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }
        public enum faseWelcome
        {
            Inicio,
            Eligiendo,

        }
    }
}