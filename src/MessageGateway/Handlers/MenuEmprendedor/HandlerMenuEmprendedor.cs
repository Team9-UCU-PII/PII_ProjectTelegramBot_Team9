//--------------------------------------------------------------------------------
// <copyright file="HandlerMenuEmprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace MessageGateway.Handlers.MenuEmprendedor
{

  /// <summary>
  /// Handler principal del menú del emprendedor.
  /// </summary>
  public class HandlerMenuEmprendedor : MessageHandlerBase
  {

    /// <summary>
    /// Constructor del handler que reacciona a la palabra clave MenuEmprendedor.
    /// </summary>
    /// <param name="next">IHandler siguiente.</param>
    public HandlerMenuEmprendedor(IMessageHandler next = null)
    : base(new string[] {"MenuEmprendedor"}, next)
    {
    }

    /// <summary>
    /// Método InternalHandle que devuelve un menú y deriva a la opción seleccionada.
    /// </summary>
    /// <param name="message">IMessage traido del Form.</param>
    /// <param name="response">La respuesta al usuario.</param>
    /// <returns>True: si se pudo manejar.</returns>
    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandle(message) && (CurrentForm as FrmMenuEmprendedor).CurrentState == faseMenuEmprendedor.Inicio)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendJoin('\n',
        "Estas son las diferentes acciones que puedes realizar:",
        "\n",
        "1. Buscar publicaciones",
        "2. Generar reportes",
        "3. Configurar cuenta",
        "Si quiere salir escriba: /abortar");
        response = sb.ToString();
        (CurrentForm as FrmMenuEmprendedor).CurrentState = faseMenuEmprendedor.Eligiendo;
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }

    /// <summary>
    /// Las fases del menu del emprendedor.
    /// </summary>
    public enum faseMenuEmprendedor
    {
      Inicio,
      Eligiendo

    }
  }
}