using System.Collections.Generic;
using System.Linq;
using MessageGateway;

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

        public void EnviarInvitacion<T>(string destinatario, string nombreTemp) where T : IUsuario, new()
        {
            IUsuario user = new T();
            user.Nombre = nombreTemp;
            Invitacion invite = new Invitacion(user, destinatario);

            TelegramService.Instancia.EnviarMensaje(destinatario, invite.ArmarMensajeInvitacion());

            this.invitacionesEnviadas.Add(invite);
        }

        // Este método es usado externamente por el MessageGateway
        private bool ValidarInvitacion(string usuarioAceptante, string enlace) 
        {
            Invitacion invite = this.invitacionesEnviadas.Where(
                (Invitacion i) => i.Destinatario == usuarioAceptante && i.Link == enlace && !i.fueAceptada
            ).SingleOrDefault();

            if (invite != null) {
                invite.Aceptar();
                return true;
            }
            else {
                return false;
            }
        }
    }
}