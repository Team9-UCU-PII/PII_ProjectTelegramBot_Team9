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
    public class TelegramBot
    {

        private const string TELEBRAM_BOT_TOKEN = "2102638185:AAFldbGxD1_7x5sw93EyKy8hvsy5c2uQBtw";
        private static TelegramBot instancia;
        private ITelegramBotClient bot;

        private TelegramBot()
        {
            this.bot = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }

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

        public long BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

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