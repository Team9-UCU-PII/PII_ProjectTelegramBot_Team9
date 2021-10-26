using System;

namespace ChatBot
{
  public class Venta
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
  }
}