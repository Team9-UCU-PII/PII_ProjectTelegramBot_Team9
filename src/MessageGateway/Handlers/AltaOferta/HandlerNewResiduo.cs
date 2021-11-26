using System.Text;
using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public class HandlerNewResiduo: MessageHandlerBase, IMessageHandler
    {
        public HandlerNewResiduo(IMessageHandler next) : base ((new string[] {"1"}), next)
        {
            this.Next = next;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmAltaOferta).CurrentState == HandlerAltaOferta.fases.ArmandoResiduo)
            //capaz podria tener menos dependencia de FrmAltaOferta, pero la vdd es el unico contexto en el que es necesario.
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Describe de que residuo se trata\n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fases.ArmandoResiduo;
                faseActual = faseResiduo.UnidadMedida;
                return true;
            }
            else if (this.faseActual == faseResiduo.UnidadMedida)
            {
                this.descripcion = message.TxtMensaje;

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Con que unidad se cuantifica? (kgs, mts2, etc...)\n");
                response = sb.ToString();
                faseActual = faseResiduo.Categoria;
                return true;
            }
            else if (this.faseActual == faseResiduo.Categoria)
            {
                this.categoria = new Categoria(message.TxtMensaje);

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Que habilitacion/es se necesitan? (\"Ninguna\" es una opción, \"Listo\" cuando hayas finalizado \n");
                response = sb.ToString();
                faseActual = faseResiduo.Habilitaciones;
                return true;
            }
            else if ((message.TxtMensaje == "Ninguna" || message.TxtMensaje == "Listo") && this.faseActual == faseResiduo.Habilitaciones)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Creado con éxito Residuo");
                response = sb.ToString();

                this.ResiduoFinal = new Residuo(this.categoria,this.descripcion,this.unit,this.habilitaciones);

                (CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fases.Eligiendo;
                (CurrentForm as FrmAltaOferta).residuo = this.ResiduoFinal;
                return true;
            }
            else if (message.TxtMensaje != "Ninguna" && message.TxtMensaje != "Listo" && this.faseActual == faseResiduo.Habilitaciones)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Habilitacion Añadida");
                response = sb.ToString();

                this.habilitaciones.Add(new Habilitacion(message.TxtMensaje));
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }

        }
        private enum faseResiduo
        {
            UnidadMedida,
            Categoria,
            Habilitaciones
        }
        private faseResiduo faseActual;
        private string descripcion;
        private string unit;
        public Categoria categoria;
        private List<Habilitacion> habilitaciones = new List<Habilitacion>();
        public Residuo ResiduoFinal;

    }
}