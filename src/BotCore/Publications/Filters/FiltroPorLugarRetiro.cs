//--------------------------------------------------------------------------------
// <copyright file="Reporte.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;

namespace BotCore.Publication.Filters
{
    /// <summary>
    /// Clase que se encarga de filtrar las distintas publicaciones por lugar de retiro.
    /// </summary>
    public class FiltroPorLugarRetiro : IFiltroBusqueda
    {
        /// <summary>
        /// Obtiene el siguiente filtro de búsqueda.
        /// </summary>
        /// <value>IFiltroBusqueda.</value>
        public IFiltroBusqueda Next {get;}

        private Publicacion lugarRetiro;

        /// <summary>
        /// Método que recibe el tipo de filtro y el siguiente filtro de búsqueda.
        /// </summary>
        /// <param name="lugarRetiro"><see cref = "Publicacion"/>.</param>
        /// <param name="next"><see cref = "IFiltroBusqueda"/>.</param>
        public FiltroPorLugarRetiro(Publicacion lugarRetiro, IFiltroBusqueda next = null)
        {
            this.lugarRetiro = lugarRetiro;
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
                if(p == this.lugarRetiro)
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