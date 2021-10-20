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

        private enum FiltrosPosibles
        {
            Vendedor,
            Residuo,
            LugarRetiro,
            PrecioMaximo,
            FrecuenciaRestock

        }
        public List<Publicacion> BuscarPublicaciones(Dictionary<string, object> PublicacionesASeparar)
        {
            List<Publicacion> result = new List<Publicacion>();
            List<Publicacion> publicacionesActivas = DataAccess.Obtener<Publicacion>();
            
            foreach (var Elemento in PublicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    switch (Elemento.Value)
                    {
                        case 1 ();
                    }
                }
            }

            return result;
        }
    }
}