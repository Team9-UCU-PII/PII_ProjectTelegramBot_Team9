//--------------------------------------------------------------------------------
// <copyright file="PublicacionRecurrente.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Subclase de <see cref = "Publicacion"/>, añade propiedad de Recurrencia del residuo.
  /// </summary>
  public class PublicacionRecurrente : Publicacion
  {
    /// <summary>
    /// Mismo Constructor que <see cref = "Publicacion"/>.
    /// </summary>
    /// <param name="residuo"><see cref = "Residuo"/>.</param>
    /// <param name="precioUnitario"><see langword = "double"/>.</param>
    /// <param name="moneda"><see langword = "string"/>.</param>
    /// <param name="cantidad"><see langword = "int"/>.</param>
    /// <param name="lugarRetiro"><see langword = "string"/>.</param>
    /// <param name="vendedor"><see cref = "Empresa"/>.</param>
    /// <param name="frecuenciaAnualRestock"><see langword = "int"/>.</param>
    /// <param name="descripcion"><see langword = "string"/>.</param>
    public PublicacionRecurrente(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, int frecuenciaAnualRestock, string descripcion)
      : base(residuo, precioUnitario, moneda, cantidad, lugarRetiro, vendedor, descripcion)
    {
      this.FrecuenciaAnualRestock = frecuenciaAnualRestock;
    }

    /// <summary>
    /// Obtiene o establece la frecuencia que se restockea
    /// el residuo. Una property exclusiva de Publicacion Recurrente.
    /// </summary>
    /// <value><see langword="int"/>.</value>
    public int FrecuenciaAnualRestock { get; set; }
  }
}