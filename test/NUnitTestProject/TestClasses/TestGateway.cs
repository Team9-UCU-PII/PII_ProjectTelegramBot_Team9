using MessageGateway;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests.TestClasses
{
    public class TestGateway : GatewayBase
    {
        private List<IMessage> mensajesEnviados;
        private List<(string Token, Invitacion Invitacion)> invitacionesEnviadas;
        private LocationApiClient locationApiClient;

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
            this.invitacionesEnviadas = new List<(string token, Invitacion invitacion)>();
            this.locationApiClient = LocationApiClient.Instancia;
        }

        public override void EnviarMensaje(IMessage mensaje)
        {
            this.mensajesEnviados.Add(mensaje);
        }

        public override void EnviarUbicacionEnMapa(IMessage mensaje, float latitud, float longitud)
        {
            throw new NotImplementedException();
        }

        public override string ObtenerLinkInvitacion
        {
            get
            {
                return ("www.testgateway.com/invite-link");
            }
        }

        public string ObtenerUltimoTokenInvitacion(string destinatario)
        {
            return this.invitacionesEnviadas.Last().Token;
        }
    }
}