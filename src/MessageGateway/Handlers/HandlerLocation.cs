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
            if ((this.CanHandle(message) && (this.CurrentForm is FrmAltaOferta) && (CurrentForm as FrmAltaOferta).CurrentStateLocation == faseLocation.Inicio)
            || ((this.CurrentForm is FrmRegistroEmpresa) && (CurrentForm as FrmRegistroEmpresa).CurrentState == HandlerRegEmpresa.fasesRegEmpresa.ArmandoLocation && (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation == faseLocation.Inicio))
            //Esta comprobacion es especifica para el alta oferta que es el primer uso que le doy a este handler,
            //se le puede añadir para casos de otros forms como "ORs".
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Se encuentra en Montevideo?\n");
                response = sb.ToString();
                
                if (CurrentForm is FrmAltaOferta)
                {
                (CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fasesAltaOferta.ArmandoLocation;
                (CurrentForm as FrmAltaOferta).CurrentStateLocation = faseLocation.tomandoMvdeo;
                }
                if (CurrentForm is FrmRegistroEmpresa)
                {
                (CurrentForm as FrmRegistroEmpresa).CurrentState = HandlerRegEmpresa.fasesRegEmpresa.ArmandoLocation;
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = faseLocation.tomandoMvdeo;
                }

                return true;
            }
            else if (((CurrentForm as FrmAltaOferta).CurrentStateLocation == faseLocation.tomandoMvdeo && message.TxtMensaje.ToLower() == "si") || ((CurrentForm as FrmRegistroEmpresa).CurrentStateLocation == faseLocation.tomandoMvdeo && message.TxtMensaje.ToLower() == "si"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ok, dinos la direccion (Con calle y numero de puerta o Km basta).\n");
                response = sb.ToString();

                if (CurrentForm is FrmAltaOferta)
                {
                (CurrentForm as FrmAltaOferta).CurrentStateLocation = faseLocation.tomandoDireccion;
                }
                if (CurrentForm is FrmRegistroEmpresa)
                {
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = faseLocation.tomandoDireccion;
                }
                return true;
            }
            else if (((CurrentForm as FrmAltaOferta).CurrentStateLocation == faseLocation.tomandoMvdeo && message.TxtMensaje.ToLower() == "no") || ((CurrentForm as FrmRegistroEmpresa).CurrentStateLocation == faseLocation.tomandoMvdeo && message.TxtMensaje.ToLower() == "no"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿En que departamento se halla? \n");
                response = sb.ToString();

                if (CurrentForm is FrmAltaOferta)
                {
                (CurrentForm as FrmAltaOferta).CurrentStateLocation = faseLocation.tomandoDpto;
                }
                if (CurrentForm is FrmRegistroEmpresa)
                {
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = faseLocation.tomandoDpto;
                }        

                return true;
            }
            else if (((CurrentForm as FrmAltaOferta).CurrentStateLocation == faseLocation.tomandoDpto) || ((CurrentForm as FrmRegistroEmpresa).CurrentStateLocation == faseLocation.tomandoDpto))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿En que ciudad?");
                response = sb.ToString();

                if (CurrentForm is FrmAltaOferta)
                {
                (CurrentForm as FrmAltaOferta).CurrentStateLocation = faseLocation.tomandoCity;
                (CurrentForm as FrmAltaOferta).dpto = message.TxtMensaje;
                }
                if (CurrentForm is FrmRegistroEmpresa)
                {
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = faseLocation.tomandoCity;
                (CurrentForm as FrmRegistroEmpresa).dpto = message.TxtMensaje;
                }  

                return true;
            }
            else if (((CurrentForm as FrmAltaOferta).CurrentStateLocation == faseLocation.tomandoCity) || ((CurrentForm as FrmRegistroEmpresa).CurrentStateLocation == faseLocation.tomandoCity))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Excelente, Ahora si dinos calle y numero de puerta o km.");
                response = sb.ToString();

                if (CurrentForm is FrmAltaOferta)
                {
                (CurrentForm as FrmAltaOferta).CurrentStateLocation = faseLocation.tomandoDireccion;
                (CurrentForm as FrmAltaOferta).city = message.TxtMensaje;
                }
                if (CurrentForm is FrmRegistroEmpresa)
                {
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = faseLocation.tomandoDireccion;
                (CurrentForm as FrmRegistroEmpresa).city = message.TxtMensaje;
                }
                return true;
            }
            else if (((CurrentForm as FrmAltaOferta).CurrentStateLocation == faseLocation.tomandoDireccion) || ((CurrentForm as FrmRegistroEmpresa).CurrentStateLocation == faseLocation.tomandoDireccion))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Creada y guardada la ubicación.");
                response = sb.ToString();

                if (CurrentForm is FrmAltaOferta)
                {
                (CurrentForm as FrmAltaOferta).CurrentStateLocation = faseLocation.Inicio;
                (CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fasesAltaOferta.Eligiendo;
                (CurrentForm as FrmAltaOferta).direccion = message.TxtMensaje;
                }
                if (CurrentForm is FrmRegistroEmpresa)
                {
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = faseLocation.Inicio;
                (CurrentForm as FrmRegistroEmpresa).direccion = message.TxtMensaje;
                }
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