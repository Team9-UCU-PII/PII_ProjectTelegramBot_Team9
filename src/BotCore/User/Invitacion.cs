//--------------------------------------------------------------------------------
// <copyright file="Invitacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Text;
using ClassLibrary.User;

namespace BotCore.User
{
    /// <summary>
    /// Clase mediadora entre <see cref = "GestorInvitaciones"/> y los <see iref = "IUsuario"/>, representa la invitación en si, y encapsula el enlace y destino.
    /// </summary>
    public class Invitacion
    {
        /// <summary>
        /// Método constructor de la invitación.
        /// </summary>
        /// <param name="organizacion">El <see iref = "IUsuario"/> temporal, generado previamente.</param>
        /// <param name="usuarioDestinatario">Username o Contacto objetivo.</param>
        /// <param name="bot">El bot que se esté usando.</param>
        public Invitacion(IUsuario organizacion, string usuarioDestinatario, MessageGateway.IGateway bot)
        {
            this.OrganizacionInvitada = organizacion;
            this.Destinatario = usuarioDestinatario;
            this.Link = Invitacion.GenerarEnlace(bot);
            this.token = GenerarToken();
            this.FueAceptada = false;
        }

        /// <summary>
        /// El usuario destinado, debería ser sobreescrito por el destinatario.
        /// </summary>
        public IUsuario OrganizacionInvitada { get; }

        /// <summary>
        /// Obtiene string que permite identificar al que se invitó.
        /// </summary>
        public string Destinatario { get; }

        /// <summary>
        /// Este token unico identifica la invitación de las demas.
        /// </summary>
        /// <value><see langword ="string"/>.</value>
        public string Token 
        {
            get
            {
                return this.token;
            }
        }

        /// <summary>
        /// Código generado para validar la invitación.
        /// </summary>
        public string Link { get; }

        /// <summary>
        /// Propiedad, permite evaluar si el destinatario aceptó la invitación
        /// y se registró.
        /// </summary>
        /// <value>True: la invitación fue completada y aceptada. False: La invitación está en proceso.</value>
        public bool FueAceptada { get; private set; }

        /// <summary>
        /// Se genera el texto del mensaje a enviarse para invitar.
        /// </summary>
        /// <returns>string.</returns>
        public string ArmarMensajeInvitacion()
        {
            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine("Has sido invitado a unirte al chatbot de Telegram.");
            mensaje.AppendLine($"Link para unirte y registrarte: {this.Link}");
            mensaje.AppendLine($"Tu token de invitación: {this.Token}");
            return mensaje.ToString();
        }

        private static string GenerarEnlace(MessageGateway.IGateway bot)
        {
            return bot.ObtenerLinkInvitacion;
        }

        /// <summary>
        /// Metodo que modifica el estado de la invitación como aceptada.
        /// </summary>
        public void Aceptar()
        {
            if (this.FueAceptada)
            {
                throw new InvalidOperationException("Esta invitación ya fue aceptada.");
            }

            this.FueAceptada = true;
        }

        private string token { get; set; }
        
        private string GenerarToken()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i<=9 ; i++)
            {
                sb.Append(random.Next(10));
            }

            return sb.ToString();
        }
    }
}