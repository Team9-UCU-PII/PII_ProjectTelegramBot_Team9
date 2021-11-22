using MessageGateway.Handlers;

namespace MessageGateway
{
    public class MessageBase : IMessage
    {
        public string ChatID { get; set; }
        public string TxtMensaje { get; set; }
        public PalabrasClaveHandlers Keyword { get; set; }

        public MessageBase(string chatID, string txtMensaje)
        {
            this.ChatID = chatID;
            this.TxtMensaje = txtMensaje;
        }

        public MessageBase(string chatID, string txtMensaje, PalabrasClaveHandlers keyword)
        {
            this.ChatID = chatID;
            this.TxtMensaje = txtMensaje;
            this.Keyword = keyword;
        }
        /*
        public IMessage CrearRespuesta(string texto)
        {
            return new MessageBase(this.ChatID, texto);
        }
        */
    }
}