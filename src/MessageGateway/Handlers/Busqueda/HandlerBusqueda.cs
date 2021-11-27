using System;
using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public class HandlerBusqueda : MessageHandlerBase, IMessageHandler
    {
        public HandlerBusqueda(IMessageHandler next) : base ((new string[] {"Busqueda"}), next)
        {
            this.Next = next;
            (CurrentForm as FrmBusqueda).CurrentState = fases.Inicio;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmBusqueda).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"A continuación, te daremos una serie de filtros por los que puedes buscar publicaciones...\n");
                sb.Append($"¿Quieres filtrar las publicaciones por empresa?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = fases.filtroEmpresa;
                return true;
            }
            else if (faseActual == fases.filtroEmpresa)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por categoría?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = fases.filtroCategoria;
                return true;
            }
            else if (faseActual == fases.filtroCategoria)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por lugar de retiro?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = fases.filtroLugarRetiro;
                return true;
            }
            else if (faseActual == fases.filtroLugarRetiro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por tipo de residuo?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = fases.filtroResiduo;
                return true;
            }
            else if (faseActual == fases.filtroResiduo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por precio máximo a pagar?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = fases.filtroPrecioMaximo;
                return true;
            }
            else if (faseActual == fases.filtroPrecioMaximo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Quieres filtrar las publicaciones por frecuencia de restock?\n");
                response = sb.ToString();
                (CurrentForm as FrmBusqueda).CurrentState = fases.filtroFrecuenciaRestock;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        private fases faseActual;
        public enum fases
        {
            Inicio,
            Eligiendo,
            filtroEmpresa,
            filtroCategoria,
            filtroLugarRetiro,
            filtroResiduo,
            filtroPrecioMaximo,
            filtroFrecuenciaRestock
        }
    }
}