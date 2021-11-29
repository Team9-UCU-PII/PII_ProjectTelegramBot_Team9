//--------------------------------------------------------------------------------
// <copyright file="MenuEmpresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using MessageGateway.Handlers.MenuEmpresa;

namespace MessageGateway.Forms
{

  /// <summary>
  /// Primer formulario del chat luego del login o el registro de una empresa.
  /// </summary>
  public class FrmMenuEmpresa : FormularioBase
  {

    /// <summary>
    /// Constructor del formulario con sus handlers.
    /// </summary>
    public FrmMenuEmpresa(Empresa empresa)
    {
      this.messageHandler =
        new HandlerMenuEmpresa(
          new HandlerOpcionesMenuEmpresa(null)
        );
            this.empresa = empresa;
    }

    /// <summary>
    /// Estado del formulario dado por el HandlerMenuEmpresa.
    /// </summary>
    public HandlerMenuEmpresa.faseMenuEmpresa CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
    
    /// <summary>
    /// Instancia de la empresa parada en el menu.
    /// </summary>
    public Empresa empresa;
    }
}