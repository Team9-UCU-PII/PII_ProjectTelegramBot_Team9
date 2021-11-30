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
using MessageGateway.Handlers.ListadoPublicaciones;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que buscara las ofertas de la empresa y permite cambiarlas en la base de datos.
    /// </summary>
    public class FrmModOferta : FormularioBase, IPostLogin, IListableForm
    {
        /// <summary>
        /// Constructor del formulario de Modificar Oferta de ofertas con sus handlers.
        /// </summary>
        public FrmModOferta(IUsuario user)
        {
            CurrentState = HandlerListadoPublicaciones.fasesListado.Inicio;
            this.InstanciaLoggeada = user;
            this.messageHandler =
                new HandlerListadoPublicaciones(null);
        }

        /// <summary>
        /// El estado del formulario, dado por su handler principal.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerListadoPublicaciones.fasesListado.</value>
        public HandlerListadoPublicaciones.fasesListado CurrentState {get; set;}

        /// <summary>
        /// Obtiene o establece el Emprendedor iniciado.
        /// </summary>
        /// <value>Emprendedor.</value>
        public IUsuario InstanciaLoggeada {get; set;}

        /// <summary>
        /// Obtiene el filtro de empresa para ver sus publicaciones unicamente.
        /// </summary>
        /// <value>IFiltroBusqueda.</value>
        public IFiltroBusqueda cadenaFilters 
        {
            get
            {
                return new FiltroPorEmpresa(InstanciaLoggeada as Empresa);
            }
        }

        ///<summary>
        /// Guarda las publicaciones de la empresa loggeada unicamente.
        /// </summary>
        /// <value>List de Publicacion.</value>
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
    }
}