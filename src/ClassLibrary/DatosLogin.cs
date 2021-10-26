namespace ChatBot
{
    public class DatosLogin
    {
        public string NombreUsuario { get; set; }

        public string Contrasenia { get; }

        public IUsuario Usuario { get;}

        public DatosLogin(string nombreUsuario, string contrasenia, IUsuario usuario)
        {
            this.NombreUsuario = nombreUsuario;
            this.Contrasenia = contrasenia;
            this.Usuario = usuario;
        }
    }
}