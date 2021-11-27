namespace MessageGateway.Handlers.MenuEmpresa
{
  public class HandlerMenuEmpresa : MessageHandlerBase
  {
    public HandlerMenuEmpresa(IMessageHandler next = null)
    : base(new string[] {"MenuEmpresa"}, next)
    {
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandler(message) && (CurrentForm as FrmMenuEMpresa).CurrentState == faseMenuEmpresa.Inicio)
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
        (CurrentForm as FrmMenuEmpresa).CurrentState = faseMenuEmpresa.Eligiendo;
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }

    public enum faseMenuEmpresa
    {
      Inicio,
      Eligiendo

    }
  }
}