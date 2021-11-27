using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public class FrmRegistroEmprendedor : FormularioBase, IFormulario
    {
        public string Nombre;

        public Location lugar;

        public string Rubro;

        public string Especializacion;

        public List<Habilitacion> Habilitaciones;

        public FrmRegistroEmprendedor()
        {
            this.messageHandler =
            new HandlerRegistroEmprendedor(
                new HandlerLocation(
                    new HandlerEscape(null)
                )
            );
        }

        public HandlerRegistroEmprendedor.fases CurrentState {get; set;}
    }
}