using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public interface IFormulario
    {
        string ReceiveMessage(IMessage mensaje);
        void ChangeForm(IFormulario next, string chatID);
        
    }
}