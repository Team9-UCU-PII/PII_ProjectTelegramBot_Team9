using ClassLibrary.User;
using ClassLibrary.LocationAPI;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public class FrmRegistroEmpresa : FormularioBase
    {
        public string NombrePublico;
        public Location Lugar
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
        public string dpto = "Montevideo";
        public string city = "Montevideo";
        public string direccion;

        public FrmRegistroEmpresa()
        {
            CurrentState = HandlerRegEmpresa.fasesRegEmpresa.Inicio;
            this.messageHandler =
                new HandlerRegEmpresa(
                    new HandlerLocation(null)
                );
        }
        public HandlerRegEmpresa.fasesRegEmpresa CurrentState;
        public HandlerLocation.faseLocation CurrentStateLocation;
    }
}