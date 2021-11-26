//--------------------------------------------------------------------------------
// <copyright file="Publicacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patrón utilizado: Expert
// Como la Publicación conoce Residuo y la cantidad, puede calcular el precio total.
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using ClassLibrary.LocationAPI;
using Importers;
using System.Text;

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Tipo base de publicación, comprende Descripcion, Residuo, Precio, Moneda, Cantidad, Lugar de Retiro y la Empresa Vendedor.
  /// </summary>
  public class Publicacion : IPrintable, IPersistible
  {
    /// <summary>
    /// Constructor de Clase Publicacion.
    /// </summary>
    /// <param name="residuo"><see cref = "Residuo"/>.</param>
    /// <param name="precioUnitario"><see langword = "double"/>.</param>
    /// <param name="moneda"><see langword = "string"/>.</param>
    /// <param name="cantidad"><see langword = "int"/>.</param>
    /// <param name="lugarRetiro"><see cref = "Location"/>.</param>
    /// <param name="vendedor"><see cref = "Empresa"/>.</param>
    /// <param name="descripcion"><see langword = "string"/>.</param>
    /// <param name="categoria"><see langword = "string"/>.</param>
    public Publicacion(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion, Categoria categoria)
    {
      this.Residuo = residuo;
      this.PrecioUnitario = precioUnitario;
      this.Moneda = moneda;
      this.Cantidad = cantidad;
      this.LugarRetiro = LocationApiClient.Instancia.GetLocation(lugarRetiro);
      this.Vendedor = vendedor;
      this.Descripcion = descripcion;
      this.Categoria = categoria;
      this.Comprado = false;
    }

    /// <summary>
    /// Obtiene o establece el <see cref ="Residuo"/> publicado.
    /// </summary>
    /// <value><see cref = "Residuo"/>.</value>
    public Residuo Residuo { get; set; }

    /// <summary>
    /// Obtiene o establece el precio por unidad.
    /// </summary>
    /// <value><see langword="double"/>.</value>
    public double PrecioUnitario { get; set; }

    /// <summary>
    /// Obtiene o establece la moneda de compra.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Moneda { get; set; }

    /// <summary>
    /// Obtiene o establece la cantidad de <see cref ="Residuo"/> según la unidad dada.
    /// </summary>
    /// <value><see langword="int"/>.</value>
    public int Cantidad { get; set; }
    
    /// <summary>
    /// Obtiene o establece el lugar de retiro.
    /// </summary>
    /// <value><see langword = "string"/>.</value>
    public Location LugarRetiro { get; set; }

    /// <summary>
    /// Obtiene o establece la <see cref ="Empresa"/> vendedora.
    /// </summary>
    /// <value>Tipo IUsuario, instancia de Empresa</value>
    public Empresa Vendedor { get; set; }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Descripcion{ get; set; }

    /// <summary>
    /// Obtiene o establece la <see cref ="Categoria"/> publicado.
    /// </summary>
    /// <value><see cref = "Categoria"/>.</value>
    public Categoria Categoria { get; set; }

    /// <summary>
    /// Obtiene o establece si la publicacion ya se compró.
    /// </summary>
    /// <value><see langword="true"/> La publicación ya fue comprada.</value>
    public bool Comprado{ get; set; }

    /// <summary>
    /// Calcula el precio final de la publicación.
    /// </summary>
    /// <value><see langword="double"/>.</value>
    public double PrecioTotal 
    {
      get 
      {
        return this.PrecioUnitario * this.Cantidad;
      }
    }

    /// <summary>
    /// Implementación de <see iref = "IPrintable"/>, genera el texto para que envíe el bot.
    /// </summary>
    /// <returns><see langword="string"/>.</returns>
    public virtual string GetTextToPrint() 
    {
      StringBuilder text = new StringBuilder();
      text.AppendLine(this.Residuo.GetTextToPrint());
      text.AppendLine($"Cantidad: {this.Cantidad} {this.Residuo.UnidadMedida}");
      text.AppendLine($"Vendedor: {this.Vendedor.Nombre}");
      text.AppendLine(this.Descripcion);
      text.AppendLine($"Precio de venta: {this.Moneda} {this.PrecioTotal} ({this.Moneda} {this.PrecioUnitario} /{this.Residuo.UnidadMedida})");
      text.AppendLine($"Lugar de retiro: {this.LugarRetiro}");
      return text.ToString();
    }
  }
}