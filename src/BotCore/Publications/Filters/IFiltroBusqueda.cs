//--------------------------------------------------------------------------------
// <copyright file="IPrintable.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using System.Collections.Generic;

namespace BotCore.Publication.Filters
{
    /// <summary>
    /// Interfaz 
    /// </summary>
    public interface IFiltroBusqueda 
    {
        IFiltroBusqueda Next {get;}

        List<Publicacion> Filtrar(List<Publicacion> publicaciones);
    }
}