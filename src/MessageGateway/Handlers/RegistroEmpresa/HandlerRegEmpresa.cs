//--------------------------------------------------------------------------------
// <copyright file="HandlerLogin.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------


using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{

    /// <summary>
    /// Handler encargado de crear una nueva empresa.
    /// </summary>
    public class HandlerRegEmpresa : MessageHandlerBase
    {

        /// <summary>
        /// Constructor en blanco porque es invocado directamente.
        /// </summary>
        /// <param name="next">IHandler siguiente.</param>
        public HandlerRegEmpresa(IMessageHandler next)
        : base(new string[] {}, next)
        {
        }

        /// <summary>
        /// Internal Handle que va tomando los datos necesarios. Location es delegado al HandlerLocation.
        /// </summary>
        /// <param name="message">IMessage traido del form.</param>
        /// <param name="response">String. Repsuesta al user.</param>
        /// <returns>True: si se pudo procesar el mensaje.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm is FrmRegistroEmpresa) && (CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "A continuación, te pediremos que ingreses los datos necesarios para que tu empresa sea visible en la plataforma.",
                "\n",
                $"Primero, ¿Es {(CurrentForm as FrmRegistroEmpresa).EmpresaPreCreada.Nombre} el nombre correcto? (Escribe \"Si\" si está Correcto o Escribe el nuevo nombre que quieras darle.)\n");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoNombre;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoNombre)
            {
                StringBuilder sb = new StringBuilder();

                if (message.TxtMensaje.ToLower() != "si")
                {
                    (CurrentForm as FrmRegistroEmpresa).NombrePublico = message.TxtMensaje;
                }
                else
                {
                    (CurrentForm as FrmRegistroEmpresa).NombrePublico = (CurrentForm as FrmRegistroEmpresa).EmpresaPreCreada.Nombre;
                }

                sb.Append($"¿A que rubro se dedica {(CurrentForm as FrmRegistroEmpresa).EmpresaPreCreada.Nombre}?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoRubro;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoRubro)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).Rubro = message.TxtMensaje;
                sb.Append("¿Como describirías la empresa?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoDescripcion;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoDescripcion)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).Descripcion = message.TxtMensaje;
                sb.Append("¿Por que medios se puede contactar?  \n(Dejanos un numero de teléfono o E-Mail por ejemplo)");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.tomandoContacto;
                return true;
            }
            else if ((CurrentForm as FrmRegistroEmpresa).CurrentState == fasesRegEmpresa.tomandoContacto)
            {
                StringBuilder sb = new StringBuilder();
                (CurrentForm as FrmRegistroEmpresa).Contacto = message.TxtMensaje;
                sb.Append("¿La empresa se ubica en Montevideo?");
                response = sb.ToString();
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.ArmandoLocation;
                (CurrentForm as FrmRegistroEmpresa).CurrentStateLocation = HandlerLocation.faseLocation.tomandoMvdeo;
                return true;
            }
            else if (((CurrentForm as FrmRegistroEmpresa).EmpresaFinal != null))
            {
                response = "Registrado con éxito";
                (CurrentForm as FrmRegistroEmpresa).CurrentState = fasesRegEmpresa.Done;
                (CurrentForm as FrmRegistroEmpresa).ChangeForm(new FrmRegistroDatosLogin((CurrentForm as FrmRegistroEmpresa).EmpresaFinal), message.ChatID);
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Fases para controlar en que parte se halla del formulario el usuario.
        /// </summary>
        public enum fasesRegEmpresa
        {
            ///Se inició el registro.
            Inicio,

            ///Esperando el nombre publico.
            tomandoNombre,

            ///Esperando el rubro.
            tomandoRubro,

            ///Tomando una descripcion.
            tomandoDescripcion,

            ///Tomando el contacto.
            tomandoContacto,

            ///Finalizado y construido el Location.
            ArmandoLocation,
            
            ///Listo el registro.
            Done
        }
    }
}