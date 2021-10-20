using System;

namespace ChatBot
{
  public class Venta
  {
    public Venta(DateTime fecha, Emprendedor comprador, Publicacion publicacion)
    {
      this.Fecha = fecha;
      this.Comprador = comprador;
      this.Publicacion = publicacion;
    }

    public DateTime Fecha {get; set;}
    public Emprendedor Comprador {get; set;}
    public Publicacion Publicacion {get; set;}
  }
}