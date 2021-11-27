namespace MessageGateway.Handlers.MenuEmprendedor
{
  public class HandlerMenuEmprendedor : MessageHandlerBase
  {
    public HandlerMenuEmprendedor(IMessageHandler next = null)
    : base(new string[] {"MenuEmprendedor"}, next)
    {
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandle(message) && (CurrentForm as FrmMenuEmprendedor).CurrentState == faseMenuEmprendedor.Inicio)
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
        (CurrentForm as FrmMenuEmprendedor).CurrentState = faseMenuEmprendedor.Eligiendo;
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }
    public enum faseMenuEmprendedor
    {
      Inicio,
      Eligiendo

    }
  }
}