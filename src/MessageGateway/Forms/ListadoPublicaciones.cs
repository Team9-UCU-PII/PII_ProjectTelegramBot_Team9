namespace MessageGateway.Forms
{
  public class FrmListadoPublicaciones : FormularioBase, IFormulario
  {
    public FrmListadoPublicaciones()
    {
      this.messageHandler =
      new HandlerListadoPublicaciones(
        new HandlerEscape(null)
      );
    }

    public HandlerListadoPublicaciones.fases CurrentState {get; set;}
  }
}