using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using System.Collections.Generic;

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
        : base (new Dictionary<string, string> {})
        {
            this.messageHandler =
            new HandlerResiduo(
                new HandlerPrecio(
                    new HandlerMoneda(
                        new HandlerCant(
                            new HandlerLocation(
                                new HandlerDescripcion(
                                    new HandlerCategoria(null)
                                )
                            )
                        )
                    )
                )
            );
        }

    }
}