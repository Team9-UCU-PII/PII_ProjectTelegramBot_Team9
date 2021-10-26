using System.Collections.Generic;

namespace BotCore.Publication
{
    public class Busqueda
    {
        private static Busqueda instancia{get;set;}
        //un singleton para que contenga los filtros posibles, ya que conocerlos tambien deberia ser su responsabilidad
        public static Busqueda Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Busqueda();
                }
                return instancia;
            }
        }

        private Busqueda(){}

        public enum FiltrosPosibles
        {
            Empresa,
            Residuo,
            LugarRetiro,
            PrecioMaximo,
            FrecuenciaRestock
        }
        public List<Publicacion> BuscarPublicaciones(Dictionary<FiltrosPosibles, object> PublicacionesASeparar)
        {
            List<Publicacion> result = new List<Publicacion>();
            List<Publicacion> publicacionesNoAptas = new List<Publicacion>();
            List<Publicacion> publicacionesActivas = DataAccess.Obtener<Publicacion>();
            
            foreach (var Filtro in PublicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    switch (Filtro.Key)
                    {
                        case FiltrosPosibles.Empresa:
                            if (suspect.Vendedor != Filtro.Key)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.Residuo:
                            if (suspect.Residuo != Filtro.Key)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.LugarRetiro:
                            if (suspect.LugarRetiro != Filtro.Key)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.PrecioMaximo:
                            if (suspect.Precio <= Filtro.Key)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.FrecuenciaRestock:
                            if (suspect is not PublicacionRecurrente && suspect.FrecuenciaAnualRestock != Filtro.Key)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            foreach (Publicacion publicacion in publicacionesActivas)
            {
                if (!publicacionesNoAptas.Contains(publicacion))
                {
                    result.Add(publicacion);
                }
            }
            return result;
        }
    }
}