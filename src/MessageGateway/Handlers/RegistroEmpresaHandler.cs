using ClassLibrary.User;

namespace MessageGateway
{
  public class RegistroEmpresaHandler : HandlerBase
  {
    public RegistroEmpresaHandler(HandlerBase siguiente) : base(siguiente)
    {
      this.PalabrasClave = new string[] { "registroEmpresa" };
    }

    protected override bool HandlerInterno(IMessage mensaje, out string respuesta)
    {
      if (this.PuedeProcesarHandler(mensaje))
      {
        string estadoDeRegistro = "NOMBRE_EMPRESA";

        if (estadoDeRegistro == "NOMBRE_EMPRESA")
        {
          respuesta = "Ingrese el nombre de la empresa";
          string nombre = AdaptadorTelegram.Instancia.MensajeRecibido;

          estadoDeRegistro = "DIRECCION_EMPRESA";

          if (estadoDeRegistro == "DIRECCION_EMPRESA")
          {
            respuesta = "Ingrese dirección de la empresa";
            string direccion = AdaptadorTelegram.Instancia.MensajeRecibido;

            estadoDeRegistro = "RUBRO_EMPRESA";

            if (estadoDeRegistro == "RUBRO_EMPRESA")
            {
              respuesta = "Ingrese el rubro";
              string rubro = AdaptadorTelegram.Instancia.MensajeRecibido;

              estadoDeRegistro = "DESCRIPCION_EMPRESA";

              if(estadoDeRegistro == "DESCRIPCION_EMPRESA")
              {
                respuesta = "Ingrese una descripción";
                string descripcion = AdaptadorTelegram.Instancia.MensajeRecibido;

                estadoDeRegistro = "CONTACTO_EMPRESA";

                if(estadoDeRegistro == "CONTACTO_EMPRESA")
                {
                  respuesta = "Ingrese un contacto";
                  string contacto = AdaptadorTelegram.Instancia.MensajeRecibido;

                  Empresa nueva_empresa = new Empresa(nombre, direccion, rubro, descripcion, contacto);
                  return true;
                }
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