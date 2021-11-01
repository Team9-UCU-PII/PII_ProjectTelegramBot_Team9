using System;
using System.Text;
using BotCore.User;

namespace BotCore.Publication
{
  public class Venta : IPrintable
  {
    public Venta(DateTime fecha)
    {
      this.Fecha = fecha;
    }

    public DateTime Fecha {get; set;}
    public Emprendedor Comprador {get; set;}
    public Publicacion Publicacion {get; set;}

    public void Comprar(Emprendedor comprador, Publicacion publicacion)
    {
      Publicacion.Comprado = true;
      this.Comprador = comprador;
      this.Publicacion = publicacion;
    }

    public string GetTextToPrint() {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Publicacion.Residuo.Descripcion} ({this.Publicacion.Cantidad} {this.Publicacion.Residuo.UnidadMedida})");
      text.AppendLine($"Vendedor: {this.Publicacion.Vendedor.Nombre}");
      text.AppendLine($"Comprador: {this.Comprador.Nombre}");
      text.AppendLine($"Precio total: {this.Publicacion.Moneda} {this.Publicacion.PrecioUnitario * this.Publicacion.Cantidad}");
      return text.ToString();
    }
  }
}