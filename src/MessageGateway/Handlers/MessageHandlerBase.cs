using System.Linq;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public abstract class MessageHandlerBase : IMessageHandler
    {
        public IMessageHandler Next { get; set; }
        public IFormulario CurrentForm { get; set; }
        private string[] keywords { get; }

        protected MessageHandlerBase(string[] keywords, IMessageHandler next)
        {
            this.keywords = keywords;
            this.Next = next;
        }
        protected bool CanHandle(IMessage message)
        {
            return this.keywords.Contains(message.TxtMensaje);
        }

        public IMessageHandler Handle(IMessage message, out string response)
        {
            if (this.InternalHandle(message, out response))
            {
                return this;
            }
            else if (this.Next != null)
            {
                return this.Next.Handle(message, out response);
            }
            else
            {
                return null;
            }
        }

        protected abstract bool InternalHandle(IMessage message, out string response);

        protected enum PalabrasClaveHandlers
    
        {

        }

    }
}