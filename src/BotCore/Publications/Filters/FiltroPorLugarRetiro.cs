//--------------------------------------------------------------------------------
// <copyright file="Reporte.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;

namespace BotCore.Publication.Filters
{
    public class FiltroPorLugarRetiro : IFiltroBusqueda
    {
        public IFiltroBusqueda Next {get;}

        private Publicacion lugarRetiro;

        public FiltroPorLugarRetiro(Publicacion lugarRetiro, IFiltroBusqueda next = null)
        {
            this.lugarRetiro = lugarRetiro;
            this.Next = next;
        }

        public List<Publicacion> Filtrar(List<Publicacion> publicaciones)
        {
            List<Publicacion> publicacionesFiltradas = new List<Publicacion>();

            foreach(Publicacion p in publicaciones)
            {
                if(p.LugarRetiro == this.lugarRetiro)
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