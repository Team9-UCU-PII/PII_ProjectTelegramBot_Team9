//--------------------------------------------------------------------------------
// <copyright file="AltaOferta.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;
using System.Collections.Generic;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que recopilara la información necesaria para crear una publicacion.
    /// </summary>
    public class FrmAltaOferta : FormularioBase, IFormulario, ILocationForm, IResiduoForm
    {

        /// <summary>
        /// Oferta resultante.
        /// </summary>
        /// <value>Publicacion.</value>
        public Publicacion Oferta
        {
            get
            {
                return (Vendedor.CrearOferta(residuo,PrecioUnitario,Moneda,Cantidad,Ubicacion,Descripcion, residuo.Categoria));
            }
        }

        /// <summary>
        /// Precio por unidad del residuo.
        /// </summary>
        public double PrecioUnitario;

        /// <summary>
        /// Moneda que la caracterizará.
        /// </summary>
        public string Moneda;

        /// <summary>
        /// Cantidad del residuo segun la unidad dada.
        /// </summary>
        public int Cantidad;

        /// <summary>
        /// Ubicación del residuo.
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
        /// La empresa vendedora.
        /// </summary>
        public Empresa Vendedor;

        /// <summary>
        /// Descripcion de la publicación.
        /// </summary>
        public string Descripcion;
        
        /// <summary>
        /// Constructor del formulario creador de publicaciones con sus handlers.
        /// </summary>
        public FrmAltaOferta()
        {
            this.messageHandler =
            new HandlerAltaOferta(
                new HandlerNewResiduo(
                    new HandlerLocation(null)
                )
            );
        }

        /// <summary>
        /// El estado del formulario, dado por su handler principal.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerAltaOferta.fasesAltaOferta.</value>
        public HandlerAltaOferta.fasesAltaOferta CurrentState { get; set; }

        /// <summary>
        /// El estado del formulario, para la subconstruccion del residuo.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerNewResiduo.fasesResiduo.</value>
        public HandlerNewResiduo.fasesResiduo CurrentStateResiduo {get; set; }

        /// <summary>
        /// El residuo creado a partir de las propiedades de IResiduoForm.
        /// </summary>
        /// <value><see cref = "Residuo" />.</value>
        public Residuo residuo {
            get
            {
                return new Residuo(categoria, descripcion,unit,habilitaciones);
            }
        }

        /// <summary>
        /// Descripcion para el residuo.
        /// </summary>
        /// <value>String.</value>
        public string descripcion { get; set; }

        /// <summary>
        /// Unidad de medida del residuo.
        /// </summary>
        /// <value>String.</value>
        public string unit { get; set; }

        /// <summary>
        /// Categoria del residuo (tambien abarca la de la publicación en si).
        /// </summary>
        /// <value></value>
        public Categoria categoria { get; set; }

        /// <summary>
        /// Habilitaciones para el residuo.
        /// </summary>
        /// <value>List de Habilitacion</value>
        public List<Habilitacion> habilitaciones { get; set; }

        /// <summary>
        /// El estado del formulario respecto la construccion de Location.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerLocation.faseLocation.</value>
        public HandlerLocation.faseLocation CurrentStateLocation { get; set; }

        /// <summary>
        /// Ciudad donde se retira lo publicado.
        /// </summary>
        /// <value>String.</value>
        public string city { get; set; }

        /// <summary>
        /// Departamento donde se retira lo publicado.
        /// </summary>
        /// <value>String.</value>
        public string dpto { get; set; }

        /// <summary>
        /// Direccion donde se retira lo publicado.
        /// </summary>
        /// <value>String.</value>
        public string direccion { get; set; }

    }
}