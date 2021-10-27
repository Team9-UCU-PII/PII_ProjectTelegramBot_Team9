using System.Collections.Generic;
using BotCore.Publication;

namespace BotCore.User
{
    public class Empresa: IUsuario
    {
        public string Nombre{ get; set;}

        public string Lugar;

        public string Rubro;

        public string Descripcion;

        public string Contacto;

        public string[] PalabrasClave;

        public List<Venta> Historial = new List<Venta>();

        public DatosLogin DatosLogin { get; private set; }

        public Empresa(string nombre, string lugar, string rubro, string descripcion, string contacto)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Descripcion = descripcion;
            this.Contacto = contacto;
        }

        public Empresa()
        {
            
        }
    }
}