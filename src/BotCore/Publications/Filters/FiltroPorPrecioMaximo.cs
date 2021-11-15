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
    public class FiltroPorPrecioMaximo : IFiltroBusqueda
    {
        public IFiltroBusqueda Next {get;}

        private Publicacion precio;

        public FiltroPorPrecioMaximo(Publicacion precio, IFiltroBusqueda next = null)
        {
            this.precio = precio;
            this.Next = next;
        }

        public List<Publicacion> Filtrar(List<Publicacion> publicaciones)
        {
            List<Publicacion> publicacionesFiltradas = new List<Publicacion>();

            foreach(Publicacion p in publicaciones)
            {
                if(p.PrecioTotal <= this.precio)
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