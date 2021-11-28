using System;
using System.Text;
using MessageGateway.Forms;
using ClassLibrary.LocationAPI;

namespace MessageGateway.Handlers
{
    public class HandlerAltaOferta: MessageHandlerBase, IMessageHandler
    {
        public HandlerAltaOferta(IMessageHandler next) : base ((new string[] {"CrearOferta"}), next)
        {
            this.Next = next;
            (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Inicio;
            (CurrentForm as FrmAltaOferta).CurrentStateLocation = HandlerLocation.faseLocation.Inicio;
            (CurrentForm as FrmAltaOferta).CurrentStateResiduo = HandlerNewResiduo.fasesResiduo.Inicio;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Vamos a crear una publicación...\n");
                sb.Append ($"¿Qué quieres especificar?\n ");
                sb.Append ($"1.Residuo\n");
                sb.Append ($"2.Costo y Cantidad\n");
                sb.Append ($"3.Lugar de Retiro\n");
                sb.Append ($"4.Descripción\n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "2" && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Que cantidad del residuo dispones? (Segun la unidad dada en la especificacion del residuo)");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoCantidad;
                return true;
            }
            else if (faseActual == fasesAltaOferta.tomandoCantidad)
            {
                StringBuilder sb = new StringBuilder();
                if (int.TryParse(message.TxtMensaje, out int result))
                {
                    (CurrentForm as FrmAltaOferta).Cantidad = result;
                    (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoMoneda;
                    sb.Append($"Añadida la cantidad. Ahora ingresa la moneda que aceptas ($ como pesos, USD como dolares, etc...)");
                    response = sb.ToString();
                    return true;
                }
                else
                {
                    sb.Append($"Intenta de nuevo, asegurate ingresar solo un numero.");
                    response = sb.ToString();
                    return true;
                }
            }
            else if (faseActual == fasesAltaOferta.tomandoMoneda)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmAltaOferta).Moneda = message.TxtMensaje;
                sb.Append($"Añadida la moneda, ahora dime el valor unitario (puede ser decimal, con un punto, como ejemplo: 2.5)");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoPrecio;
                return true;
            }
            else if ((faseActual == fasesAltaOferta.tomandoPrecio))
            {
                StringBuilder sb = new StringBuilder();
                if (double.TryParse(message.TxtMensaje, out double result))
                {
                    (CurrentForm as FrmAltaOferta).PrecioUnitario = result;
                    (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                    sb.Append($"Listo");
                    response = sb.ToString();
                    return true;
                }
                else
                {
                    sb.Append($"Intenta de nuevo, asegurate ingresar solo un numero y con punto decimal de ser necesario.");
                    response = sb.ToString();
                    return true;
                }
            }
            else if (message.TxtMensaje == "4" && (CurrentForm as FrmAltaOferta).CurrentState == fasesAltaOferta.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Como describirías la publicación?");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.tomandoDescripcion;
                return true;
            }
            else if (faseActual == fasesAltaOferta.tomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Guardada la descripción");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).Descripcion = message.TxtMensaje;
                (CurrentForm as FrmAltaOferta).CurrentState = fasesAltaOferta.Eligiendo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        private fasesAltaOferta faseActual;
        public enum fasesAltaOferta
        {
            Inicio,
            Eligiendo,
            ArmandoResiduo,
            ArmandoLocation,
            tomandoResiduo,
            tomandoCantidad,
            tomandoMoneda,
            tomandoPrecio,
            tomandoDescripcion
        }

        
        public string dpto = "Montevideo";
        public string city = "Montevideo";
        public Location locationFinal;

    }
}