namespace MessageGateway
{
    public interface IGateway
    {
        static IGateway Instancia {get;}
        void EnviarMensaje(string destinatario, string texto);
        void EnviarInvitacion(string destinatario, string texto);
    }
}