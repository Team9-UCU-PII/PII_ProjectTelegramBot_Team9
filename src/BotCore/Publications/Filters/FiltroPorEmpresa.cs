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
    /// Clase que se encarga de filtrar las distintas publicaciones la empresa publicadora.
    /// </summary>
    public class FiltroPorEmpresa : IFiltroBusqueda
    {
        /// <summary>
        /// Obtiene el siguiente filtro de búsqueda.
        /// </summary>
        /// <value>IFiltroBusqueda.</value>
        public IFiltroBusqueda Next {get; set;}

        private Empresa empresa;

        /// <summary>
        /// Método que recibe el tipo de filtro y el siguiente filtro de búsqueda.
        /// </summary>
        /// <param name="empresa"><see cref = "Empresa"/>.</param>
        /// <param name="next"><see cref = "IFiltroBusqueda"/>.</param>
        public FiltroPorEmpresa(Empresa empresa, IFiltroBusqueda next = null)
        {
            this.empresa = empresa;
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
                if(p.Vendedor == this.empresa)
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