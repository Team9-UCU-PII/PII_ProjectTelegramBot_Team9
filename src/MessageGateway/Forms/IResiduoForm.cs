using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Handlers;
using System.Collections.Generic;

namespace MessageGateway.Forms
{
    public interface IResiduoForm
    {
        Residuo residuo { get; set; }
        string descripcion { get; set; }
        string unit { get; set; }
        Categoria categoria { get; set; }
        List<Habilitacion> habilitaciones { get; set; }
        HandlerNewResiduo.fasesResiduo CurrentStateResiduo { get; set; }
    }
}