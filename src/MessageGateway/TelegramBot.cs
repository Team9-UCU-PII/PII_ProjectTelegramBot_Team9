//--------------------------------------------------------------------------------
// <copyright file="TelegramBot.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace MessageGateway
{
    /// <summary>
    /// Esta clase representa el bot en si y su cliente, accede a los handlers a traves de
    /// un adaptador que implementa <see iref ="IGateway"/>.
    /// </summary>
    public class TelegramBot
    {

        private readonly string TELEGRAM_BOT_TOKEN;
        private static TelegramBot instancia;
        private ITelegramBotClient bot;

        private TelegramBot()
        {
            if (System.IO.File.Exists("token"))
            {
                this.TELEGRAM_BOT_TOKEN = System.IO.File.ReadAllText("token");
                this.bot = new TelegramBotClient(TELEGRAM_BOT_TOKEN);
            }
            else
            {
                throw new Exception("No se encontró el API token del bot.");
            }
        }

        /// <summary>
        /// Obtiene el cliente del bot.
        /// </summary>
        /// <value>ITelegramBotClient</value>
        public ITelegramBotClient Cliente
        {
            get
            {
                return this.bot;
            }
        }

        private User BotInfo
        {
            get
            {
                return this.Cliente.GetMeAsync().Result;
            }
        }

        /// <summary>
        /// Obtiene el id del bot, un numero identificador único.
        /// </summary>
        /// <value><see langword ="long"/>.</value>
        public long BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        /// <summary>
        /// Obtiene el nombre del bot.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

        /// <summary>
        /// Obtiene acceso al singleton del Bot.
        /// </summary>
        /// <value><see cref ="TelegramBot"/>.</value>
        public static TelegramBot Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TelegramBot();
                }
                return instancia;
            }
        }
    }
}