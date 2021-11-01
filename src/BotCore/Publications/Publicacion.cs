using System.Text;
using BotCore.User;

namespace BotCore.Publication
{
  public class Publicacion : IPrintable
  {
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

    public Residuo Residuo {get; set;}
    public double PrecioUnitario {get; set;}
    public string Moneda {get; set;}
    public int Cantidad {get; set;}
    public string LugarRetiro {get; set;}
    public Empresa Vendedor {get; set;}
    public string Descripcion{get; set;}
    public bool Comprado{get; set;}

    public double PrecioTotal {
      get {
        return this.PrecioUnitario * this.Cantidad;
      }
    }

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