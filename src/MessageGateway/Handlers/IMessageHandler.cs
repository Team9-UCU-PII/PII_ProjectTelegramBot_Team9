using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public interface IMessageHandler
    {
        IFormulario ContainingForm { get; set; }
        IMessageHandler Next { get; set; }
        IMessageHandler Handle(IMessage mensaje, out string response, out string nextHandlerKeyword);
    }
}