using System.Linq;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public abstract class MessageHandlerBase : IMessageHandler
    {
        public IMessageHandler Next { get; set; }
        public IFormulario ContainingForm { get; set; }
        private string[] keywords { get; }

        protected MessageHandlerBase(string[] keywords, IMessageHandler next)
        {
            this.keywords = keywords;
            this.Next = next;
        }
        protected bool CanHandle(IMessage message)
        {
            return this.keywords.Contains<string>(message.Keyword);
        }

        public IMessageHandler Handle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.InternalHandle(message, out response, out nextHandlerKeyword))
            {
                return this;
            }
            else if (this.Next != null)
            {
                return this.Next.Handle(message, out response, out nextHandlerKeyword);
            }
            else
            {
                return null;
            }
        }

        protected abstract bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword);
    }
}