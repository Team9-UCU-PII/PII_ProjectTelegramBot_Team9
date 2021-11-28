//--------------------------------------------------------------------------------
// <copyright file="HandlerRegistroEmpresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
    /// <summary>
    /// Handler principal del registro de empresas.
    /// </summary>
    public class HandlerRegistroEmpresa : MessageHandlerBase, IMessageHandler
    {
        /// <summary>
        /// Constructor, al instanciarse en un form ya le asigna a este sus estados iniciales necesarios.
        /// </summary>
        /// <param name="next">Siguiente IHandler.</param>
        public HandlerRegistroEmpresa(IMessageHandler next) : base ((new string[] {"RegistroEmpresa"}), next)
        {
            this.Next = next;
            (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.Inicio;
            (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = HandlerLocation.faseLocation.Inicio;
        }

        /// <summary>
        /// Internal handle que presenta un menu para ir completando el registro.
        /// Delega la tarea de registro de location a sus handler particular.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmRegistroEmpresa).CurrentState == Fases.Inicio)
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
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "1" && (CurrentForm as FrmRegistroEmpresa).CurrentState == Fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ingresa tu nombre. Ten en cuenta que este nombre será público para todos los usuarios.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.TomandoNombre;
                return true;
            }
            else if (faseActual == Fases.TomandoNombre)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Nombre guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Nombre = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "3" && (CurrentForm as FrmRegistroEmpresa).CurrentState == Fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿A qué rubro se dedica la empresa?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.TomandoRubro;
                return true;
            }
            else if (faseActual == Fases.TomandoRubro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Rubro guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Rubro = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "4" && (CurrentForm as FrmRegistroEmpresa).CurrentState == Fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Agrega una breve descripción de la empresa.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.TomandoDescripcion;
                return true;
            }
            else if (faseActual == Fases.TomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Descripción guardada con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Descripcion = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "5" && (CurrentForm as FrmRegistroEmpresa).CurrentState == Fases.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ingresa un contacto para que se puedan comunicar contigo.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.TomandoContacto;
                return true;
            }
            else if (faseActual == Fases.TomandoContacto)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Contacto guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).Contacto = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmpresa).CurrentState = Fases.Eligiendo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        private Fases faseActual;

        /// <summary>
        /// Las diferentes fases que este handler necesita para completar toda su información.
        /// </summary>
        public enum Fases
        {
            /// <summary>
            /// Iniciador del handler.
            /// </summary>
            Inicio,
            
            /// <summary>
            /// El user está parado en el menú.
            /// </summary>
            Eligiendo,

            /// <summary>
            /// Se espera el nombre de la empresa.
            /// </summary>
            TomandoNombre,

            /// <summary>
            /// Se espera el rubro de la empresa.
            /// </summary>
            TomandoRubro,

            /// <summary>
            /// Se espera una descripción de la empresa.
            /// </summary>
            TomandoDescripcion,

            /// <summary>
            /// Se espera un contacto de la empresa.
            /// </summary>
            TomandoContacto
        }
    }
}