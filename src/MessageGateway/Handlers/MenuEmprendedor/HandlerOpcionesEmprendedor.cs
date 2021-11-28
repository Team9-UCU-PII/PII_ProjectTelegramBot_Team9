//--------------------------------------------------------------------------------
// <copyright file="HandlerOpcionesEmprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Forms;

namespace MessageGateway.Handlers.MenuEmprendedor
{

  /// <summary>
  /// Handler que según la opción seleccionada, redirecciona al formulario correspondiente a la acción.
  /// </summary>
  public class HandlerOpcionesMenuEmprendedor : MessageHandlerBase
  {

    /// <summary>
    /// El constructor, las palabras claves hacen referencia a la opciones del menú del emprendedor.
    /// </summary>
    /// <param name="next"></param>
    public HandlerOpcionesMenuEmpresa(IMessageHandler next = null)
    : base(new string[] {"1", "2", "3", "4"}, next)
    {
    }

    /// <summary>
    /// Método InternalHandle que redirecciona a formularios según la opción seleccionada.
    /// </summary>
    /// <param name="message">IMessage traido del form.</param>
    /// <param name="response">String de la respuesta al usuario.</param>
    /// <returns>True: si se pudo manejar el mensaje.</returns>
    protected override bool InternalHandle(IMessage message, out string response)
    {
      response = string.Empty;
      if (this.CanHandle(message) && (CurrentForm as FrmMenuEmprendedor).CurrentState == HandlerMenuEmprendedor.faseMenuEmprendedor.Eligiendo)
      {
        response = string.Empty;
        switch (message.TxtMensaje)
        {
          case "1":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            this.CurrentForm.ChangeForm(new FrmBuscarPublicacion(), message.ChatID);
            break;
          case "2":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            this.CurrentForm.ChangeForm(new FrmReportes(), message.ChatID);
            break;
          case "3":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            this.CurrentForm.ChangeForm(new FrmCuenta(), message.ChatID);
            break;
          case "4":
            (CurrentForm as FrmMenuEmprendedor).CurrentState = HandlerMenuEmprendedor.faseMenuEmprendedor.Inicio;
            message.TxtMensaje = "abortar";
            break;
          default:
            return false;
        }
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}