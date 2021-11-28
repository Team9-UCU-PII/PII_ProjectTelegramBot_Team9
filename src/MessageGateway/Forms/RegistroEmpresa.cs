//--------------------------------------------------------------------------------
// <copyright file="RegistroEmpresa.cs" company="Universidad Católica del Uruguay">
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
    /// Formulario que recopilara la información necesaria para registrar una empresa.
    /// </summary>
    public class FrmRegistroEmpresa : FormularioBase, IFormulario, ILocationForm
    {
        /// <summary>
        /// Nombre de la empresa.
        /// </summary>
        public string Nombre;

        /// <summary>
        /// Ubicación de la empresa.
        /// </summary>
        /// <value><see cref = "Location" />.</value>
        public Location Ubicacion
        {
            get
            {
                return LocationApiClient.Instancia.GetLocation(direccion,city,dpto);
            }
        }

        /// <summary>
        /// Rubro de la empresa.
        /// </summary>
        public string Rubro;

        /// <summary>
        /// Descripción de la empresa.
        /// </summary>
        public string Descripcion;

        /// <summary>
        /// Contacto de la empresa.
        /// </summary>
        public string Contacto;

        /// <summary>
        /// Constructor del formulario de registro de las empresas con sus handlers.
        /// </summary>
        public FrmRegistroEmpresa()
        {
            this.messageHandler =
            new HandlerRegistroEmprendedor(
                new HandlerLocation(
                    new HandlerEscape(null)
                )
            );
        }

        /// <summary>
        /// El estado del formulario, dado por su handler principal.
        /// </summary>
        /// <value><see langword = "enum"/> de FrmRegistroEmpresa.fases.</value>
        public HandlerRegistroEmpresa.fases CurrentState {get; set;}

        /// <summary>
        /// El estado del formulario respecto la construccion de Location.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerLocation.faseLocation.</value>
        public HandlerLocation.faseLocation CurrentStateLocation { get; set; }

        /// <summary>
        /// Ciudad donde se encuentra el emprendedor.
        /// </summary>
        /// <value>String.</value>
        public string city { get; set; }

        /// <summary>
        /// Departamento donde se encuentra el emprendedor.
        /// </summary>
        /// <value>String.</value>
        public string dpto { get; set; }

        /// <summary>
        /// Dirección donde se encuentra el emprendedor.
        /// </summary>
        /// <value>String.</value>
        public string direccion { get; set; }
    }
}