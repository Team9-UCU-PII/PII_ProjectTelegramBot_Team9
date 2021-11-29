//--------------------------------------------------------------------------------
// <copyright file="Busqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que recopilara la información necesaria para buscar las ofertas según los filtros.
    /// </summary>
    public class FrmBusqueda : FormularioBase
    {

        /// <summary>
        /// Constructor del formulario de búsqueda de ofertas con sus handlers.
        /// </summary>
        public FrmBusqueda()
        {
            this.messageHandler =
            new HandlerBusqueda((null));
        }

        /// <summary>
        /// El estado del formulario, dado por su handler principal.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerBusqueda.FasesBusqueda.</value>
        public HandlerBusqueda.FasesBusqueda CurrentState {get; set;}
    }
}