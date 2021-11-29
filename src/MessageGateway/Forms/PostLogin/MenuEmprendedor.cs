//--------------------------------------------------------------------------------
// <copyright file="MenuEmprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using MessageGateway.Handlers.MenuEmprendedor;

namespace MessageGateway.Forms
{

  /// <summary>
  /// Primer formulario del chat luego del login o el registro de un emprendedor.
  /// </summary>
  public class FrmMenuEmprendedor : FormularioBase, IPostLogin
  {

    /// <summary>
    /// Constructor del formulario con sus handlers.
    /// </summary>
    public FrmMenuEmprendedor(IUsuario emprendedor)
    {
      this.messageHandler =
        new HandlerMenuEmprendedor(
          new HandlerOpcionesMenuEmprendedor(null)
        );
      this.InstanciaLoggeada = emprendedor;
      this.CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
    }

    /// <summary>
    /// Estado del formulario dado por el handler MenuEmprendedor.
    /// </summary>
    public HandlerMenuEmprendedor.faseMenuEmprendedor CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;

    /// <summary>
    /// Obtiene o establece la instancia que refiere el emprendedor en este form.
    /// </summary>
    /// <value>Emprendedor.</value>
    public IUsuario InstanciaLoggeada { get; set;}
  }
}