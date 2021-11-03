//--------------------------------------------------------------------------------
// <copyright file="Residuo.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    /// <param name="categoria"><see cref = "Categoria"/>.</param>
    /// <param name="descripcion"><see langword = "string"/>.</param>
    /// <param name="unidadMedida"><see langword = "string"/>.</param>
    /// <param name="habilitaciones"><see tref = "List T"/>.</param>
    public Residuo(Categoria categoria, string descripcion, string unidadMedida, List<Habilitacion> habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }

    /// <summary>
    /// Obtiene o establece la categoria generica a la que pertenece el residuo.
    /// </summary>
    /// <value>Un <see langword="Array"/> de <see cref = "Categoria"/>.</value>
    public Categoria Categoria { get; set; }

    /// <summary>
    /// Obtiene o establece una descripción.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Descripcion { get; set; }

    /// <summary>
    /// Obtiene o establece la unidad de medida.
    /// </summary>
    /// <example> Kgs, Lts, m2, etc. </example>
    /// <value><see langword="string"/>.</value>
    public string UnidadMedida { get; set; }

    /// <summary>
    /// Obtiene o establece las Habilitaciones necesarias para la compra del residuo.
    /// </summary>
    /// <value><see langword="Array"/> de <see cref = "Habilitacion"/>.</value>
    public List<Habilitacion> Habilitaciones { get; set; }
    
    /// <summary>
    /// Implementación de <see iref = "IPrintable"/>, genera el texto para que envíe el bot.
    /// </summary>
    /// <returns><see langword="string"/>.</returns>
    public string GetTextToPrint() 
    {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Descripcion} ({this.Categoria.Nombre})");
      text.AppendLine($"Los emprendedores requieren las siguientes habilitaciones para manejar este residuo: {string.Join(", ", this.Habilitaciones.Select(h => h.Nombre))}");
      return text.ToString();
    }
  }
}