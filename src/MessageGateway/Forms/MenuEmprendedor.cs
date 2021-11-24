using MessageGateway.Handlers.MenuEmprendedor;

namespace MessageGateway.Forms
{
  public class FrmMenuEmprendedor : FormularioBase
  {
    public FrmMenuEmprendedor() : base(new Dictionary<string, string>
      {
        {"1", "buscar publicaciones"},
        {"2", "generar reportes"},
        {"3", "configurar cuenta"},
        {"4", "salir"}
      })
    {
      this.messageHandler =
        new HandlerMenuEmprendedor(
          new HandlerOpcionesMenuEmprendedor()
        );
    }
  }
}