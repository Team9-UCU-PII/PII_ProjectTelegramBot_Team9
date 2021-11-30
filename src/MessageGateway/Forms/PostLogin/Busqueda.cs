//--------------------------------------------------------------------------------
// <copyright file="Busqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;
using BotCore.Publication.Filters;
using ClassLibrary.User;
using BotCore.Publication;
using MessageGateway.Handlers;
using MessageGateway.Handlers.ListadoPublicaciones;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que recopilara la información necesaria para buscar las ofertas según los filtros.
    /// </summary>
    public class FrmBusqueda : FormularioBase, IPostLogin, IListableForm
    {
        /// <summary>
        /// Constructor del formulario de búsqueda de ofertas con sus handlers.
        /// </summary>
        public FrmBusqueda(IUsuario user)
        {
            CurrentState = HandlerBusqueda.FasesBusqueda.Inicio;
            this.InstanciaLoggeada = user;
            this.messageHandler =
            new HandlerBusqueda(
                new HandlerListadoPublicaciones(null)
            );
        }

        /// <summary>
        /// Departamento a buscarse.
        /// </summary>
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

        /// <summary>
        /// Obtiene o establece los filtros que se desean utilizar.
        /// </summary>
        /// <value>IFiltroBUsqueda.</value>
        public IFiltroBusqueda cadenaFilters {get; set;}

        /// Guarda las publicaciones filtradas.
        /// </summary>
        /// <value></value>
        public List<Publicacion> publicacionesFiltradas 
        {
            get
            {
               return Busqueda.Instancia.BuscarPublicaciones(this.cadenaFilters); 
            }
            set {}
        }

        public Publicacion publicacionSeparada {get; set;}

        public HandlerListadoPublicaciones.fasesListado CurrentStateListado {get; set;}

        /// <summary>
        /// Metodo que crea una cadena de filtros nueva, o añade las solicitadas.
        /// </summary>
        /// <param name="filtro">IFiltroBusqueda.</param>
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