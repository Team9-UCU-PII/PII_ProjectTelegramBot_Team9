using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public interface IFormulario
    {
        IFormulario Next { set; }
        string NextMessageKeyword { set; }
        string ReceiveMessage(IMessage mensaje);
    }
}