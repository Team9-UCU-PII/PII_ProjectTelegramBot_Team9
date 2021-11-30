//--------------------------------------------------------------------------------
// <copyright file="HandlerBusqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;
using ClassLibrary.User;
using BotCore.Publication.Filters;
using ClassLibrary.Publication;

namespace MessageGateway.Handlers
{
    /// <summary>
    /// Handler principal de la búsqueda de ofertas.
    /// </summary>
    public class HandlerBusqueda : MessageHandlerBase, IMessageHandler
    {
        /// <summary>
        /// Constructor, al instanciarse en un form ya le asigna a este sus estados iniciales necesarios.
        /// </summary>
        /// <param name="next">Siguiente IHandler.</param>
        public HandlerBusqueda(IMessageHandler next) : base ((new string[] {"Busqueda"}), next)
        {
            this.Next = next;
        }

        /// <summary>
        /// Internal handle que va mostrando los distintos tipos de filtros.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"A continuación, te daremos una serie de filtros por los que puedes buscar publicaciones...\n");
                sb.Append($"¿Quieres filtrar las publicaciones por empresa?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroEmpresa;
                return true;
            }
            else if (message.TxtMensaje.ToLower() == "si" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroEmpresa)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿De que empresa buscas?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.tomandoFiltroEmpresa;
                return true;
            }
            else if ((message.TxtMensaje == "no" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroEmpresa) || ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroEmpresa))
            {
                StringBuilder sb = new StringBuilder();

                if((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroEmpresa)
                {
                sb.AppendLine("Añadido filtro. \n");
                (CurrentForm as FrmBusqueda).AddFilter(new FiltroPorEmpresa(new Empresa(message.TxtMensaje)));
                }

                sb.Append($"¿Quieres filtrar las publicaciones por categoría?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroCategoria;
                return true;
            }
            else if (message.TxtMensaje.ToLower() == "si" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroCategoria)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿De que categoría buscas?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.tomandoFiltroCategoria;
                return true;
            }
            else if ((message.TxtMensaje.ToLower() == "no" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroCategoria) || (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroCategoria)
            {
                StringBuilder sb = new StringBuilder();

                if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroCategoria)
                {
                    sb.AppendLine("Añadido el filtro");
                    (CurrentForm as FrmBusqueda).AddFilter(new FiltroPorCategoria(new Categoria(message.TxtMensaje)));
                }

                sb.Append($"¿Quieres filtrar las publicaciones por lugar de retiro?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroLugarRetiro;
                return true;
            }
            else if (message.TxtMensaje.ToLower() == "si" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroLugarRetiro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿De que departamento buscas?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.tomandoFiltroLugarRetiro1;
                return true;
            }
            else if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroLugarRetiro1)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmBusqueda).dpto = message.TxtMensaje;
                sb.Append($"¿De que ciudad buscas?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.tomandoFiltroLugarRetiro2;
                return true;
            }
            else if (((message.TxtMensaje.ToLower() == "no" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroLugarRetiro)) || (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroLugarRetiro2)
            {
                StringBuilder sb = new StringBuilder();

                if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroLugarRetiro2)
                {
                    sb.AppendLine("Añadido el filtro");
                    (CurrentForm as FrmBusqueda).AddFilter(new FiltroPorLugarRetiro((message.TxtMensaje), (CurrentForm as FrmBusqueda).dpto));
                }

                sb.Append($"¿Quieres filtrar las publicaciones por precio máximo a pagar?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroPrecioMaximo;
                return true;
            }
            else if (message.TxtMensaje.ToLower() == "si" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroPrecioMaximo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Hasta que valor?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.tomandoFiltroPrecioMaximo;
                return true;
            }
            else if ((message.TxtMensaje.ToLower() == "no" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroPrecioMaximo) || (CurrentForm as FrmBusqueda).CurrentState==FasesBusqueda.tomandoFiltroPrecioMaximo)
            {
                StringBuilder sb = new StringBuilder();

                if((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroPrecioMaximo)
                {
                    if (double.TryParse(message.TxtMensaje, out double valor))
                    {
                        sb.AppendLine("Añadido el filtro.");
                        (CurrentForm as FrmBusqueda).AddFilter(new FiltroPorPrecioMaximo(valor));
                    }
                    else
                    {
                        sb.Append("Ingresa un valor numerico maximo.");
                        response = sb.ToString();
                        return true;
                    }
                }

                sb.Append($"¿Quieres filtrar las publicaciones por frecuencia de restock?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroFrecuenciaRestock;
                return true;
            }
            else if (message.TxtMensaje.ToLower() == "si" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroFrecuenciaRestock)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Con que frecuencia anual?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.tomandoFiltroFrecuenciaRestock;
                return true;
            }
            else if ((message.TxtMensaje.ToLower() == "no" && (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroFrecuenciaRestock)
            || (CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroFrecuenciaRestock)
            {
                StringBuilder sb = new StringBuilder();

                if((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.tomandoFiltroFrecuenciaRestock)
                {
                    if (int.TryParse(message.TxtMensaje, out int valor))
                    {
                        sb.Append("Añadido el filtro.");
                        (CurrentForm as FrmBusqueda).AddFilter(new FiltroPorFrecuenciaRestock(valor));
                    }
                    else
                    {
                        sb.Append("Ingresa un valor numerico maximo.");
                        response = sb.ToString();
                        return true;
                    }
                }

                sb.Append($"¿Listo para buscar?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.Done;
                (CurrentForm as IListableForm).CurrentStateListado = ListadoPublicaciones.HandlerListadoPublicaciones.fasesListado.Inicio;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Las diferentes fases que este handler necesita para completar toda su información.
        /// </summary>
        public enum FasesBusqueda
        {
            /// <summary>
            /// Iniciador del handler.
            /// </summary>
            Inicio,

            /// <summary>
            /// El user está parado en el menú.
            /// </summary>
            Eligiendo,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por empresa.
            /// </summary>
            FiltroEmpresa,

            ///Se espera si se toma el filtro Empresa.
            tomandoFiltroEmpresa,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por ecategoría.
            /// </summary>
            FiltroCategoria,

            ///Esperando el Filtro de Categoria
            tomandoFiltroCategoria,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por lugar de retiro.
            /// </summary>
            FiltroLugarRetiro,

            ///Primera parte para tomar el lugar de retiro.
            tomandoFiltroLugarRetiro1,

            ///segunda parte, ya tomado el lugar.
            tomandoFiltroLugarRetiro2,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por residuo.
            /// </summary>
            FiltroResiduo,

            ///esperando el residuo a buscarse.
            tomandoFiltroResiduo,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por precio máximo.
            /// </summary>
            FiltroPrecioMaximo,

            ///tomando el valor maximo dispuesto a pagar.
            tomandoFiltroPrecioMaximo,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por frecuencia de restock.
            /// </summary>
            FiltroFrecuenciaRestock,

            ///Esperando cuantas veces al año se desea que se restockee.
            tomandoFiltroFrecuenciaRestock,

            ///Finalizado el filtrado y handler.
            Done
        }
    }
}