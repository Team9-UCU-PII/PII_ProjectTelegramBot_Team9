//--------------------------------------------------------------------------------
// <copyright file="TelegramMessage.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using Telegram.Bot.Types;

namespace MessageGateway
{
    public class TelegramMessage : IMessage
    {
        public string ChatID { get; }

        public string TxtMensaje { get; }

        public TelegramMessage (Message msj)
        {
            this.ChatID = msj.Chat.Id.ToString();
            this.TxtMensaje = msj.Text.ToLower();
        }

        private TelegramMessage (string newTxt)
        {
            this.TxtMensaje = newTxt;
        }

        public IMessage CrearRespuesta(string txt)
        {
            return new TelegramMessage(txt);
        }
    }
}