//--------------------------------------------------------------------------------
// <copyright file="MenuEmprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers.MenuEmprendedor;

namespace MessageGateway.Forms
{

  /// <summary>
  /// Primer formulario del chat luego del login o el registro de un emprendedor.
  /// </summary>
  public class FrmMenuEmprendedor : FormularioBase
  {

    /// <summary>
    /// Constructor del formulario con sus handlers.
    /// </summary>
    public FrmMenuEmprendedor()
    {
      this.messageHandler =
        new HandlerMenuEmprendedor(
          new HandlerOpcionesMenuEmprendedor(null)
        );
    }

    /// <summary>
    /// Estado del formulario dado por el handler MenuEmprendedor.
    /// </summary>
    public HandlerMenuEmprendedor.faseMenuEmprendedor CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
  }
}