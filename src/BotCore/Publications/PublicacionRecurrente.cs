using BotCore.User;

namespace BotCore.Publication
{
  /// <summary>
  /// Subclase de <see cref = "Publicacion"/>, añade propiedad de Recurrencia del residuo.
  /// </summary>
  public class PublicacionRecurrente : Publicacion
  {
    /// <summary>
    /// Mismo Constructor que <see cref = "Publicacion"/>.
    /// </summary>
    /// <param name="residuo"></param>
    /// <param name="precio"></param>
    /// <param name="moneda"></param>
    /// <param name="cantidad"></param>
    /// <param name="lugarRetiro"></param>
    /// <param name="vendedor"></param>
    /// <param name="frecuenciaAnualRestock"></param>
    /// <param name="descripcion"></param>
    public PublicacionRecurrente(Residuo residuo, double precio, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, int frecuenciaAnualRestock, string descripcion)
      :base(residuo, precio, moneda, cantidad, lugarRetiro, vendedor, descripcion)
    {
      this.FrecuenciaAnualRestock = frecuenciaAnualRestock;
    }
    /// <summary>
    /// Property exclusiva de Publicacion Recurrente.
    /// </summary>
    /// <value>Tipo int</value>
    public int FrecuenciaAnualRestock {get; set;} 

  }
}