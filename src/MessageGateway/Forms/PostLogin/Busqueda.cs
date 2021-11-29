//--------------------------------------------------------------------------------
// <copyright file="Busqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using BotCore.Publication.Filters;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que recopilara la información necesaria para buscar las ofertas según los filtros.
    /// </summary>
    public class FrmBusqueda : FormularioBase, IPostLogin
    {

        /// <summary>
        /// Constructor del formulario de búsqueda de ofertas con sus handlers.
        /// </summary>
        public FrmBusqueda(IUsuario user)
        {
            CurrentState = HandlerBusqueda.FasesBusqueda.Inicio;
            this.InstanciaLoggeada = user;
            this.messageHandler =
            new HandlerBusqueda((null));
        }

        public string dpto;

        /// <summary>
        /// El estado del formulario, dado por su handler principal.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerBusqueda.FasesBusqueda.</value>
        public HandlerBusqueda.FasesBusqueda CurrentState {get; set;}

        /// <summary>
        /// Obtiene o establece el Emprendedor iniciado.
        /// </summary>
        /// <value>Emprendedor.</value>
        public IUsuario InstanciaLoggeada {get; set;}

        public IFiltroBusqueda cadenaFilters {get; set;}
        public void AddFilter(IFiltroBusqueda filtro)
        {
            if (this.cadenaFilters == null)
            {
                this.cadenaFilters = filtro;
            }
            else
            {
                IFiltroBusqueda filter = this.cadenaFilters;
                IFiltroBusqueda nextFilter = filter.Next;
                do
                {
                    filter = filter.Next;
                    nextFilter = nextFilter.Next;
                }
                while (nextFilter!=null);
                filter.Next = filtro;
            }
        }
    }
}