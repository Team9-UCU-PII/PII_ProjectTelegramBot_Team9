using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public class FrmBusqueda : FormularioBase, IFormulario
    {
        public string Nombre;

        public FrmBusqueda()
        {
            this.messageHandler =
            new HandlerBusqueda(
                new HandlerEscape(null)
            );
        }

        public HandlerBusqueda.fases CurrentState {get; set;}
    }
}