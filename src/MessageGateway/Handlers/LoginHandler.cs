using ClassLibrary.User;

namespace MessageGateway
{
  public class DLHandler : HandlerBase
  {
    public DLHandler(HandlerBase siguiente) : base(siguiente)
    {
      this.PalabrasClave = new string[] { "login" };
    }

    protected override bool HandlerInterno(IMessage mensaje, out string respuesta)
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        respuesta = "Ingrese nombre de usuario:";
        string nombreUsuario = AdaptadorTelegram.Instancia.MensajeRecibido;

        respuesta = "Ingrese contraseña:";
        string contrasenia = AdaptadorTelegram.Instancia.MensajeRecibido;

        DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia);

        return true;
      }

      respuesta = string.Empty;
      return false;
    }
}
}