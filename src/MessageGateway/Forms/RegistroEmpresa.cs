using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public class FrmRegistroEmpresa : FormularioBase, IFormulario
    {
        public string Nombre;

        public Location lugar;

        public string Rubro;

        public string Descripcion;

        public string Contacto;

        public FrmRegistroEmpresa()
        {
            this.messageHandler =
            new HandlerRegistroEmprendedor(
                new HandlerLocation(
                    new HandlerEscape(null)
                )
            );
        }

        public FrmRegistroEmpresa.fases CurrentState {get; set;}
    }
}