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
            if ((this.CanHandle(message) && (this.CurrentForm is ILocationForm) && (CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.Inicio))
            //Esta comprobacion es especifica para el alta oferta que es el primer uso que le doy a este handler,
            //se le puede añadir para casos de otros forms como "ORs".
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Se encuentra en Montevideo?\n");
                response = sb.ToString();
                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.tomandoMvdeo;

                return true;
            }
            else if (((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoMvdeo && message.TxtMensaje.ToLower() == "si"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ok, dinos la direccion (Con calle y numero de puerta o Km basta).\n");
                response = sb.ToString();

                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.tomandoDireccion;
                (CurrentForm as ILocationForm).city = "Montevideo";
                (CurrentForm as ILocationForm).dpto = "Montevideo";

                return true;
            }
            else if (((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoMvdeo && message.TxtMensaje.ToLower() == "no"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿En que departamento se halla? \n");
                response = sb.ToString();

                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.tomandoDpto;      

                return true;
            }
            else if (((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoDpto))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿En que ciudad?");
                response = sb.ToString();

                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.tomandoCity;
                (CurrentForm as ILocationForm).dpto = message.TxtMensaje;  

                return true;
            }
            else if (((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoCity) || ((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoCity))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Excelente, Ahora si dinos calle y numero de puerta o km.");
                response = sb.ToString();

                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.tomandoDireccion;
                (CurrentForm as ILocationForm).city = message.TxtMensaje;

                return true;
            }
            else if (((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoDireccion) || ((CurrentForm as ILocationForm).CurrentStateLocation == faseLocation.tomandoDireccion))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Creada y guardada la ubicación.");
                response = sb.ToString();

                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.Inicio;
                (CurrentForm as ILocationForm).direccion = message.TxtMensaje;

                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }

        }
        public enum faseLocation
        {
            Inicio,
            tomandoMvdeo,
            tomandoDpto,
            tomandoCity,
            tomandoDireccion
        }
    }
}