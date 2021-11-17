//--------------------------------------------------------------------------------
// <copyright file="Reporte.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.User;

namespace BotCore.Publication.Filters
{
    /// <summary>
    /// Clase que se encarga de filtrar las distintas publicaciones por frecuencia de restock.
    /// </summary>
    public class FiltroPorFrecuenciaRestock : IFiltroBusqueda
    {
        /// <summary>
        /// Obtiene el siguiente filtro de búsqueda.
        /// </summary>
        /// <value>IFiltroBusqueda.</value>
        public IFiltroBusqueda Next {get;}

        private PublicacionRecurrente frecuencia;

        /// <summary>
        /// Método que recibe el tipo de filtro y el siguiente filtro de búsqueda.
        /// </summary>
        /// <param name="frecuencia"><see cref = "PublicacionRecurrente"/>.</param>
        /// <param name="next"><see cref = "IFiltroBusqueda"/>.</param>
        public FiltroPorFrecuenciaRestock(PublicacionRecurrente frecuencia, IFiltroBusqueda next = null)
        {
            this.frecuencia = frecuencia;
            this.Next = next;
        }

        /// <summary>
        /// Método que retorna las publicaciones que coinciden con el filtro aplicado.
        /// </summary>
        /// <param name="publicaciones"></param>
        /// <returns></returns>
        public List<Publicacion> Filtrar(List<Publicacion> publicaciones)
        {
            List<Publicacion> publicacionesFiltradas = new List<Publicacion>();

            foreach(Publicacion p in publicaciones)
            {
                if(p == this.frecuencia)
                {
                    publicacionesFiltradas.Add(p);
                }
            }

            if (this.Next != null)
            {
                return this.Next.Filtrar(publicacionesFiltradas);
            }
            else
            {
                return publicacionesFiltradas;
            }
        }
    }
}