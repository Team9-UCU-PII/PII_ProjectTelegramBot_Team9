namespace ChatBot
{
    public class Empresa: IUsuario
    {
        public string Nombre;

        public string Lugar;

        public string Descripcion;

        public string Contacto;

        public string[] PalabrasClave;

        public Empresa(string nombre, string lugar, string descripcion, string contacto, string nombreUsuario, string contraseña)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Descripcion = descripcion;
            this.Contacto = contacto;
            this.NombreUsuario = nombreUsuario;
            this.Contraseña = contraseña;
        }

        public void AceptarVenta(Publicacion publicacion)
        {

        }
    }
}