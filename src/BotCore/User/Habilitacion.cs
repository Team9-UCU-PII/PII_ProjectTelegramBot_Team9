namespace BotCore.User
{
  /// <summary>
  /// Clase representativa de las habilitaciones existentes aplicables a los residuos y emprendedores.
  /// </summary>
  public class Habilitacion
  {
    /// <summary>
    /// Contructor del tipo Habilitación.
    /// </summary>
    /// <param name="nombre"></param>
    public Habilitacion(string nombre)
    {
      this.Nombre = nombre;
    }
    /// <summary>
    /// Nombre de la habilitación.
    /// </summary>
    /// <value></value>
    public string Nombre{get; set;}
  }
}