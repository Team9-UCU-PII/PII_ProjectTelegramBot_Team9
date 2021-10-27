using BotCore.User;

namespace BotCore.Publication
{
  public class PublicacionRecurrente : Publicacion
  {
    public PublicacionRecurrente(Residuo residuo, double precio, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, int frecuenciaAnualRestock, string descripcion)
      :base(residuo, precio, moneda, cantidad, lugarRetiro, vendedor, descripcion)
    {
      this.FrecuenciaAnualRestock = frecuenciaAnualRestock;
    }
    public int FrecuenciaAnualRestock {get; set;} 

  }
}