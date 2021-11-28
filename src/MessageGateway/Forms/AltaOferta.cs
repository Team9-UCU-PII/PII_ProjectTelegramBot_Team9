using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;
using MessageGateway.Handlers.Escape;
using System.Collections.Generic;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que recopilara la información necesaria para crear una publicacion.
    /// </summary>
    public class FrmAltaOferta : FormularioBase, IFormulario, ILocationForm, IResiduoForm
    {

        public double PrecioUnitario;

        public string Moneda;

        public int Cantidad;

        public Location Ubicacion
        {
            get
            {
                return LocationApiClient.Instancia.GetLocation(direccion,city,dpto);
            }
        }
    
        public Empresa Vendedor;

        public string Descripcion;

        public Categoria Categoria;
        

        public FrmAltaOferta()
        {
            this.messageHandler =
            new HandlerAltaOferta(
                new HandlerNewResiduo(
                    new HandlerLocation(
                            new HandlerEscape(null)
                    )
                )
            );
        }
        public HandlerAltaOferta.fasesAltaOferta CurrentState { get; set; }
        public HandlerNewResiduo.fasesResiduo CurrentStateResiduo {get; set; }
        public Residuo residuo {
            get
            {
                return new Residuo(categoria, descripcion,unit,habilitaciones);
            }
        }
        public string descripcion { get; set; }
        public string unit { get; set; }
        public Categoria categoria { get; set; }
        public List<Habilitacion> habilitaciones { get; set; }

        public HandlerLocation.faseLocation CurrentStateLocation { get; set; }
        public string city { get; set; }        
        public string dpto { get; set; }
        public string direccion { get; set; }

    }
}