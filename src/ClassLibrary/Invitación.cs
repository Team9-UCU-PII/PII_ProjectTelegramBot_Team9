namespace ChatBot
{
    //esta clase funciona de mediadora entre el registro del user y el gestionador
    //similar al Visitor. Ayuda a almacenar tanto el user como el enlace que se uso para invitar
    //en el gestionador
    public  class Invitacion
    {
        public IUsuario UserInvitado;
        public string Link;
        public void InvitacionAceptada(IUsuario user)
        {
            this.UserInvitado = user;
            GestorInvitaciones.Instancia.invitacionesEnviadas.Add(this);      
        }
        public void InvitacionEnviada(string Enlace)
        {
            //aca el chatbot le pediria la data al usuario
            Autenticacion.Instancia.RegistrarUsuario(contraseña, username);
            Autenticacion.Instancia.AceptarInvitacion(this);
            this.Link = Enlace;
        }
    }
