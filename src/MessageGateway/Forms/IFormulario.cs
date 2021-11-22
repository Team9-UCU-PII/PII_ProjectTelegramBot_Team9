using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public interface IFormulario
    {
        IFormulario Next { set; }
        PalabrasClaveHandlers NextMessageKeyword { set; }
        string ReceiveMessage(IMessage mensaje);
    }
}