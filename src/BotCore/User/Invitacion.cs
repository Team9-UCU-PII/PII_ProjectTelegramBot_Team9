namespace BotCore.User
{
    /// <summary>
    /// Clase mediadora que representa la invitación en si, y encapsula el enlace y destino.
    /// </summary>
    public  class Invitacion
    {
        private Invitacion(string enlace, IUsuario user)
        {
            this.UserInvitado = user;
            this.Link = enlace;
            this.fueAceptada = false;
        }
        /// <summary>
        /// El usuario destinado, debería ser sobreescrito por el destinatario.
        /// </summary>
        public IUsuario UserInvitado;
        private string Link;
        /// <summary>
        /// Propiedad, permite evaluar si el destinatario aceptó la invitación
        /// y se registró.
        /// </summary>
        public bool fueAceptada;
        /// <summary>
        /// Se almacena la invitación en el gestor.
        /// </summary>
        /// <param name="invite"></param>
        public static void Guardar(Invitacion invite)
        {
            GestorInvitaciones.Instancia.invitacionesEnviadas.Add(invite);      
        }
        /// <summary>
        /// Método que envia la invitación.
        /// </summary>
        /// <param name="objetivo"></param>
        /// <param name="user"></param>
        /// <returns></returns>
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
}