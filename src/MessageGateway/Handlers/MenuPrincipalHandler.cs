using Telegram.Bot.Types;

namespace MessageGateway
{
  public class MenuHandler : HandlerBase
  {
    public MenuHandler(HandlerBase siguiente) : base(siguiente)
    {
      this.PalabrasClave = new string[] { "menu" };
    }

    protected override bool HandlerInterno(IMessage mensaje, out string respuesta) //mensaje es String
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        respuesta = " Bienvenido!! \nIngrese el número correspondiente a la accion: \n1- Login. \n2- Tengo un código de invitación.";
        return true;
      }

      respuesta = string.Empty;
      return false;
    }
  }
}