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
using MessageGateway.Forms;

namespace MessageGateway
{
    /// <summary>
    /// Esta clase instanciaría el chat y bot, y almacenaría mensaje por mensaje que va recibiendo como string.
    /// Permite implementar gateway a telegram.
    /// </summary>
    public class AdaptadorTelegram : IGateway
    {
        private static AdaptadorTelegram instancia { get; set; }
        private IMessage UltimoMensaje { get; set; }

        /// <summary>
        /// Obitene acceso a la instancia del singleton de Adaptador.
        /// </summary>
        /// <value><see cref ="AdaptadorTelegram"/>.</value>
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

        /// <summary>
        /// Atributo que instancia el bot.
        /// </summary>
        public TelegramBot TelegramBot = TelegramBot.Instancia;
        private AdaptadorTelegram()
        {
            //Cuando se consstruye el adaptador ya se instancia el bot e inicia recepción de mensajes.

            //Asigno un gestor de mensajes
            this.TelegramBot.Cliente.OnMessage += OnMessage;
        }

        public void Start()
        {
            this.CurrentForm = new FrmBienvenida();
            this.TelegramBot.Cliente.StartReceiving();
        }

        /// <summary>
        /// Metodo de la interfaz <see iref ="IGateway"/>, envia un string como mensaje.
        /// </summary>
        /// <param name="mensaje"><see langword ="string"/>.</param>
        public void EnviarMensaje (IMessage mensaje)
        {
            this.TelegramBot.Cliente.SendTextMessageAsync(mensaje.ChatID, mensaje.TxtMensaje);
        }

        /// <summary>
        /// Obtiene el link pertinente para hablar con el bot.
        /// </summary>
        public string ObtenerLinkInvitacion
        {
            get
            {
                return ($"t.me/{TelegramBot.BotId}");
            }
        }

        public IFormulario CurrentForm { get; set; }

        /// <summary>
        /// Método que envía una ubicacion al usuario.
        /// </summary>
        public void EnviarUbicacionEnMapa(IMessage mensaje, float latitud, float longitud)
        {
            this.TelegramBot.Cliente.SendLocationAsync(mensaje.ChatID, latitud, longitud);
        }

        private async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            IMessage adaptedMessage = new TelegramMessageAdapter(message);
            if (adaptedMessage.TxtMensaje == "/start")
            {
                adaptedMessage.Keyword = "Inicio";
            }
            
            string frmPrevioMensaje;
            string respuesta;
            string frmPostMensaje;
            do
            {
                frmPrevioMensaje = this.CurrentForm.GetType().ToString();
                respuesta = this.CurrentForm.ReceiveMessage(adaptedMessage);
                frmPostMensaje = this.CurrentForm.GetType().ToString();
            }
            while (frmPrevioMensaje != frmPostMensaje);

            await this.TelegramBot.Cliente.SendTextMessageAsync(adaptedMessage.ChatID, respuesta);
        }
    }
}