//--------------------------------------------------------------------------------
// <copyright file="Residuo.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Clase representativa de los disferentes residuos. Contiene <see cref = "Categoria"/>s, 
  /// Descripcion, unidad de medida y <see cref = "Habilitacion"/>es.
  /// </summary>
  public class Residuo : IPrintable
  {
    /// <summary>
    /// Constructor de Residuo.
    /// </summary>
    /// <param name="categoria"></param>
    /// <param name="descripcion"></param>
    /// <param name="unidadMedida"></param>
    /// <param name="habilitaciones"></param>
    public Residuo(Categoria categoria, string descripcion, string unidadMedida, List<Habilitacion> habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }
    /// <summary>
    /// Categoria generica a la que pertenece el residuo.
    /// </summary>
    /// <value>Array de <see cref = "Categoria"/></value>
    public Categoria Categoria {get; set;}
    /// <summary>
    /// Property de Residuo.
    /// </summary>
    /// <value>string</value>
    public string Descripcion {get; set;}
    /// <summary>
    /// Property de Residuo.
    /// </summary>
    /// <example> Kgs, Lts, m2, etc </example>
    /// <value>string</value>
    public string UnidadMedida {get; set;}
    /// <summary>
    /// Property de Habilitaciones necesarias para la compra del residuo.
    /// </summary>
    /// <value>Array de <see cref = "Habilitacion"/></value>
    public List<Habilitacion> Habilitaciones {get; set;}

    public string GetTextToPrint() {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Descripcion} ({this.Categoria.Nombre})");
      text.AppendLine($"Los emprendedores requieren las siguientes habilitaciones para manejar este residuo: {string.Join(", ", this.Habilitaciones.Select(h => h.Nombre))}");
      return text.ToString();
    }
  }
}