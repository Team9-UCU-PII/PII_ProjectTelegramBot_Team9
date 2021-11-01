//--------------------------------------------------------------------------------
// <copyright file="Categoria.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Instancia de las categorias posibles para <see cref="Residuo"/>.
  /// </summary>
  public class Categoria
  {
    /// <summary>
    /// Constructor de categoria.
    /// </summary>
    /// <param name="nombre"></param>
    public Categoria(string nombre)
    {
      this.Nombre = nombre;
    }

/// <summary>
/// Property de la categoria.
/// </summary>
/// <value>string</value>
    public string Nombre{get; set;}
  }
}