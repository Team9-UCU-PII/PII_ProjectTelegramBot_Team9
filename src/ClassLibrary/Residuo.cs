namespace ChatBot
{
  public class Residuo
  {
    public Residuo(string categoria, string descripcion, string unidadMedida, string[] habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }

    public string Categoria {get; set;}
    public string Descripcion {get; set;}
    public string UnidadMedida {get; set;}
    public string[] Habilitaciones {get; set;}
  }
}