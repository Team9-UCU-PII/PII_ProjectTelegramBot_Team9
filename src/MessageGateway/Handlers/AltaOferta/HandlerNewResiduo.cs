//--------------------------------------------------------------------------------
// <copyright file="HandlerNewResiduo.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{

    /// <summary>
    /// Handler con el destino de crear un residuo.
    /// </summary>
    public class HandlerNewResiduo: MessageHandlerBase, IMessageHandler
    {

        /// <summary>
        /// Constructor, se agregan palabras clave para menus que lo invoquen.
        /// </summary>
        /// <param name="next">IHandler siguiente.</param>
        public HandlerNewResiduo(IMessageHandler next) : base ((new string[] {"1"}), next)
        {
            this.Next = next;
        }

        /// <summary>
        /// InternalHandle que va tomando las cualidades necesarias para crear un residuo.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">Respuesta al User.</param>
        /// <returns>True: si se pudo manejar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm is IResiduoForm) && (CurrentForm as IResiduoForm).CurrentStateResiduo == fasesResiduo.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Describe de que residuo se trata\n");
                response = sb.ToString();
                (CurrentForm as IResiduoForm).CurrentStateResiduo = fasesResiduo.Descripcion;
                return true;
            }
            else if ((CurrentForm as IResiduoForm).CurrentStateResiduo == fasesResiduo.Descripcion)
            {
                (CurrentForm as IResiduoForm).descripcion = message.TxtMensaje;

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Con que unidad se cuantifica? (kgs, mts2, etc...)\n");
                response = sb.ToString();
                (CurrentForm as IResiduoForm).CurrentStateResiduo = fasesResiduo.UnidadMedida;
                return true;
            }
            else if ((CurrentForm as IResiduoForm).CurrentStateResiduo == fasesResiduo.UnidadMedida)
            {
                (CurrentForm as IResiduoForm).unit = message.TxtMensaje;

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Como la categorizarías?");
                response = sb.ToString();
                (CurrentForm as IResiduoForm).CurrentStateResiduo = fasesResiduo.Categoria;
                return true;
            }
            else if ((CurrentForm as IResiduoForm).CurrentStateResiduo == fasesResiduo.Categoria)
            {
                (CurrentForm as IResiduoForm).categoria = new Categoria(message.TxtMensaje);

                StringBuilder sb = new StringBuilder();
                sb.Append($"¿Que habilitacion/es se necesitan? (\"Ninguna\" es una opción, \"Listo\" cuando hayas finalizado \n");
                response = sb.ToString();
                (CurrentForm as IResiduoForm).CurrentStateResiduo = fasesResiduo.Habilitaciones;
                return true;
            }
            else if ((message.TxtMensaje == "Ninguna" || message.TxtMensaje == "Listo") && (CurrentForm as IResiduoForm).CurrentStateResiduo == fasesResiduo.Habilitaciones)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Creado con éxito Residuo");
                response = sb.ToString();

                (CurrentForm as IResiduoForm).CurrentStateResiduo = fasesResiduo.Inicio;
                return true;
            }
            else if (message.TxtMensaje != "Ninguna" && message.TxtMensaje != "Listo" && (CurrentForm as IResiduoForm).CurrentStateResiduo == fasesResiduo.Habilitaciones)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Habilitacion Añadida");
                response = sb.ToString();

                if ((CurrentForm as IResiduoForm).habilitaciones == null)
                {
                    (CurrentForm as IResiduoForm).habilitaciones = new List<Habilitacion>();
                }
                (CurrentForm as IResiduoForm).habilitaciones.Add(new Habilitacion(message.TxtMensaje));
                (CurrentForm as IResiduoForm).CurrentStateResiduo = fasesResiduo.Inicio;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }

        }

        /// <summary>
        /// Las fases requeridas para crear un residuo.
        /// </summary>
        public enum fasesResiduo
        {

            ///Esperando ser llamado.
            Inicio,

            ///esperando la descripcion.
            Descripcion,

            ///Esperando la unidad de medida.
            UnidadMedida,

            ///Esperando la Categoria del residuo.
            Categoria,

            ///Tomando las habilitaciones necesarias.
            Habilitaciones,
 
            ///Terminado el residuo (si no se quiere poder sobreescribir).
            Done
        }
    }
}