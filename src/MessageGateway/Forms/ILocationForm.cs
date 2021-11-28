using MessageGateway.Handlers;
using ClassLibrary.LocationAPI;

namespace MessageGateway.Forms
{
    public interface ILocationForm
    {
        string direccion { get; set; }
        string city { get; set; }
        string dpto { get; set; }
        HandlerLocation.faseLocation CurrentStateLocation { get; set; }
        Location Ubicacion { get; }
    }
}