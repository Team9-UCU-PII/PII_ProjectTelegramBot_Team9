//--------------------------------------------------------------------------------
// <copyright file="ILocationForm.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers;
using ClassLibrary.LocationAPI;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Interfaz que engloba todos los formularios que necesitan una instancia de Location.
    /// </summary>
    public interface ILocationForm
    {

        /// <summary>
        /// La direccion, calle y puerta.
        /// </summary>
        /// <value>String.</value>
        string direccion { get; set; }

        /// <summary>
        /// La ciudad.
        /// </summary>
        /// <value>String.</value>
        string city { get; set; }

        /// <summary>
        /// El departamento.
        /// </summary>
        /// <value>String.</value>
        string dpto { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de construccion del Location.
        /// </summary>
        /// <value></value>
        HandlerLocation.faseLocation CurrentStateLocation { get; set; }

        /// <summary>
        /// Instancia de Location Construida a partir del resto de propiedades.
        /// </summary>
        /// <value></value>
        Location Ubicacion { get; }
    }
}