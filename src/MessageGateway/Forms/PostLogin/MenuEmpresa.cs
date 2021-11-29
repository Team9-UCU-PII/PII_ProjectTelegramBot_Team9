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
  public class FrmMenuEmpresa : FormularioBase, IPostLogin
  {

    /// <summary>
    /// Constructor del formulario con sus handlers.
    /// </summary>
    public FrmMenuEmpresa(IUsuario empresa)
    {
      this.messageHandler =
        new HandlerMenuEmpresa(
          new HandlerOpcionesMenuEmpresa(null)
        );
            this.InstanciaLoggeada = empresa;
      this.CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
    }

    /// <summary>
    /// Estado del formulario dado por el HandlerMenuEmpresa.
    /// </summary>
    public HandlerMenuEmpresa.faseMenuEmpresa CurrentState = HandlerMenuEmpresa.faseMenuEmpresa.Inicio;
    
    /// <summary>
    /// Obtiene o establece la instancia de la empresa parada en el menu.
    /// </summary>
    /// <value>Empresa.</value>
    public IUsuario InstanciaLoggeada {get; set;}
    }
}