using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public interface IMessageHandler
    {
        IFormulario CurrentForm { get; set; }
        IMessageHandler Next { get; set; }
        IMessageHandler Handle(IMessage mensaje, out string response);
    }
}