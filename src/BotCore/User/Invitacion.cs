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
    /// Clase mediadora que representa la invitación en si, y encapsula el enlace y destino.
    /// </summary>
    public  class Invitacion
    {
        public const int K_LargoEnlace = 20;

        public Invitacion(IUsuario organizacion, string usuarioDestinatario)
        {
            this.OrganizacionInvitada = organizacion;
            this.Destinatario = usuarioDestinatario;
            this.Link = Invitacion.GenerarEnlace();
            this.fueAceptada = false;
        }
        /// <summary>
        /// El usuario destinado, debería ser sobreescrito por el destinatario.
        /// </summary>
        public IUsuario OrganizacionInvitada {get;}

        /// <summary>
        /// Via de comunicacion para que llegue la invitacion (numero, mail, etc)
        /// </summary>
        public string Destinatario {get;}
        
        /// <summary>
        /// Código generado para validar la invitación.
        /// </summary>
        public string Link {get;}

        /// <summary>
        /// Propiedad, permite evaluar si el destinatario aceptó la invitación
        /// y se registró.
        /// </summary>
        public bool fueAceptada {get; private set;}
        public string ArmarMensajeInvitacion()
        {
            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine("Has sido invitado a unirte al chatbot de Telegram.");
            mensaje.AppendLine($"Link para unirte y registrarte: {this.Link}");
            return mensaje.ToString();
        }
        private static string GenerarEnlace()
        {
            string enlace = "enlace";
            if (enlace.Length < K_LargoEnlace)
            {
                int cantCaracteresAAgregar = K_LargoEnlace - enlace.Length;
                enlace += new string('0', cantCaracteresAAgregar);
            }
            else
            {
                enlace = enlace.Substring(0, K_LargoEnlace);
            }
            return enlace;
        }

        public void Aceptar() {
            if (this.fueAceptada) {
                throw new InvalidOperationException("Esta invitación ya fue aceptada.");
            }

            this.fueAceptada = true;
        }
    }
}