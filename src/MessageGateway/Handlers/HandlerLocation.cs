using System.Text;
using ClassLibrary.Publication;
using ClassLibrary.User;
using ClassLibrary.LocationAPI;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public class HandlerLocation: MessageHandlerBase, IMessageHandler
    {
        public HandlerLocation(IMessageHandler next) : base ((new string[] {"3"}), next)
        {
            this.Next = next;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (this.CurrentForm is FrmAltaOferta) && (CurrentForm as FrmAltaOferta).CurrentState == HandlerAltaOferta.fases.Eligiendo)
            //Esta comprobacion es especifica para el alta oferta que es el primer uso que le doy a este handler,
            //se le puede añadir para casos de otros forms como "ORs".
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Muy bien, ¿El retiro es en Montevideo? (Departamento y ciudad)\n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fases.ArmandoLocation;
                faseActual = faseLocation.tomandoMvdeo;
                return true;
            }
            else if (this.faseActual == faseLocation.tomandoMvdeo && message.TxtMensaje == "Si")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ok, dinos la direccion (Con calle y numero de puerta basta).\n");
                response = sb.ToString();
                faseActual = faseLocation.tomandoDireccion;
                return true;
            }
            else if (this.faseActual == faseLocation.tomandoMvdeo && message.TxtMensaje == "No")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿En que departamento se halla? \n");
                response = sb.ToString();
                faseActual = faseLocation.tomandoDpto;
                return true;
            }
            else if (this.faseActual == faseLocation.tomandoDpto)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿En que ciudad?");
                response = sb.ToString();

                this.dpto = message.TxtMensaje;

                faseActual = faseLocation.tomandoCity;
                return true;
            }
            else if (this.faseActual == faseLocation.tomandoCity)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Excelente, Ahora si dinos calle y numero de puerta o km.");
                response = sb.ToString();

                this.city = message.TxtMensaje;
                return true;
            }
            else if (this.faseActual == faseLocation.tomandoDireccion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Creada y guardada el lugar de retiro.");
                response = sb.ToString();
                this.locationFinal = LocationApiClient.Instancia.GetLocation(message.TxtMensaje, this.city, this.dpto);

                if (this.CurrentForm is FrmAltaOferta)
                {
                    (this.CurrentForm as FrmAltaOferta).lugarRetiro = this.locationFinal;
                    (this.CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fases.Eligiendo;
                }
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }

        }
        private enum faseLocation
        {
            tomandoMvdeo,
            tomandoDpto,
            tomandoCity,
            tomandoDireccion
        }
        private string dpto = "Montevideo";
        private string city = "Montevideo";
        private faseLocation faseActual;
        private Location locationFinal;
        

    }
}