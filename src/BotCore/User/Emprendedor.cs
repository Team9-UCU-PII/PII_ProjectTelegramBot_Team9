using System.Collections.Generic;
using BotCore.Publication;

namespace BotCore.User
{
    public class Emprendedor: IUsuario
    {
        public string Nombre{ get; set;}

        public string Lugar;

        public string Rubro;

        public string Especializacion;

        public string[] Habilitaciones;

        public List<Venta> Historial = new List<Venta>();

        public DatosLogin DatosLogin { get; private set; }

        public Emprendedor(string nombre, string lugar, string rubro, string especializacion)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Especializacion = especializacion;
        }

        public Emprendedor()
        {
        
        }
    }
}