using MessageGateway;
using BotCore.User;
using System.Collections.Generic;
using System.Linq;

namespace Tests.TestClasses
{
    public class TestGateway : IGateway
    {
        public enum TiposMensajes
        {
            Invitacion,
            Normal
        }

        private List<(string, string, TiposMensajes)> mensajesEnviados;

        private static TestGateway instancia;
        public static TestGateway Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TestGateway();
                }
                return instancia;
            }
        }

        private TestGateway()
        {
            this.mensajesEnviados = new List<(string, string, TiposMensajes)>();
        }

        public void EnviarMensaje(string destinatario, string texto)
        {
            this.mensajesEnviados.Add((destinatario, texto, TiposMensajes.Normal));
        }

        public void EnviarInvitacion(string destinatario, string texto)
        {
            this.mensajesEnviados.Add((destinatario, texto, TiposMensajes.Invitacion));
        }

        public string ObtenerUltimoEnlaceInvitacion(string destinatario)
        {
            string textoInvitacion = this.mensajesEnviados.Where(
                x => x.Item1 == destinatario && x.Item3 == TiposMensajes.Invitacion
            ).Last().Item2;
            return textoInvitacion.Substring(textoInvitacion.LastIndexOf(' ') + 1);
        }
    }
}