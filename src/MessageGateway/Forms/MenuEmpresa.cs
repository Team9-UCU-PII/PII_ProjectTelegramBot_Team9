using MessageGateway.Handlers.MenuEmpresa;

namespace MessageGateway.Forms
{
  public class FrmMenuEmpresa : FormularioBase
  {
    public FrmMenuEmpresa()
    {
      this.messageHandler =
        new HandlerMenuEmpresa(
          new HandlerOpcionesMenuEmpresa()
        );
    }
    public HandlerMenuEmpresa.faseMenuEmpresa CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
  }
}