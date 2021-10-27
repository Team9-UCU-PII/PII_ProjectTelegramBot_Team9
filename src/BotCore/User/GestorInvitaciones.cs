using System.Collections.Generic;
using System.Text;

namespace BotCore.User
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
        public List<IUsuario> usuariosInvitados = new List<IUsuario>();

        public void EnviarInvitacion<T>(string numeroObjetivo, string nombreTemp) where T : IUsuario, new()
        {
            IUsuario user = new T();
            user.Nombre = nombreTemp;
            invitacionesEnviadas.Add(Invitacion.Enviar(numeroObjetivo, user));
            //se arma el txt y link y manda al bot
        }

        private bool ValidarInvitacion(Invitacion invite)
        {
            if  (invitacionesEnviadas.Contains(invite))
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