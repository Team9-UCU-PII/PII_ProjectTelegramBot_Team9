using System;
using System.Text;

namespace BotCore.User
{
    //esta clase funciona de mediadora entre el registro del user y el gestionador
    //similar al Visitor. Ayuda a almacenar tanto el user como el enlace que se uso para invitar
    //en el gestionador
    public  class Invitacion
    {
        public Invitacion(IUsuario organizacion, string usuarioDestinatario)
        {
            this.OrganizacionInvitada = organizacion;
            this.Destinatario = usuarioDestinatario;
            this.Link = Invitacion.GenerarEnlace();
            this.fueAceptada = false;
        }
        
        public IUsuario OrganizacionInvitada {get;}
        public string Destinatario {get;}
        public string Link {get;}
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
            //se genera el enlace
            return "enlace";
        }

        public void Aceptar() {
            if (this.fueAceptada) {
                throw new InvalidOperationException("Esta invitación ya fue aceptada.");
            }

            this.fueAceptada = true;
        }
    }
}