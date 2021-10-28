namespace BotCore.Publication
{
  /// <summary>
  /// Instancia de las categorias posibles para <see cref="Residuo"/>
  /// </summary>
  public class Categoria
  {
    /// <summary>
    /// Constructor de categoria
    /// </summary>
    /// <param name="categorias"></param>
    public Categoria(string categorias)
    {
      this.Categorias = categorias;
    }

/// <summary>
/// Property de la categoria 
/// </summary>
/// <value>string</value>
    public string Categorias{get; set;}
  }
}