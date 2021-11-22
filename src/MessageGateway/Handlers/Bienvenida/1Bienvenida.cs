using System.Text;

namespace MessageGateway.Handlers.Bienvenida
{
    public class HandlerBienvenida : MessageHandlerBase
    {
        public HandlerBienvenida(IMessageHandler next = null)
        : base(new PalabrasClaveHandlers[] {PalabrasClaveHandlers.Inicio}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "¡Bienvenido a #Nombre del bot#, nuestra plataforma de economía circular!",
                "Por favor, selecciona una de las opciones para ingresar: ",
                "\n",
                "1. Iniciar sesión",
                "2. Tengo un link de invitación");
                response = sb.ToString();
                nextHandlerKeyword = PalabrasClaveHandlers.Opciones;
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
                return false;
            }
        }
    }
}