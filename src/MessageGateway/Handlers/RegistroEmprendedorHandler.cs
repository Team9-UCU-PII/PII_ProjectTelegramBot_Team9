using ClassLibrary.User;

namespace MessageGateway
{
  public class RegistroEmprendedorHandler : HandlerBase
  {
    public RegistroEmprendedorHandler(HandlerBase siguiente) : base(siguiente)
    {
      this.PalabrasClave = new string[] { "registroEmprendedor" };
    }

    protected override bool HandlerInterno(IMessage mensaje, out string respuesta)
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        string estadoDeRegistro = "NOMBRE_EMPRENDEDOR";

        if (estadoDeRegistro == "NOMBRE_EMPRENDEDOR")
        {
          respuesta = "Ingrese el nombre de usuario";
          string nombre = AdaptadorTelegram.Instancia.MensajeRecibido;

          estadoDeRegistro = "DIRECCION_EMPRENDEDOR";

          if (estadoDeRegistro == "DIRECCION_EMPRENDEDO")
          {
            respuesta = "Ingrese dirección";
            string direccion = AdaptadorTelegram.Instancia.MensajeRecibido;

            estadoDeRegistro = "RUBRO_EMPRENDEDOR";

            if (estadoDeRegistro == "RUBRO_EMPRENDEDOR")
            {
              respuesta = "Ingrese el rubro";
              string rubro = AdaptadorTelegram.Instancia.MensajeRecibido;

              estadoDeRegistro = "ESPECIALIZACION_EMPRENDEDOR";

              if(estadoDeRegistro == "ESPECIALIZACION_EMPRENDEDOR")
              {
                respuesta = "Ingrese especialización";
                string especializacion = AdaptadorTelegram.Instancia.MensajeRecibido;

                List<Habilitacion> listahabilitacion = {};
                Emprendedor nuevo_emprendedor = new Emprendedor(nombre, direccion, rubro, especializacion, listahabilitacion);

                return true;
              }
            }
          }
        }
      }
      respuesta = string.Empty;
      return false;
    }
  }
}