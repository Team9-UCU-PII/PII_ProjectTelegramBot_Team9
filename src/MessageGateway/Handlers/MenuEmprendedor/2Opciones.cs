using MessageGateway.Forms;

namespace MessageGateway.Handlers.MenuEmprendedor
{
  public class HandlerOpcionesMenuEmprendedor : MessageHandlerBase
  {
    public HandlerOpcionesMenuEmpresa(IMessageHandler next = null)
    : base(new PalabrasClaveHandlers[] { PalabrasClaveHandlers.OpcionesEmprendedor }, next)
    {
    }

    protected override bool InternalHandle(IMessage message, out string response, out PalabrasClaveHandlers nextHandlerKeyword)
    {
      response = string.Empty;
      if (this.CanHandle(message))
      {
        response = "";
        switch (message.TxtMensaje)
        {
          case "buscar publicaciones":
            this.ContainingForm.Next = new FrmBusqueda();
            break;
          case "generar reportes":
            this.ContainingForm.Next = new FrmGenerarReportes(); 
            break;
          case "configurar cuenta":
            this.ContainingForm.Next = new FrmConfigurarCuenta();
            break;
          case "salir":
            this.ContainingForm.Next = new FrmEscape();
            break;
          default:
            nextHandlerKeyword = PalabrasClaveHandlers.OpcionesEmprendedor;
            return false;
        }
        nextHandlerKeyword = PalabrasClaveHandlers.MenuEmprendedor;
        return true;
      }
      else
      {
        nextHandlerKeyword = PalabrasClaveHandlers.OpcionesEmprendedor;
        return false;
      }
    }
  }
}