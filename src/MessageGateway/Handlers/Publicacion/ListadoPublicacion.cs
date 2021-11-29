using BotCore.Publicacion;

namespace MessageGateway.Handlers.ListadoPublicaciones
{
  public class HandlerListadoPublicaciones : MessageHandlerBase, IMessageHandler, IListableForm
  {
    public HandlerListadoPublicaciones(IMessageHandler next)
    : base ((new string[] {"ListadoPublicacion"}), next) 
    {
      this.Next = next;
      (CurrentForm as FrmListadoPublicaciones).CurrentState = fasesListPublicacion.Inicio;
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandle(message) && (CurrentForm as FrmListadoPublicaciones).CurrentState == fasesListPublicacion.Inicio)
      {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Lista de publicaciones según los filtros elegidos:\n");
        response = sb.ToString();
        (CurrentForm as FrmListadoPublicaciones).CurrentState = fasesListPublicacion.PublicacionesFiltradas;
        return true;
      }
      else if ((CurrentForm as FrmListadoPublicaciones).CurrentState = fasesListPublicacion.PublicacionesFiltradas)
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Busqueda.BuscarPublicaciones);
        response = sb.ToString();
      }
    } 
  }
}