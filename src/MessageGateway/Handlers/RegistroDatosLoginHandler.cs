using ClassLibrary.User;

namespace MessageGateway
{
  public class RegistrarDLHandler : HandlerBase
  {
    public RegistrarDLHandler(HandlerBase siguiente) : base(siguiente)
    {

    }

    protected override bool HandlerInterno(IMessage mensaje, out string respuesta) //mensaje es IMessage
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        respuesta = "Ingrese nombre de usuario:";
        string nombreUsuario = Console.ReadLine();

        respuesta = "Ingrese contraseña:";
        string contrasenia = Console.ReadLine();

        DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia);

        return true;
      }

      respuesta = string.Empty;
      return false;
    }
}
}