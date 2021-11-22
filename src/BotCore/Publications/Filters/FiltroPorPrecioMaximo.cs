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
    /// Clase que se encarga de filtrar las distintas publicaciones por precio máximo.
    /// </summary>
    public class FiltroPorPrecioMaximo : IFiltroBusqueda
    {
        /// <summary>
        /// Obtiene el siguiente filtro de búsqueda.
        /// </summary>
        /// <value>IFiltroBusqueda.</value>
        public IFiltroBusqueda Next {get;}

        private double precioMaximo;

        /// <summary>
        /// Método que recibe el tipo de filtro y el siguiente filtro de búsqueda.
        /// </summary>
        /// <param name="referencia"><see cref = "Publicacion"/>.</param>
        /// <param name="next"><see cref = "IFiltroBusqueda"/>.</param>
        public FiltroPorPrecioMaximo(Publicacion referencia, IFiltroBusqueda next = null)
        {
            this.precioMaximo = referencia.PrecioTotal;
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
                if(p.PrecioTotal <= this.precioMaximo)
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