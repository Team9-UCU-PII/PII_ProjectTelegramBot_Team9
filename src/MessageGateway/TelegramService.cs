using System;

namespace MessageGateway
{

    /// <summary>
    /// Clase placeholder del Bot a enviar mensajes.
    /// </summary>
    public class TelegramService : IGateway
    {
        private static TelegramService instancia;
        /// <summary>
        /// Acceso al Singleton del Bot.
        /// </summary>
        /// <value><see cref = "TelegramService"/></value>
        public static TelegramService Instancia 
        {
            get 
            {
                if (TelegramService.instancia == null) 
                {
                    TelegramService.instancia = new TelegramService();
                }
                return TelegramService.instancia;
            }
        }

        private TelegramService() {}

        public void EnviarInvitacion(string destinatario, string texto)
        {
        }
        
        /// <summary>
        /// Placeholder para el envio de un mensaje
        /// </summary>
        /// <param name="username"></param>
        /// <param name="text"></param>
        public void EnviarMensaje(string username, string text) 
        {    
        }
    }
}
