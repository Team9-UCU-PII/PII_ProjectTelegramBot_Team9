using BotCore.User;

namespace BotCore.Publication
{
  public class PublicacionRecurrente : Publicacion
  {
    public PublicacionRecurrente(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, int frecuenciaAnualRestock, string descripcion)
      :base(residuo, precioUnitario, moneda, cantidad, lugarRetiro, vendedor, descripcion)
    {
      this.FrecuenciaAnualRestock = frecuenciaAnualRestock;
    }
    public int FrecuenciaAnualRestock {get; set;} 

  }
}