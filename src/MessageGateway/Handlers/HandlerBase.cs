using System;
using System.Linq;
using Telegram.Bot.Types;

namespace MessageGateway
{
  public abstract class HandlerBase : IHandler
  {
    /// <summary>
    /// Obtiene el próximo "handler".
    /// </summary>
    /// <value>El "handler" que será invocado si este "handler" no procesa el mensaje.</value>
    public IHandler Siguiente { get; set; }

    /// <summary>
    /// Obtiene o asigna el conjunto de palabras clave que este "handler" puede procesar.
    /// </summary>
    /// <value>Un array de palabras clave.</value>
    public string[] PalabrasClave { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="HandlerBase"/>.
    /// </summary>
    /// <param name="siguiente">El próximo "handler".</param>
    public HandlerBase(IHandler siguiente)
    {
      this.Siguiente = siguiente;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="HandlerBase"/> con una lista de comandos.
    /// </summary>
    /// <param name="palabrasClave">La lista de comandos.</param>
    /// <param name="siguiente">El próximo "handler".</param>
    public HandlerBase(string[] palabrasClave, HandlerBase siguiente)
    {
      this.PalabrasClave = palabrasClave;
      this.Siguiente = siguiente;
    }

    /// <summary>
    /// Este método debe ser sobreescrito por las clases sucesores. La clase sucesora procesa el mensaje y retorna
    /// true o no lo procesa y retorna false.
    /// </summary>
    /// <param name="mensaje">El mensaje a procesar.</param>
    /// <param name="respuesta">La respuesta al mensaje procesado.</param>
    /// <returns>true si el mensaje fue procesado; false en caso contrario</returns>
    protected abstract bool HandlerInterno(Message mensaje, out string respuesta);
    // {
    //     throw new InvalidOperationException("Este método debe ser sobrescrito");
    // }

    /// <summary>
    /// Este método puede ser sobreescrito en las clases sucesores que procesan varios mensajes cambiando de estado
    /// entre mensajes deben sobreescribir este método para volver al estado inicial. En la clase base no hace nada.
    /// </summary>
    protected virtual void CancelacionInterna()
    {
      // Intencionalmente en blanco.
    }

    /// <summary>
    /// Determina si este "handler" puede procesar el mensaje. En la clase base se utiliza el array
    /// <see cref="HandlerBase.PalabrasClave"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. Las
    /// clases sucesores pueden sobreescribir este método para proveer otro mecanismo para determina si procesan o no
    /// un mensaje.
    /// </summary>
    /// <param name="mensaje">El mensaje a procesar.</param>
    /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
    protected virtual bool PuedeProcesarHandler(IMessage mensaje)
    {
      // Cuando no hay palabras clave este método debe ser sobreescrito por las clases sucesoras y por lo tanto
      // este método no debería haberse invocado.
      if (this.PalabrasClave == null || this.PalabrasClave.Length == 0)
      {
        throw new InvalidOperationException("No hay palabras clave que puedan ser procesadas");
      }

      return this.PalabrasClave.Any(s => mensaje.Text.Equals(s, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// Procesa el mensaje o la pasa al siguiente "handler" si existe.
    /// </summary>
    /// <param name="mensaje">El mensaje a procesar.</param>
    /// <param name="respuesta">La respuesta al mensaje procesado.</param>
    /// <returns>El "handler" que procesó el mensaje si el mensaje fue procesado; null en caso contrario.</returns>
    public IHandler Handle(IMessage mensaje, out string respuesta)
    {
      if (this.HandlerInterno(mensaje, out respuesta))
      {
        return this;
      }
      else if (this.Siguiente != null)
      {
        return this.Siguiente.Handle(mensaje, out respuesta);
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Retorna este "handler" al estado inicial. En los "handler" sin estado no hace nada. Los "handlers" que
    /// procesan varios mensajes cambiando de estado entre mensajes deben sobreescribir este método para volver al
    /// estado inicial.
    /// </summary>
    public virtual void Cancelar()
    {
      this.CancelacionInterna();
      if (this.Siguiente != null)
      {
        this.Siguiente.Cancelar();
      }
    }
  }
}