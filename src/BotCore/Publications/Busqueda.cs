using System.Collections.Generic;
using System.Linq;
using Importers;

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
            List<Publicacion> publicacionesActivas = DataAccess.Instancia.Obtener<Publicacion>().Where((Publicacion p) => !p.Comprado).ToList();
            
            foreach (var Filtro in PublicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    switch (Filtro.Key)
                    {
                        case FiltrosPosibles.Empresa:
                            if (!suspect.Vendedor.Equals(Filtro.Value))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.Residuo:
                            if (!suspect.Residuo.Equals(Filtro.Value))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.LugarRetiro:
                            if (!suspect.LugarRetiro.Equals(Filtro.Value))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.PrecioMaximo:
                            if (suspect.PrecioUnitario > (double) Filtro.Value)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.FrecuenciaRestock:
                            if (suspect is not PublicacionRecurrente || !(suspect as PublicacionRecurrente).FrecuenciaAnualRestock.Equals(Filtro.Value))
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