//--------------------------------------------------------------------------------
// <copyright file="ReceptorTelegram.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using System.Text;

namespace MessageGateway
{
    /// <summary>
    /// Esta clase instanciaría el chat y bot, y almacenaría mensaje por mensaje que va recibiendo como string.
    /// Permite implementar gateway a telegram.
    /// </summary>
    public class AdaptadorTelegram : IGateway
    {
        private static AdaptadorTelegram instancia { get; set; }
        private string UltimoMensaje { get; set; }

        public static AdaptadorTelegram Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new AdaptadorTelegram();
                }
                return instancia;
            }
        }
        public Chat Chat;
        public TelegramBot TelegramBot  = TelegramBot.Instancia;
        private AdaptadorTelegram()
        {
            //Cuando se consstruye el adaptador ya se instancia el bot e inicia recepción de mensajes.

            //Asigno un gestor de mensajes
            this.TelegramBot.Cliente.OnMessage += OnMessage;
            //Inicio la escucha de mensajes
            this.TelegramBot.Cliente.StartReceiving();
        }

        public void EnviarMensaje (string mensaje)
        {
            this.TelegramBot.Cliente.SendTextMessageAsync(this.Chat.Id, mensaje);
        }

        public string MensajeRecibido
        {
            get
            {
                if (UltimoMensaje != null)
                {
                    return UltimoMensaje;
                }
                return "";
            }
        }

        private async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            Chat chatInfo = message.Chat;
            this.Chat = chatInfo;
            this.UltimoMensaje = message.Text.ToLower();
        }
    }
}