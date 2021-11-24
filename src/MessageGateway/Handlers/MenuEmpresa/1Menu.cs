namespace MessageGateway.Handlers.MenuEmpresa
{
  public class HandlerMenuEmpresa : MessageHandlerBase
  {
    public HandlerMenuEmpresa(IMessageHandler next = null)
    : base(new PalabrasClaveHandlers[] { PalabrasClaveHandlers.MenuEmpresa }, next)
    {
    }

    protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
    {
      if (this.CanHandler(message))
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendJoin('\n',
        "Estas son las diferentes acciones que puedes realizar:",
        "\n",
        "1. Crear publicación",
        "2. Modificar publicaciones",
        "3. Generar reportes",
        "4. Configurar cuenta",
        "5. Salir");
        response = sb.ToString();
        nextHandlerKeyword = PalabrasClaveHandlers.OpcionesEmpresa;
        return true;
      }
      else
      {
        response = string.Empty;
        nextHandlerKeyword = PalabrasClaveHandlers.MenuEmpresa;
        return false;
      }
    }
  }
}