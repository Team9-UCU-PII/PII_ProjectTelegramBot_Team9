using System;

namespace MessageGateway
{
    public class TelegramService
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

        public void EnviarMensaje(string username, string text) {
            
        }
    }
}
