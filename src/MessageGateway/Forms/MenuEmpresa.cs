using MessageGateway.Handlers.MenuEmpresa;

namespace MessageGateway.Forms
{
  public class FrmMenuEmpresa : FormularioBase, IFormulario
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