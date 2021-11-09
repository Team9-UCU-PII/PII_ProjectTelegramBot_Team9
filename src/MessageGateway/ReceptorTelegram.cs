//--------------------------------------------------------------------------------
// <copyright file="ReceptorTelegram.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Library;
using Telegram.Bot.Types.Enums;
using System.IO;
using System.Text;

namespace MessageGateway
{
    /// <summary>
    /// Esta clase instanciaría el chat y almacenaría mensaje por mensaje que va recibiendo como string.
    /// </summary>
    public class ReceptorTelegram
    {
        private static string mensaje;

        public static Chat Chat;
        public static string getMensajes
        {
            get
            {
                //Obtengo una instancia de TelegramBot
                TelegramBot telegramBot = TelegramBot.Instancia;

                //Obtengo el cliente de Telegram
                ITelegramBotClient bot = telegramBot.Cliente;

                //Asigno un gestor de mensajes
                bot.OnMessage += OnMessage;

                //Inicio la escucha de mensajes
                bot.StartReceiving();

                return ReceptorTelegram.mensaje;
            }
        }
    
        private static async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            Chat chatInfo = message.Chat;
            ReceptorTelegram.Chat = chatInfo;
            ReceptorTelegram.mensaje = message.Text.ToLower();
        }
    }
}