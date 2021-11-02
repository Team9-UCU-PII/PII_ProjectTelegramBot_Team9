//--------------------------------------------------------------------------------
// <copyright file="Publicacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using System.Text;

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Tipo base de publicación, comprende Descripcion, Residuo, Precio, Moneda, Cantidad, Lugar de Retiro y la Empresa Vendedor.
  /// </summary>
  public class Publicacion : IPrintable
  {
    /// <summary>
    /// Constructor de Clase Publicacion.
    /// </summary>
    /// <param name="residuo"><see cref = "Residuo"/>.</param>
    /// <param name="precioUnitario"><see langword = "double"/>.</param>
    /// <param name="moneda"><see langword = "string"/>.</param>
    /// <param name="cantidad"><see langword = "int"/>.</param>
    /// <param name="lugarRetiro"><see langword = "string"/>.</param>
    /// <param name="vendedor"><see cref = "Empresa"/>.</param>
    /// <param name="descripcion"><see langword = "string"/>.</param>
    public Publicacion(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion)
    {
      this.Residuo = residuo;
      this.PrecioUnitario = precioUnitario;
      this.Moneda = moneda;
      this.Cantidad = cantidad;
      this.LugarRetiro = lugarRetiro;
      this.Vendedor = vendedor;
      this.Descripcion = descripcion;
      this.Comprado = false;
    }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see cref = "Residuo"/>.</value>
    public Residuo Residuo { get; set; }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see langword="double"/>.</value>
    public double PrecioUnitario { get; set; }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Moneda { get; set; }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see langword="int"/>.</value>
    public int Cantidad { get; set; }
    
    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see langword = "string"/>.</value>
    public string LugarRetiro { get; set; }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value>Tipo IUsuario, instancia de Empresa</value>
    public Empresa Vendedor { get; set; }

    /// <summary>
    /// Property de Publicacion.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Descripcion{ get; set; }

    /// <summary>
    /// Property de Publicacion que determina si la publicacion ya se compró.
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
    public string GetTextToPrint() {
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