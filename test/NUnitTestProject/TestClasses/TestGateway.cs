using MessageGateway;
using BotCore.User;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests.TestClasses
{
    public class TestGateway : IGateway
    {
        private List<IMessage> mensajesEnviados;
        private IMessage UltimoMensaje { get; set; }

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
            this.mensajesEnviados = new List<IMessage>();
        }

        public void EnviarMensaje(IMessage mensaje)
        {
            this.mensajesEnviados.Add(mensaje);
        }

        public IMessage MensajeRecibido
        {
            get
            {
                if (UltimoMensaje != null)
                {
                    return UltimoMensaje;
                }
                throw new Exception("Error recibir mensaje.");
            }
        }

        public string ObtenerLinkInvitacion
        {
            get
            {
                return ("www.testgateway.com/invite-link");
            }
        }

        public string ObtenerUltimoTokenInvitacion(string destinatario)
        {
            // TODO: revisar
            return null;
            /*
            string textoInvitacion = this.mensajesEnviados.Where(
                x => x.Item1 == destinatario && x.Item3 == TiposMensajes.Invitacion
            ).Last().Item2;
            return textoInvitacion.Substring(textoInvitacion.LastIndexOf(' ') + 1);
            */
        }
    }
}