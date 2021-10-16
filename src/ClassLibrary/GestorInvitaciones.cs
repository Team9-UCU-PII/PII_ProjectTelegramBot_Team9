using System.Collections.Generic;
using System.Text;

namespace ChatBot
{
    public class GestorInvitaciones
    {
        private GestorInvitaciones(){}
        private GestorInvitaciones instancia {get;set;}

        //Lo hago singleton porque solo se precisa una instancia y tiene que guardar un estado (los invites enviados)
        public GestorInvitaciones GestionadorInvitaciones
        {
            get
            {
                if (this.instancia == null)
                {
                    this.instancia = new GestorInvitaciones();
                    return this.instancia;
                }
                return this.instancia;
            }
        }
        private List<string> invitacionesEnviadas = new List<string>();

        public void EnviarInvitacion(string numeroObjetivo)
        {
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
            mensaje.Append("Has sido invitado a unirte al chatbot de Telegram");
            mensaje.Append($"Link para unirte y registrarte:");
            mensaje.Append(Enlace);

            return mensaje.ToString();
        }
    }
}