namespace ChatBot
{
  public class Residuo
  {
    public Residuo(Categoria[] categoria, string descripcion, string unidadMedida, Habilitaciones[] habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }

    public Categoria[] Categoria {get; set;}
    public string Descripcion {get; set;}
    public string UnidadMedida {get; set;}
    public Habilitaciones[] Habilitaciones {get; set;}
  }
}