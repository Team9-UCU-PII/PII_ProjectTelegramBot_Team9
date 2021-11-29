//--------------------------------------------------------------------------------
// <copyright file="HandlerLocation.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{

    /// <summary>
    /// Handler encargado de tomar datos para generar un Location.
    /// </summary>
    public class HandlerLocation: MessageHandlerBase, IMessageHandler
    {

        /// <summary>
        /// Constructor. La palabra clave dada es para el menu de AltaOferta
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public HandlerLocation(IMessageHandler next) : base ((new string[] {"3", "2"}), next)
        {
            this.Next = next;
        }

        /// <summary>
        /// Internal Handle, va tomando los strings necesarios para el GetLocation del API.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns></returns>
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

                (CurrentForm as ILocationForm).CurrentStateLocation = faseLocation.Done;
                (CurrentForm as ILocationForm).direccion = message.TxtMensaje;

                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }

        }

        /// <summary>
        /// Fases necesarias para crear un Location.
        /// </summary>
        public enum faseLocation
        {

            ///Fase de entrada por Menu
            Inicio,

            ///Revisando si es en montevideo o no (atajo.)
            tomandoMvdeo,

            ///Esperando el departamento.
            tomandoDpto,

            ///Esperando la ciudad.
            tomandoCity,

            ///Esperando la calle y puerta (o Km).
            tomandoDireccion,
            
            ///Se termino de formar la ubicacion.
            Done
        }
    }
}