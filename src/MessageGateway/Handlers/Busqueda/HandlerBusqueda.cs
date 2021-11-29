//--------------------------------------------------------------------------------
// <copyright file="HandlerBusqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Text;
using MessageGateway.Forms;

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
            else if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroEmpresa)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por categoría?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroCategoria;
                return true;
            }
            else if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroCategoria)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por lugar de retiro?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroLugarRetiro;
                return true;
            }
            else if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroLugarRetiro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por tipo de residuo?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroResiduo;
                return true;
            }
            else if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroResiduo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por precio máximo a pagar?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroPrecioMaximo;
                return true;
            }
            else if ((CurrentForm as FrmBusqueda).CurrentState == FasesBusqueda.FiltroPrecioMaximo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por frecuencia de restock?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = FasesBusqueda.FiltroFrecuenciaRestock;
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

            /// <summary>
            /// Se espera respuesta si quiere filtrar por ecategoría.
            /// </summary>
            FiltroCategoria,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por lugar de retiro.
            /// </summary>
            FiltroLugarRetiro,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por residuo.
            /// </summary>
            FiltroResiduo,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por precio máximo.
            /// </summary>
            FiltroPrecioMaximo,

            /// <summary>
            /// Se espera respuesta si quiere filtrar por frecuencia de restock.
            /// </summary>
            FiltroFrecuenciaRestock
        }
    }
}