using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    public interface IFormulario
    {
        PalabrasClaveHandlers NextMessageKeyword { set; }
        string ReceiveMessage(IMessage mensaje);
        void ChangeForm(IFormulario next, string chatID);
    }
}