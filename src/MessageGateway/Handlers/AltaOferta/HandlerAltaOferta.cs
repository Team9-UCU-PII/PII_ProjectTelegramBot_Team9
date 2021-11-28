//--------------------------------------------------------------------------------
// <copyright file="HandlerAltaOferta.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;
using ClassLibrary.LocationAPI;

namespace MessageGateway.Handlers
{

    /// <summary>
    /// Handler principal de la creacion de publicaciones.
    /// </summary>
    public class HandlerAltaOferta: MessageHandlerBase, IMessageHandler
    {

        /// <summary>
        /// Constructor, al instanciarse en un form ya le asigna a este sus estados iniciales necesarios.
        /// </summary>
        /// <param name="next">Siguiente IHandler</param>
        public HandlerAltaOferta(IMessageHandler next) : base ((new string[] {"CrearOferta"}), next)
        {
            this.Next = next;
            (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Inicio;
            (CurrentForm as FrmAltaOferta).CurrentStateLocation = HandlerLocation.faseLocation.Inicio;
            (CurrentForm as FrmAltaOferta).CurrentStateResiduo = HandlerNewResiduo.fasesResiduo.Inicio;
        }

        /// <summary>
        /// Internal handle que presenta un menu para ir completando la creacion de oferta.
        /// Delega tareas de creacion de residuo y location a sus handlers particulares.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Vamos a crear una publicación...\n");
                sb.Append ($"¿Qué quieres especificar?\n ");
                sb.Append ($"1.Residuo\n");
                sb.Append ($"2.Costo y Cantidad\n");
                sb.Append ($"3.Lugar de Retiro\n");
                sb.Append ($"4.Descripción\n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "2" && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Que cantidad del residuo dispones? (Segun la unidad dada en la especificacion del residuo)");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoCantidad;
                return true;
            }
            else if (faseActual == fasesAltaOferta.tomandoCantidad)
            {
                StringBuilder sb = new StringBuilder();
                if (int.TryParse(message.TxtMensaje, out int result))
                {
                    (CurrentForm as FrmAltaOferta).Cantidad = result;
                    (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoMoneda;
                    sb.Append($"Añadida la cantidad. Ahora ingresa la moneda que aceptas ($ como pesos, USD como dolares, etc...)");
                    response = sb.ToString();
                    return true;
                }
                else
                {
                    sb.Append($"Intenta de nuevo, asegurate ingresar solo un numero.");
                    response = sb.ToString();
                    return true;
                }
            }
            else if (faseActual == fasesAltaOferta.tomandoMoneda)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmAltaOferta).Moneda = message.TxtMensaje;
                sb.Append($"Añadida la moneda, ahora dime el valor unitario (puede ser decimal, con un punto, como ejemplo: 2.5)");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoPrecio;
                return true;
            }
            else if ((faseActual == fasesAltaOferta.tomandoPrecio))
            {
                StringBuilder sb = new StringBuilder();
                if (double.TryParse(message.TxtMensaje, out double result))
                {
                    (CurrentForm as FrmAltaOferta).PrecioUnitario = result;
                    (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                    sb.Append($"Listo");
                    response = sb.ToString();
                    return true;
                }
                else
                {
                    sb.Append($"Intenta de nuevo, asegurate ingresar solo un numero y con punto decimal de ser necesario.");
                    response = sb.ToString();
                    return true;
                }
            }
            else if (message.TxtMensaje == "4" && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Como describirías la publicación?");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoDescripcion;
                return true;
            }
            else if (faseActual == fasesAltaOferta.tomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Guardada la descripción");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).Descripcion = message.TxtMensaje;
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        private fasesAltaOferta faseActual;

        /// <summary>
        /// Las diferentes fases que este handler necesita para completar toda su información
        /// </summary>
        public enum fasesAltaOferta
        {

            /// <summary>
            /// Iniciador del handler.
            /// </summary>
            Inicio,

            /// <summary>
            /// El user esta parado en el menu.
            /// </summary>
            Eligiendo,

            /// <summary>
            /// Se espera la cantidad en el proximo msj.
            /// </summary>
            tomandoCantidad,

            /// <summary>
            /// Se espera la moneda.
            /// </summary>
            tomandoMoneda,

            /// <summary>
            /// Se espera el precio por unidad.
            /// </summary>
            tomandoPrecio,

            /// <summary>
            /// Se espera una descripción.
            /// </summary>
            tomandoDescripcion
        }

        /// <summary>
        /// String para armar el location, por defecto es Mvdeo el departamento.
        /// </summary>
        public string dpto = "Montevideo";
    
        /// <summary>
        /// String para armar el Location, por defecto es Mvdeo la ciudad.
        /// </summary>
        public string city = "Montevideo";

        /// <summary>
        /// Instancia Location en si formada.
        /// </summary>
        public Location locationFinal;

    }
}