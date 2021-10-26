namespace ChatBot
{
    //esta clase funciona de mediadora entre el registro del user y el gestionador
    //similar al Visitor. Ayuda a almacenar tanto el user como el enlace que se uso para invitar
    //en el gestionador
    public  class Invitacion
    {
        private Invitacion(string enlace, IUsuario user)
        {
            this.UserInvitado = user;
            this.Link = enlace;
            this.fueAceptada = false;
        }
        public IUsuario UserInvitado;
        public string Link;
        public bool fueAceptada;
        public static void Guardar(Invitacion invite)
        {
            GestorInvitaciones.Instancia.invitacionesEnviadas.Add(invite);      
        }
        public static Invitacion Enviar(string objetivo, IUsuario user)
        {
            string link = Invitacion.GenerarEnlace();
            Invitacion invite = new Invitacion(link, user);

            //placeholder "mensaje del bot"

            return invite;
        }
        private static string GenerarEnlace()
        {
            //se genera el enlace
            return "enlace";
        }
    }
