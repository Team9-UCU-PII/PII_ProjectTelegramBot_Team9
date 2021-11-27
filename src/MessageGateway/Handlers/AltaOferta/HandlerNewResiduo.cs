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
            if (this.CanHandle(message) && (CurrentForm as FrmAltaOferta).CurrentState == HandlerAltaOferta.fasesAltaOferta.ArmandoResiduo)
            //capaz podria tener menos dependencia de FrmAltaOferta, pero la vdd es el unico contexto en el que es necesario.
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Describe de que residuo se trata\n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentStateResiduo = fasesResiduo.Descripcion;
                return true;
            }
            else if ((CurrentForm as FrmAltaOferta).CurrentStateResiduo == fasesResiduo.Descripcion)
            {
                this.descripcion = message.TxtMensaje;

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Con que unidad se cuantifica? (kgs, mts2, etc...)\n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentStateResiduo = fasesResiduo.UnidadMedida;
                return true;
            }
            else if ((CurrentForm as FrmAltaOferta).CurrentStateResiduo == fasesResiduo.UnidadMedida)
            {
                this.unit = message.TxtMensaje;

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Como la categorizarías?");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentStateResiduo = fasesResiduo.Categoria;
                return true;
            }
            else if ((CurrentForm as FrmAltaOferta).CurrentStateResiduo == fasesResiduo.Categoria)
            {
                this.categoria = new Categoria(message.TxtMensaje);

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Que habilitacion/es se necesitan? (\"Ninguna\" es una opción, \"Listo\" cuando hayas finalizado \n");
                response = sb.ToString();
                (CurrentForm as FrmAltaOferta).CurrentStateResiduo = fasesResiduo.Habilitaciones;
                return true;
            }
            else if ((message.TxtMensaje == "Ninguna" || message.TxtMensaje == "Listo") && (CurrentForm as FrmAltaOferta).CurrentStateResiduo == fasesResiduo.Habilitaciones)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Creado con éxito Residuo");
                response = sb.ToString();

                (CurrentForm as FrmAltaOferta).residuo = new Residuo(this.categoria,this.descripcion,this.unit,this.habilitaciones);
                (CurrentForm as FrmAltaOferta).CurrentStateResiduo = fasesResiduo.Inicio;
                (CurrentForm as FrmAltaOferta).CurrentState = HandlerAltaOferta.fasesAltaOferta.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje != "Ninguna" && message.TxtMensaje != "Listo" && this.faseActual == fasesResiduo.Habilitaciones)
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
        public enum fasesResiduo
        {
            Inicio,
            Descripcion,
            UnidadMedida,
            Categoria,
            Habilitaciones
        }
        private fasesResiduo faseActual;
        private string descripcion;
        private string unit;
        public Categoria categoria;
        private List<Habilitacion> habilitaciones = new List<Habilitacion>();
        public Residuo ResiduoFinal;

    }
}