using BotCore.User;

namespace BotCore.Publication
{
  public class Publicacion
  {
    public Publicacion(Residuo residuo, double precio, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion, bool comprado)
    {
      this.Residuo = residuo;
      this.Precio = precio;
      this.Moneda = moneda;
      this.Cantidad = cantidad;
      this.LugarRetiro = lugarRetiro;
      this.Vendedor = vendedor;
      this.Descripcion = descripcion;
      this.Comprado = comprado;
    }

    public Residuo Residuo {get; set;}
    public double Precio {get; set;}
    public string Moneda {get; set;}
    public int Cantidad {get; set;}
    public string LugarRetiro {get; set;}
    public Empresa Vendedor {get; set;}
    public string Descripcion{get; set;}
    public bool Comprado{get; set;}
  }
}