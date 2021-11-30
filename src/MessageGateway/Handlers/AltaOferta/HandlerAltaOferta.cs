//--------------------------------------------------------------------------------
// <copyright file="HandlerAltaOferta.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;
using ClassLibrary.Publication;
using BotCore.Publication;

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
        public HandlerAltaOferta(IMessageHandler next) : base ((new string[] {"crearoferta", "menu"}), next)
        {
            this.Next = next;
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
            if ((CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Inicio 
            || (this.CanHandle(message) && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Vamos a crear/modificar una publicación...\n");
                sb.Append ($"¿Qué quieres especificar?\n ");
                sb.Append ($"1.Residuo\n");
                sb.Append ($"2.Costo y Cantidad\n");
                sb.Append ($"3.Lugar de Retiro\n");
                sb.Append ($"4.Descripción\n");
                sb.Append ($"5.Recurrencia del residuo (si es aplicable).\n");
                sb.Append ($"6.Listo y Publicar");
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
            else if ((CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.tomandoCantidad)
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
            else if ((CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.tomandoMoneda)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmAltaOferta).Moneda = message.TxtMensaje;
                sb.Append($"Añadida la moneda, ahora dime el valor unitario (puede ser decimal, con un punto, como ejemplo: 2.5)");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoPrecio;
                return true;
            }
            else if (((CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.tomandoPrecio))
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
            else if ((CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.tomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Guardada la descripción");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).Descripcion = message.TxtMensaje;
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "5" && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Cada cuantos meses se restockea el residuo?");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoRecurrencia;
                return true;
            }
            else if ((CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.tomandoRecurrencia)
            {
                StringBuilder sb = new StringBuilder();
                if (int.TryParse(message.TxtMensaje, out int result))
                {
                    (CurrentForm as FrmAltaOferta).FrecuenciaRestock = result;
                    (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                    sb.Append($"Listo");
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
            else if (message.TxtMensaje == "6" && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo)
            {
                Publicacion oferta = (CurrentForm as FrmAltaOferta).Oferta;
                Publicador publish = Publicador.Instancia;
                if (oferta != null)
                {
                    if ((CurrentForm as FrmAltaOferta).ofertaModificable == null)
                    {
                        if (oferta is PublicacionRecurrente)
                        {
                            try
                            {
                                publish.PublicarOfertaRecurrente
                                (oferta.Residuo, 
                                oferta.PrecioUnitario, 
                                oferta.Moneda, 
                                oferta.Cantidad, 
                                oferta.LugarRetiro, 
                                oferta.Vendedor, 
                                oferta.Descripcion, 
                                oferta.Categoria, 
                                (oferta as PublicacionRecurrente).FrecuenciaAnualRestock);
                            }
                            catch (System.Exception)
                            {
                                response =  "Esa publicación ya existe";
                                return true;
                            }
                            response = "Creada y Publicada la Oferta, di \"/abortar\" para volver al menu principal...";
                            ((CurrentForm as FrmAltaOferta)).CurrentState = fasesAltaOferta.Done;
                            return true;
                        }
                        else
                        {
                            try
                            {
                            publish.PublicarOferta
                            (oferta.Residuo, 
                            oferta.PrecioUnitario, 
                            oferta.Moneda, 
                            oferta.Cantidad, 
                            oferta.LugarRetiro, 
                            oferta.Vendedor, 
                            oferta.Descripcion, 
                            oferta.Categoria);
                            }
                            catch (System.Exception)
                            {
                                response =  "Esa publicación ya existe";
                                return true;
                            }
                            response = "Creada y Publicada la Oferta, di \"/abortar\" para volver al menu principal...";
                            ((CurrentForm as FrmAltaOferta)).CurrentState = fasesAltaOferta.Done;
                            return true;
                        }
                    }
                    else
                    {
                        if (oferta is PublicacionRecurrente)
                        {
                            publish.ActualizarOfertaRecurrente((CurrentForm as FrmAltaOferta).ofertaModificable as PublicacionRecurrente, oferta as PublicacionRecurrente);
                        }
                        else
                        {
                            publish.ActualizarOfertaRecurrente((CurrentForm as FrmAltaOferta).ofertaModificable as PublicacionRecurrente, oferta as PublicacionRecurrente);
                        }
                    response = "Modificada y RePublicada la Oferta, di \"/abortar\" para volver al menu principal...";
                    ((CurrentForm as FrmAltaOferta)).CurrentState = fasesAltaOferta.Done;
                    return true;
                    }
                }
                else
                {
                    response ="Algo aun no esta completado, si quieres cancelar escribe /Abortar";
                    return true;
                }
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

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
            tomandoDescripcion,

            ///Esperando recurrencia.
            tomandoRecurrencia,

            ///Lista la publicación.
            Done
        }
    }
}