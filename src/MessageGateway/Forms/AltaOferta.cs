using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public class FrmAltaOferta : FormularioBase, IFormulario
    {
        public Residuo residuo;

        public double PrecioUnitario;

        public string Moneda;

        public int Cantidad;

        public Location lugarRetiro;
    
        public Empresa Vendedor;

        public string Descripcion;

        public Categoria Categoria;

        public FrmAltaOferta()
        {
            this.CurrentState = possibleStates.Inicio;
            this.messageHandler =
            new HandlerAltaOferta(
                new HandlerNewResiduo(
                    new HandlerLocation(
                            new HandlerEscape(null)
                    )
                )
            );
        }
        public possibleStates CurrentState {get; set;}
        public enum possibleStates
        {
            Inicio,
            EsperandoData,
            Done
        }

    }
}