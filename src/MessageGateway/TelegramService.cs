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

        /// <summary>
        /// Vacío de momento, metodo para invitar.
        /// </summary>
        /// <param name="destinatario"><see langword = "string"/> del destinatario.</param>
        /// <param name="texto"><see langword = "string"/> con la invitación armada.</param>
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
