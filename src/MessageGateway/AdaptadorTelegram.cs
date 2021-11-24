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
using System.Collections.Generic;

namespace MessageGateway
{
    /// <summary>
    /// Esta clase instanciaría el chat y bot, y almacenaría mensaje por mensaje que va recibiendo
    /// como string. Permite portar la aplicación a Telegram siguiendo el patrón Adapter al heredar
    /// de la clase base que implementa IGateway
    /// </summary>
    public class AdaptadorTelegram : GatewayBase
    {
        private static AdaptadorTelegram instancia { get; set; }

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
        : base()
        {
            //Asigno un gestor de mensajes
            this.TelegramBot.Cliente.OnMessage += OnMessage;
            //Cuando se consstruye el adaptador ya se instancia el bot e inicia recepción de mensajes.
            this.TelegramBot.Cliente.StartReceiving();
        }

        /// <summary>
        /// Metodo de la interfaz <see iref ="IGateway"/>, envia un string como mensaje.
        /// </summary>
        /// <param name="mensaje">el IMessage a enviar.</param>
        public override void EnviarMensaje (IMessage mensaje)
        {
            this.TelegramBot.Cliente.SendTextMessageAsync(mensaje.ChatID, mensaje.TxtMensaje);
        }

        /// <summary>
        /// Obtiene el link pertinente para hablar con el bot.
        /// </summary>
        public override string ObtenerLinkInvitacion
        {
            get
            {
                return ($"t.me/{TelegramBot.BotId}");
            }
        }

        /// <summary>
        /// Diccionario que almacena el formulario actual de las distintas conversaciones que 
        /// mantiene el bot con los distintos usuarios.
        /// </summary>
        private Dictionary<string, IFormulario> Conversaciones { get; }

        /// <summary>
        /// Metodo que envia un mensaje de tipo ubicacion vinculado a un valor de latitud y longitud.
        /// </summary>
        /// <param name="mensaje">IMessage: info del chat.</param>
        /// <param name="latitud">Float: valor de latitud</param>
        /// <param name="longitud">Float: valor de longitud.</param>
        public override void EnviarUbicacionEnMapa(IMessage mensaje, float latitud, float longitud)
        {
            this.TelegramBot.Cliente.SendLocationAsync(mensaje.ChatID, latitud, longitud);
        }

        private async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            IMessage adaptedMessage = new TelegramMessageAdapter(message);
            string chatID = adaptedMessage.ChatID;

            if (adaptedMessage.TxtMensaje == "/start")
            {
                adaptedMessage.Keyword = Handlers.PalabrasClaveHandlers.Inicio;
                this.CrearConversacion(adaptedMessage.ChatID);
            }
            else if (adaptedMessage.TxtMensaje == "/abortar")
            {
                IFormulario formi;
                if 
                (
                    this.ObtenerFormularioActual(adaptedMessage.ChatID) is FrmAceptarInvitacion ||
                    this.ObtenerFormularioActual(adaptedMessage.ChatID) is FrmBienvenida ||
                    this.ObtenerFormularioActual(adaptedMessage.ChatID) is FrmRegistroDatosLogin ||
                    this.ObtenerFormularioActual(adaptedMessage.ChatID) is FrmRegistroEmpresa ||
                    this.ObtenerFormularioActual(adaptedMessage.ChatID) is FrmLogin 
                )
                {
                    formi = new FrmBienvenida();
                }
                else
                {
                    formi = new FrmBienvenida();
                }
                
                this.CambiarFormulario(formi,adaptedMessage.ChatID);
            }

            string frmPrevioMensaje;
            string respuesta;
            string frmPostMensaje;
            do
            {
                frmPrevioMensaje = this.ObtenerFormularioActual(chatID).GetType().ToString();
                respuesta = this.ObtenerFormularioActual(chatID).ReceiveMessage(adaptedMessage);
                frmPostMensaje = this.ObtenerFormularioActual(chatID).GetType().ToString();
            }
            while (frmPrevioMensaje != frmPostMensaje);

            await this.TelegramBot.Cliente.SendTextMessageAsync(adaptedMessage.ChatID, respuesta);
        }
    }
}