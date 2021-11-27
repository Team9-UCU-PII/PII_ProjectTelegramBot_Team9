using MessageGateway.Handlers.MenuEmprendedor;

namespace MessageGateway.Forms
{
  public class FrmMenuEmprendedor : FormularioBase
  {
    public FrmMenuEmprendedor()
    {
      this.messageHandler =
        new HandlerMenuEmprendedor(
          new HandlerOpcionesMenuEmprendedor()
        );
    }
    public HandlerMenuEmprendedor.faseMenuEmprendedor CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
  }
}