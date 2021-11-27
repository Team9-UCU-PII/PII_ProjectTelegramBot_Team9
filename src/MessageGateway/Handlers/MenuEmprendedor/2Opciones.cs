using MessageGateway.Forms;

namespace MessageGateway.Handlers.MenuEmprendedor
{
  public class HandlerOpcionesMenuEmprendedor : MessageHandlerBase
  {
    public HandlerOpcionesMenuEmpresa(IMessageHandler next = null)
    : base(new string[] {"1", "2", "3", "4"}, next)
    {
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      response = string.Empty;
      if (this.CanHandle(message) && (CurrentForm as FrmMenuEmprendedor).CurrentState == HandlerMenuEmprendedor.faseMenuEmprendedor.Eligiendo)
      {
        response = string.Empty;
        switch (message.TxtMensaje)
        {
          case "1":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            this.CurrentForm.ChangeForm(new FrmBuscarPublicacion(), message.ChatID);
            break;
          case "2":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            this.CurrentForm.ChangeForm(new FrmReportes(), message.ChatID);
            break;
          case "3":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            this.CurrentForm.ChangeForm(new FrmCuenta(), message.ChatID);
            break;
          case "4":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
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