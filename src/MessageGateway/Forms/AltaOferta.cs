using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;
using MessageGateway.Handlers.Escape;

namespace MessageGateway.Forms
{
    public class FrmAltaOferta : FormularioBase, IFormulario, ILocationForm
    {
        public Residuo residuo;

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
        public string city { get; set; }        
        public string dpto { get; set; }
        public string direccion { get; set; }

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
        public HandlerAltaOferta.fasesAltaOferta CurrentState {get; set;}
        public HandlerNewResiduo.fasesResiduo CurrentStateResiduo;
        public HandlerLocation.faseLocation CurrentStateLocation {get; set;}

    }
}