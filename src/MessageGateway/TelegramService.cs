using System;

namespace MessageGateway
{
    public class TelegramService : IGateway
    {
        private static TelegramService instancia;
        public static TelegramService Instancia {
            get {
                if (TelegramService.instancia == null) {
                    TelegramService.instancia = new TelegramService();
                }
                return TelegramService.instancia;
            }
        }

        private TelegramService() {}

        public void EnviarMensaje(string destinatario, string texto) {
            
        }

        public void EnviarInvitacion(string destinatario, string texto)
        {
            
        }
    }
}
