//--------------------------------------------------------------------------------
// <copyright file="TelegramMessage.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using Telegram.Bot.Types;

namespace MessageGateway
{

    /// <summary>
    /// Adaptador del mensaje de telegram a la interfaz <see iref ="IMessage"/>.
    /// </summary>
    public class TelegramMessageAdapter : IMessage
    {

        /// <summary>
        /// Obtiene la id del chat en curso.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        public string ChatID { get; }

        /// <summary>
        /// Obtiene el mensaje que se recibió o envió último.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        public string TxtMensaje { get; }

        /// <summary>
        /// Constructor de un adaptador a partir de un Message de Telegram.
        /// </summary>
        /// <param name="msj"><see cref ="Message"/>.</param>
        public TelegramMessageAdapter (Message msj)
        {
            this.ChatID = msj.Chat.Id.ToString();
            this.TxtMensaje = msj.Text.ToLower();
        }

        private TelegramMessageAdapter (string newTxt)
        {
            this.TxtMensaje = newTxt;
        }

        /// <summary>
        /// Permite crear un message de telegram a partir de un <see langword ="string"/>.
        /// </summary>
        /// <param name="txt">Texto a hacer mensaje.</param>
        /// <returns><see cref ="TelegramMessageAdapter"/>.</returns>
        public IMessage CrearRespuesta(string txt)
        {
            return new TelegramMessageAdapter(txt);
        }
    }
}