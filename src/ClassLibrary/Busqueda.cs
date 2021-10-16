using System.Collections.Generic;

namespace ChatBot
{
    public class Busqueda
    {
        public Busqueda Instancia
        {
            get
            {
                if (this.Instancia == null)
                {
                    this.Instancia = new Busqueda();
                    return this.Instancia;
                }
                return this.Instancia;
            }
            set{}
        }

        private Busqueda(){}

        public List<Publicacion> BuscarPublicaciones(Dictionary<string, Publicacion> PublicacionesActivas, string PalabraClave)
        {
            List<Publicacion> result = new List<Publicacion>();
            foreach (string i in PublicacionesActivas.Keys)
            {
                if (i == PalabraClave)
                {
                    result.Add(PublicacionesActivas[i]);
                }
            }
            return result;
        }
    }
}