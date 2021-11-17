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
    /// Clase que se encarga de filtrar las distintas publicaciones por tipo de residuo.
    /// </summary>
    public class FiltroPorResiduo : IFiltroBusqueda
    {
        /// <summary>
        /// Obtiene el siguiente filtro de búsqueda.
        /// </summary>
        /// <value>IFiltroBusqueda.</value>
        public IFiltroBusqueda Next {get;}

        private Residuo residuo;

        /// <summary>
        /// Método que recibe el tipo de filtro y el siguiente filtro de búsqueda.
        /// </summary>
        /// <param name="residuo"><see cref = "Residuo"/>.</param>
        /// <param name="next"><see cref = "IFiltroBusqueda"/>.</param>
        public FiltroPorResiduo(Residuo residuo, IFiltroBusqueda next = null)
        {
            this.residuo = residuo;
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
                if(p.Residuo == this.residuo)
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