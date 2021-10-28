using BotCore.User;

namespace BotCore.Publication
{
  /// <summary>
  /// Clase representativa de los disferentes residuos. Contiene <see cref = "Categoria"/>s, Descripcion, unidad de medida y <see cref = "Habilitacion"/>es
  /// </summary>
  public class Residuo
  {
    /// <summary>
    /// Constructor de Residuo
    /// </summary>
    /// <param name="categoria"></param>
    /// <param name="descripcion"></param>
    /// <param name="unidadMedida"></param>
    /// <param name="habilitaciones"></param>
    public Residuo(Categoria[] categoria, string descripcion, string unidadMedida, Habilitacion[] habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }
    /// <summary>
    /// Property de Categorias descriptivas del residuo
    /// </summary>
    /// <value>Array de <see cref = "Categoria"/></value>
    public Categoria[] Categoria {get; set;}
    /// <summary>
    /// Property de Residuo
    /// </summary>
    /// <value>string</value>
    public string Descripcion {get; set;}
    /// <summary>
    /// Property de Residuo
    /// </summary>
    /// <example> Kgs, Lts, m2, etc </example>
    /// <value>string</value>
    public string UnidadMedida {get; set;}
    /// <summary>
    /// Property de Habilitaciones necesarias para la compra del residuo
    /// </summary>
    /// <value>Array de <see cref = "Habilitacion"/></value>
    public Habilitacion[] Habilitaciones {get; set;}
  }
}