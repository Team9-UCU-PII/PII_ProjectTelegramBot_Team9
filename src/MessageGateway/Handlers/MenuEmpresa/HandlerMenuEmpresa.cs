//--------------------------------------------------------------------------------
// <copyright file="HandlerMenuEmpresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace MessageGateway.Handlers.MenuEmpresa
{

  /// <summary>
  /// Handler principal del menu de empresa.
  /// </summary>
  public class HandlerMenuEmpresa : MessageHandlerBase
  {

    /// <summary>
    /// El constructor, reacciona a la palabra clave MenuEmpresa.
    /// </summary>
    /// <param name="next">IHandler siguiente</param>
    public HandlerMenuEmpresa(IMessageHandler next = null)
    : base(new string[] {"MenuEmpresa"}, next)
    {
    }

    /// <summary>
    /// Método InternalHandle que devuelve un menú y deriva a la opción seleccionada.
    /// </summary>
    /// <param name="message">IMessage traido del form.</param>
    /// <param name="response">String de la respuesta al usuario.</param>
    /// <returns>True: si se pudo manejar.</returns>
    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandler(message) && (CurrentForm as FrmMenuEMpresa).CurrentState == faseMenuEmpresa.Inicio)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendJoin('\n',
        "Estas son las diferentes acciones que puedes realizar:",
        "\n",
        "1. Crear publicación",
        "2. Modificar publicaciones",
        "3. Generar reportes",
        "4. Configurar cuenta",
        "Si quiere salir escriba: /abortar");
        response = sb.ToString();
        (CurrentForm as FrmMenuEmpresa).CurrentState = faseMenuEmpresa.Eligiendo;
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }

    /// <summary>
    /// Las fases del menú de empresa.
    /// </summary>
    public enum faseMenuEmpresa
    {
      Inicio,
      Eligiendo

    }
  }
}