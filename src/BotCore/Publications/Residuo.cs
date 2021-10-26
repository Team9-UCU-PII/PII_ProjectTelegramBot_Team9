using BotCore.User;

namespace BotCore.Publication
{
  public class Residuo
  {
    public Residuo(Categoria[] categoria, string descripcion, string unidadMedida, Habilitacion[] habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }

    public Categoria[] Categoria {get; set;}
    public string Descripcion {get; set;}
    public string UnidadMedida {get; set;}
    public Habilitacion[] Habilitaciones {get; set;}
  }
}