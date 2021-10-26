using System.Collections.Generic;
using System.Text;

namespace ChatBot
{
    public class GestorInvitaciones
    {
        private GestorInvitaciones(){}
        private static GestorInvitaciones instancia {get;set;}

        //Lo hago singleton porque solo se precisa una instancia y tiene que guardar un estado (los invites enviados)
        public static GestorInvitaciones Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GestorInvitaciones();
                }
                return instancia;
            }
        }
        public List<Invitacion> invitacionesEnviadas = new List<Invitacion>();

        public void EnviarInvitacion(string numeroObjetivo)
        {
            Invitacion invite = new Invitacion();
            invite.InvitacionEnviada(numeroObjetivo);
            //se arma el txt y link y manda al bot
        }

        private bool ValidarInvitacion(string Enlace)
        {
            if  (invitacionesEnviadas.Contains(Enlace))
            {
                return true;
            }
            return false;
        }

        private string ArmarMensajeInvitacion(string Enlace)
        {
            StringBuilder mensaje = new StringBuilder();
            mensaje.Append("Has sido invitado a unirte al chatbot de Telegram\n");
            mensaje.Append($"Link para unirte y registrarte:");
            mensaje.Append(Enlace);

            return mensaje.ToString();
        }
    }
}