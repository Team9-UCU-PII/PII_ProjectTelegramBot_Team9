//--------------------------------------------------------------------------------
// <copyright file="Venta.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright> 
//
// Patrón utilizado: Low coupling.
//Esta clase ayuda a vincular una Empresa y un Emprendedor con una publicación de por medio.
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using System;
using System.Text;

namespace ClassLibrary.Publication
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
    /// <param name="fecha"><see cref = "DateTime"/>.</param>
    public Venta(DateTime fecha)
    {
      this.Fecha = fecha;
    }

    /// <summary>
    /// Obtiene o establece la fecha de venta.
    /// </summary>
    /// <value><see cref = "DateTime"/>.</value>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Obtiene o establece el comprador.
    /// </summary>
    /// <value><see cref = "Emprendedor"/>.</value>
    public Emprendedor Comprador { get; set; }

    /// <summary>
    /// Obtiene o establece la publicacion que fue comprada.
    /// </summary>
    /// <value><see cref = "Publicacion"/>.</value>
    public Publicacion Publicacion { get; set; }

    /// <summary>
    /// Metodo que concreta la compra de la Publicación.
    /// </summary>
    /// <param name="comprador"><see cref = "Emprendedor"/>.</param>
    /// <param name="publicacion"><see cref = "Publicacion"/>.</param>
    public void Comprar(Emprendedor comprador, Publicacion publicacion)
    {
      publicacion.Comprado = true;
      this.Comprador = comprador;
      this.Publicacion = publicacion;
    }

    /// <summary>
    /// Implementacion del tipo <see iref = "IPrintable"/>.
    /// </summary>
    /// <returns><see langword="string"/>.</returns>
    public string GetTextToPrint() 
    {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Publicacion.Residuo.Descripcion} ({this.Publicacion.Cantidad} {this.Publicacion.Residuo.UnidadMedida})");
      text.AppendLine($"Vendedor: {this.Publicacion.Vendedor.Nombre}");
      text.AppendLine($"Comprador: {this.Comprador.Nombre}");
      text.AppendLine($"Precio total: {this.Publicacion.Moneda} {this.Publicacion.PrecioUnitario * this.Publicacion.Cantidad}");
      return text.ToString();
    }
  }
}