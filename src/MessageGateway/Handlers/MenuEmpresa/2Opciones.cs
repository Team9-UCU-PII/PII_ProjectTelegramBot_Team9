using MessageGateway.Forms;

namespace MessageGateway.Handlers.MenuEmpresa
{
  public class HandlerOpcionesMenuEmpresa : MessageHandlerBase
  {
    public HandlerOpcionesMenuEmpresa(IMessageHandler next = null)
    : base(new string[] {"1", "2", "3", "4", "5"}, next)
    {
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      response = string.Empty;
      if (this.CanHandle(message) && (CurrentForm as FrmMenuEmpresa).CurrentState == HandlerMenuEmpresa.faseMenuEmpresa.Eligiendo)
      {
        response = string.Empty;
        switch (message.TxtMensaje)
        {
          case "1":
            (CurrentForm as FrmMenuEmpresa).CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
            this.CurrentForm.ChangeForm(new FrmAltaOferta(), message.ChatID);
            break;
          //No se los nombres de los formularios, aún no estan hechos.
          case "2":
            (CurrentForm as FrmMenuEmpresa).CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
            this.CurrentForm.ChangeForm(new FrmModificarPublicacion(), message.ChatID);
            break;
          case "3":
            (CurrentForm as FrmMenuEmpresa).CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
            this.CurrentForm.ChangeForm(new FrmReportes(), message.ChatID);
            break;
          case "4":
            (CurrentForm as FrmMenuEmpresa).CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
            this.CurrentForm.ChangeForm(new FrmCuenta(), message.ChatID);
            break;
          case "5":
            (CurrentForm as FrmMenuEmpresa).CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
            message.TxtMensaje = "abortar";
            break;
          default:
            return false;
        }
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}