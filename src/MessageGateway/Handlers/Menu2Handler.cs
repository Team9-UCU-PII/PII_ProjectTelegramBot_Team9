using Telegram.Bot.Types;
using System;

namespace MessageGateway
{
  public class Menu2Handler : HandlerBase
  {
    public Menu2Handler(HandlerBase siguiente) : base(siguiente)
    {
      this.PalabrasClave = new string[] { "1", "2" };
    }

    protected override bool HandlerInterno(IMessage mensaje, out string respuesta) //mensaje es String
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        if (mensaje.ToString() == "1")
        {
          IHandler RegistrarDL = new RegistrarDLHandler(this);
          respuesta = "Ingrese nombre de usuario:";
          string nombreUsuario = Console.ReadLine();

          respuesta = "Ingrese contraseña:";
          string contrasenia = Console.ReadLine();

          DatosLogin dl = new DatosLogin(nombreUsuario, contrasenia);

          return true;
        }
      }

      respuesta = string.Empty;
      return false;
    }
  }
}