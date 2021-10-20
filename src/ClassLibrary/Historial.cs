using System.Collections.Generic;

namespace ChatBot
{
  public class Historial : IUsuario
  {
    public Historial(List<Venta> ventas, IUsuario usuario)
    {
      this.Ventas = ventas;
      this.Usuario = usuario;
    }

    public List<Venta> Ventas {get; set;}
    public IUsuario Usuario {get; set;}
  }
}