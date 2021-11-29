//--------------------------------------------------------------------------------
// <copyright file="HandlerRegistroEmprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Forms;
using ClassLibrary.LocationAPI;
using Importers;

namespace MessageGateway.Handlers
{
    /// <summary>
    /// Handler principal del registro de emprendedores.
    /// </summary>
    public class HandlerRegistroEmprendedor : MessageHandlerBase, IMessageHandler
    {
        /// <summary>
        /// Constructor, al instanciarse en un form ya le asigna a este sus estados iniciales necesarios.
        /// </summary>
        /// <param name="next">Siguiente IHandler.</param>
        public HandlerRegistroEmprendedor(IMessageHandler next) : base ((new string[] {"RegistroEmprendedor"}), next)
        {
            this.Next = next;
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
            if ((CurrentForm is FrmRegistroEmprendedor) && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"A continuación, te pediremos que ingreses los datos necesarios para que como emprendedor seas visible en la plataforma...\n");
                sb.Append ($"¿Qué quieres especificar?\n ");
                sb.Append ($"1.Nombre\n");
                sb.Append ($"2.Lugar\n");
                sb.Append ($"3.Rubro\n");
                sb.Append ($"4.Especialización\n");
                sb.Append ($"5.Habilitaciones\n");
                sb.Append ($"6. Listo");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "1" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Ingresa tu nombre. Ten en cuenta que este nombre será público para todos los usuarios.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.TomandoNombre;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.TomandoNombre)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Nombre guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).Nombre = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "3" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿A qué rubro te dedicas como emprendedor?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.TomandoRubro;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.TomandoRubro)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Rubro guardado con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).Rubro = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "4" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Cuál es tu especialidad como emprendedor?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.TomandoEspecializacion;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.TomandoEspecializacion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Especialización guardada con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).Especializacion = message.TxtMensaje;
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.Eligiendo;
                return true;
            }
            else if (message.TxtMensaje == "5" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Cuáles son tus habilitaciones? (\"Ninguna\" es una opción, \"Listo\" cuando hayas finalizado \n");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.TomandoHabilitacion;
                return true;
            }
            else if (message.TxtMensaje != "Ninguna" && message.TxtMensaje != "Listo" && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.TomandoHabilitacion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Habilitación guardada con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).habilitaciones.Add(new Habilitacion(message.TxtMensaje));
                return true;
            }
            else if ((message.TxtMensaje == "Ninguna" || message.TxtMensaje == "Listo") && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.TomandoHabilitacion)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Habilitaciones guardadas con éxito!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.Eligiendo;
                return true;
            }
            else if ((message.TxtMensaje == "6") && (CurrentForm as FrmRegistroEmprendedor).CurrentState == FasesRegEmprendedor.Eligiendo)
            {
                StringBuilder sb = new StringBuilder();
                Emprendedor emprendedor = (CurrentForm as FrmRegistroEmprendedor).emprendedorFinal;
                if (emprendedor != null)
                {
                    (CurrentForm as FrmRegistroEmprendedor).CurrentState = FasesRegEmprendedor.Done;
                    sb.Append("Emprendedor Creado, ¡Bienvenido!");
                    response = sb.ToString();

                    da.Insertar(emprendedor);
                    da.Insertar(emprendedor.DatosLogin);

                    CurrentForm.ChangeForm( new FrmMenuEmprendedor(emprendedor), message.ChatID);
                    return true;
                }

                sb.Append("Algo aún falta completar...");
                response = sb.ToString();
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

       

        /// <summary>
        /// Las diferentes fases que este handler necesita para completar toda su información.
        /// </summary>
        public enum FasesRegEmprendedor
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
            /// Se espera el nombre del emprendedor.
            /// </summary>
            TomandoNombre,

            /// <summary>
            /// Se espera el rubro del emprendedor.
            /// </summary>
            TomandoRubro,

            /// <summary>
            /// Se espera la especialización del emprendedor.
            /// </summary>
            TomandoEspecializacion,

            /// <summary>
            /// Se espera las habilitaciones del emprendedor.
            /// </summary>
            TomandoHabilitacion,

            ///Finalizado el registro.
            Done
        }
        private DataAccess da = DataAccess.Instancia;
    }
}