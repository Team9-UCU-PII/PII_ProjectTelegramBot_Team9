namespace ChatBot
{
    public class Emprendedor: IUsuario
    {
        public string Nombre;

        public string Lugar;

        public string Rubro;

        public string Especializacion;

        public string[] Habilitaciones;

        public Emprendedor(string nombre, string lugar, string rubro, string especializacion, string nombreUsuario, string contraseña)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Especializacion = especializacion;
            this.NombreUsuario = nombreUsuario;
            this.Contraseña = contraseña;
        }

        public void ComprarPublicacion(Publicacion publicacion)
        {

        }
    }
}