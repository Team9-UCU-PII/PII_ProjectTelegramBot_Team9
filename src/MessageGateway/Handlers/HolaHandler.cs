using Telegram.Bot.Types;

namespace MessageGateway
{
  /// <summary>
  /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
  /// </summary>
  public class HolaHandler : HandlerBase
  {
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="HolaHandler"/>. Esta clase procesa el mensaje "hola".
    /// </summary>
    /// <param name="siguiente">El próximo "handler".</param>
    public HolaHandler(HandlerBase siguiente) : base(siguiente)
    {
      this.PalabrasClave = new string[] { "hola" };
    }

    /// <summary>
    /// Procesa el mensaje "hola" y retorna true; retorna false en caso contrario.
    /// </summary>
    /// <param name="mensaje">El mensaje a procesar.</param>
    /// <param name="respuesta">La respuesta al mensaje procesado.</param>
    /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
    protected override bool HandlerInterno(IMessage mensaje, out string respuesta) //mensaje es String
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        respuesta = $"¡Hola! {mensaje.From.FirstName}";
        return true;
      }

      respuesta = string.Empty;
      return false;
    }
  }
}