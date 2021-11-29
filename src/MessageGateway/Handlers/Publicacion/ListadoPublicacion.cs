namespace MessageGateway.Handlers.ListadoPublicaciones
{
  public class HandlerListadoPublicaciones : MessageHandlerBase, IMessageHandler
  {
    public HandlerListadoPublicaciones(IMessageHandler next)
    : base ((new string[] {"ListadoPublicacion"}), next) 
    {
      this.Next = next;
      (CurrentForm as FrmListadoPublicaciones).CurrentState = false.Inicio;
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandle(message) && (CurrentForm as FrmListadoPublicaciones).CurrentState == fases.Inicio)
      {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Lista de publicaciones según los filtros elegidos:\n");
      }
    }
  }
}