namespace ChatBot
{
  public class Publicacion
  {
    public Publicacion(Residuo residuo, double precio, string moneda, int cantidad, string lugarRetiro, Empresa vendedor)
    {
      this.Residuo = residuo;
      this.Precio = precio;
      this.Moneda = moneda;
      this.Cantidad = cantidad;
      this.LugarRetiro = lugarRetiro;
      this.Vendedor = vendedor;
    }

    public Residuo Residuo {get; set;}
    public double Precio {get; set;}
    public string Moneda {get; set;}
    public int Cantidad {get; set;}
    public string LugarRetiro {get; set;}
    public Empresa Vendedor {get; set;}
  }
}