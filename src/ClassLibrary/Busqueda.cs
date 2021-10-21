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
            Empresa,
            Residuo,
            LugarRetiro,
            PrecioMaximo,
            FrecuenciaRestock
        }
        public List<Publicacion> BuscarPublicaciones(Dictionary<string, object> PublicacionesASeparar)
        {
            List<Publicacion> result = new List<Publicacion>();
            List<Publicacion> publicacionesActivas = DataAccess.Obtener<Publicacion>();
            
            foreach (var Filtro in PublicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    switch (Filtro.Value.GetType())
                    {
                        case Empresa:
                            if (suspect.Vendedor.Nombre == Filtro.Key)
                            {
                                result.Add(suspect);
                            }
                            break;
                        case Residuo:
                            if (suspect.Residuo.Descripcion == Filtro.Key)
                            {
                                result.Add(suspect);
                            }
                            break;
                        case LugarRetiro:
                            if (suspect.LugarRetiro == Filtro.Key)
                            {
                                result.Add(suspect);
                            }
                            break;
                        case PrecioMaximo:
                            if (suspect.Precio <= Filtro.Key)
                            {
                                result.Add(suspect);
                            }
                            break;
                        case FrecuenciaRestock:
                            if (suspect is PublicacionRecurrente && suspect.FrecuenciaAnualRestock == Filtro.Key)
                            {
                                result.Add(suspect);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }
    }
}