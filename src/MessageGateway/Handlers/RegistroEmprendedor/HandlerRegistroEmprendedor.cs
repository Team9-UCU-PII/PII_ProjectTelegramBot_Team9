using System;
using System.Text;
using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    public class HandlerRegistroEmprendedor : MessageHandlerBase, IMessageHandler
    {
        public HandlerRegistroEmprendedor(IMessageHandler next) : base ((new string[] {"RegistroEmprendedor"}), next)
        {
            this.Next = next;
            (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.Inicio;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmRegistroEmprendedor).CurrentState == fases.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"A continuación, te pediremos que ingreses los datos necesarios para que como emprendedor seas visible en la plataforma...\n");
                sb.Append ($"¿Qué quieres especificar?\n ");
                sb.Append ($"1.Nombre\n");
                sb.Append ($"2.Lugar\n");
                sb.Append ($"3.Rubro\n");
                sb.Append ($"4.Especialización\n");
                sb.Append ($"5.Habilitaciones\n");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "1" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ingresa tu nombre. Ten en cuenta que este nombre será público para todos los usuarios.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.tomandoNombre;
                return true;
            }
            else if (faseActual == fases.tomandoNombre)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Nombre guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).Nombre = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "3" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿A qué rubro te dedicas como emprendedor?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.tomandoRubro;
                return true;
            }
            else if (faseActual == fases.tomandoRubro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Rubro guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).Rubro = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "4" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Cuál es tu especialidad como emprendedor?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.tomandoEspecializacion;
                return true;
            }
            else if (faseActual == fases.tomandoEspecializacion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Especialización guardada con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).Especializacion = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "5" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Cuáles son tus habilitaciones? (\"Ninguna\" es una opción, \"Listo\" cuando hayas finalizado \n")");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.tomandoHabilitacion;
                return true;
            }
            else if (message.TxtMensaje != "Ninguna" && message.TxtMensaje != "Listo" && faseActual == fases.tomandoHabilitacion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Habilitación guardada con éxito!");
                response = sb.ToString();
                this.habilitaciones.Add(new Habilitacion(message.TxtMensaje));
                return true;
            }
            else if ((message.TxtMensaje == "Ninguna" || message.TxtMensaje == "Listo") && faseActual == fases.tomandoHabilitacion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Habilitaciones guardadas con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = fases.Eligiendo;
                (CurrentForm as FrmRegistroEmprendedor).Habilitaciones = this.habilitaciones;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        private List<Habilitacion> habilitaciones = new List<Habilitacion>();
        private fases faseActual;
        public enum fases
        {
            Inicio,
            Eligiendo,
            tomandoNombre,
            tomandoRubro,
            tomandoEspecializacion,
            tomandoHabilitacion
        }
    }
}