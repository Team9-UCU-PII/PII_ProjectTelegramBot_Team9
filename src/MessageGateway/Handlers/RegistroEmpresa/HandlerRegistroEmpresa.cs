using System;
using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public class HandlerRegistroEmpresa : MessageHandlerBase, IMessageHandler
    {
        public HandlerRegistroEmpresa(IMessageHandler next) : base ((new string[] {"RegistroEmpresa"}), next)
        {
            this.Next = next;
            (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.Inicio;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmRegistroEmpresa).CurrentState == fases.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"A continuación, te pediremos que ingreses los datos necesarios para que como empresa seas visible en la plataforma...\n");
                sb.Append ($"¿Qué quieres especificar?\n ");
                sb.Append ($"1.Nombre\n");
                sb.Append ($"2.Lugar\n");
                sb.Append ($"3.Rubro\n");
                sb.Append ($"4.Descripción\n");
                sb.Append ($"5.Contacto\n");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "1" && (CurrentForm as FrmRegistroEmpresa).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ingresa tu nombre. Ten en cuenta que este nombre será público para todos los usuarios.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.tomandoNombre;
                return true;
            }
            else if (faseActual == fases.tomandoNombre)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Nombre guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Nombre = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "3" && (CurrentForm as FrmRegistroEmpresa).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿A qué rubro se dedica la empresa?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.tomandoRubro;
                return true;
            }
            else if (faseActual == fases.tomandoRubro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Rubro guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Rubro = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "4" && (CurrentForm as FrmRegistroEmpresa).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Agrega una breve descripción de la empresa.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.tomandoDescripcion;
                return true;
            }
            else if (faseActual == fases.tomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Descripción guardada con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Descripcion = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "5" && (CurrentForm as FrmRegistroEmpresa).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ingresa un contacto para que se puedan comunicar contigo.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.tomandoContacto;
                return true;
            }
            else if (faseActual == fases.tomandoContacto)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Contacto guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Contacto = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fases.Eligiendo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        private fases faseActual;
        public enum fases
        {
            Inicio,
            Eligiendo,
            tomandoNombre,
            tomandoLugar,
            tomandoRubro,
            tomandoDescripcion,
            tomandoContacto
        }
    }
}