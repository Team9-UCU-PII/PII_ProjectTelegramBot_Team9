using MessageGateway.Forms;

namespace MessageGateway.Handlers.MenuEmpresa
{
  public class HandlerOpcionesMenuEmpresa : MessageHandlerBase
  {
    public HandlerOpcionesMenuEmpresa(IMessageHandler next = null)
    : base(new PalabrasClaveHandlers[] { PalabrasClaveHandlers.Opciones }, next)
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
          case "crear publicacion":
            this.ContainingForm.Next = new FrmAltaOferta();
            break;
          case "modificar publicaciones":
            this.ContainingForm.Next = new FrmModificarOferta();
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
            nextHandlerKeyword = PalabrasClaveHandlers.Opciones;
            return false;
        }
        nextHandlerKeyword = PalabrasClaveHandlers.Inicio;
        return true;
      }
      else
      {
        nextHandlerKeyword = PalabrasClaveHandlers.Opciones;
        return false;
      }
    }
  }
}