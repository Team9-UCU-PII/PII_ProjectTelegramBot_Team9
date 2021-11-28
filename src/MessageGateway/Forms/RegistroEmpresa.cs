using ClassLibrary.User;
using ClassLibrary.LocationAPI;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public class FrmRegistroEmpresa : FormularioBase , ILocationForm
    {
        public string NombrePublico;
        public Location Ubicacion
        {
            get
            {
                return LocationApiClient.Instancia.GetLocation(direccion,city,dpto);
            }
        }
        public string Rubro;
        public string Descripcion;
        public string Contacto;
        public Empresa EmpresaPreCreada;
        public string dpto { get; set; }
        public string city { get; set; }
        public string direccion { get; set; }

        public FrmRegistroEmpresa()
        {
            CurrentState = HandlerRegEmpresa.fasesRegEmpresa.Inicio;
            this.messageHandler =
                new HandlerRegEmpresa(
                    new HandlerLocation(null)
                );
        }
        public HandlerRegEmpresa.fasesRegEmpresa CurrentState;
        public HandlerLocation.faseLocation CurrentStateLocation { get; set; }
    }
}