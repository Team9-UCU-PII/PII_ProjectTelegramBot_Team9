//--------------------------------------------------------------------------------
// <copyright file="Venta.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Text;
using BotCore.User;

namespace BotCore.Publication
{
  /// <summary>
  /// Clase que reune las compras de <see cref = "Publicacion"/> y los implicados, y se encarga de hacer la compra en sí.
  /// Implementa <see iref = "IPrintable"/>.
  /// </summary>
  public class Venta : IPrintable
  {
    /// <summary>
    /// Se crea la instancia de venta con la fecha del momento.
    /// </summary>
    /// <param name="fecha"></param>
    public Venta(DateTime fecha)
    {
      this.Fecha = fecha;
    }
    /// <summary>
    /// Property de Venta.
    /// </summary>
    /// <value><see cref = "DateTime"/></value>
    public DateTime Fecha {get; set;}
    /// <summary>
    /// Property de Venta.
    /// </summary>
    /// <value><see cref = "Emprendedor"/></value>
    public Emprendedor Comprador {get; set;}
    /// <summary>
    /// Property de Venta.
    /// </summary>
    /// <value><see cref = "Publicacion"/></value>
    public Publicacion Publicacion {get; set;}
    /// <summary>
    /// Metodo que concreta la compra de la <see cref = "Publicacion"/>.
    /// </summary>
    /// <param name="comprador"></param>
    /// <param name="publicacion"></param>
    public void Comprar(Emprendedor comprador, Publicacion publicacion)
    {
      Publicacion.Comprado = true;
      this.Comprador = comprador;
      this.Publicacion = publicacion;
    }
/// <summary>
/// Implementacion del tipo <see iref = "IPrintable"/>.
/// </summary>
/// <returns>String</returns>
    public string GetTextToPrint() {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Publicacion.Residuo.Descripcion} ({this.Publicacion.Cantidad} {this.Publicacion.Residuo.UnidadMedida})");
      text.AppendLine($"Vendedor: {this.Publicacion.Vendedor.Nombre}");
      text.AppendLine($"Comprador: {this.Comprador.Nombre}");
      text.AppendLine($"Precio total: {this.Publicacion.Moneda} {this.Publicacion.Precio * this.Publicacion.Cantidad}");
      return text.ToString();
    }
  }
}