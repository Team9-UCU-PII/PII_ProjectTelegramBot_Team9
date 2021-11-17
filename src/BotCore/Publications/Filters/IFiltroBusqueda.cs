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
    /// Interfaz que unifica tipos cuyo objetivo es devolver texto al bot para imprimir
    /// y retornar al usuario.
    /// </summary>
    public interface IFiltroBusqueda 
    {
        /// <summary>
        /// Obtiene el siguiente filtro de búsqueda.
        /// </summary>
        /// <value></value>
        IFiltroBusqueda Next {get;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publicaciones"></param>
        /// <returns></returns>
        List<Publicacion> Filtrar(List<Publicacion> publicaciones);
    }
}