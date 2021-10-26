namespace ChatBot
{
    public class Autenticacion
    {
        private static Autenticacion instancia;

        public static Autenticacion Instancia 
        {
            get
            {
                if(instancia == null)
                {
                    Autenticacion.instancia = new Autenticacion();
                }
                return Autenticacion.instancia;
            }
        }

        private Autenticacion()
        {
            
        }

        public static bool ValidarUsuario(string nombreUsuario, string contraseña)
        {

        }
    }
}