namespace MessageGateway.Handlers.MenuEmprendedor
{
  public class HandlerMenuEmprendedor : MessageHandlerBase
  {
    public HandlerMenuEmprendedor(IMessageHandler next = null)
    : base(new PalabrasClaveHandlers[] { PalabrasClaveHandlers.MenuEmprendedor }, next)
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
        "1. Buscar publicaciones",
        "2. Generar reportes",
        "3. Configurar cuenta",
        "4. Salir");
        response = sb.ToString();
        nextHandlerKeyword = PalabrasClaveHandlers.OpcionesEmprendedor;
        return true;
      }
      else
      {
        response = string.Empty;
        nextHandlerKeyword = PalabrasClaveHandlers.MenuEmprendedor;
        return false;
      }
    }
  }
}