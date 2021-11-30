//--------------------------------------------------------------------------------
// <copyright file="IListableForm.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;
using MessageGateway.Handlers.ListadoPublicaciones;

namespace MessageGateway.Forms
{

  /// <summary>
  /// Interfaz para las publicaciones filtradas.
  /// </summary>
  public interface IListableForm
  {

    /// <summary>
    /// Lista en la que se guardan las publicaciones filtradas.
    /// </summary>
    /// <value></value>
    List<Publicacion> publicacionesFiltradas {get; set;}

    /// <summary>
    /// Obtiene o establece el estado de construccion del listado.
    /// </summary>
    /// <value></value>
    HandlerListadoPublicaciones.fasesListado CurrentStateListado {get; set;}

    /// <summary>
    /// Lista para guardar la publicación con detalles.
    /// </summary>
    /// <value></value>
    Publicacion publicacionSeparada {get; set;}
  }
}