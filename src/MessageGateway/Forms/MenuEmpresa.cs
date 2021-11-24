using MessageGateway.Handlers.MenuEmpresa;

namespace MessageGateway.Forms
{
  public class FrmMenuEmpresa : FormularioBase
  {
    public FrmMenuEmpresa() : base(new Dictionary<string, string>
      {
        {"1", "crear publicacion"},
        {"2", "modificar publicaciones"},
        {"3", "generar reportes"},
        {"4", "configurar cuenta"},
        {"5", "salir"}
      })
    {
      this.messageHandler =
        new HandlerMenuEmpresa(
          new HandlerOpcionesMenuEmpresa()
        );
    }
  }
}