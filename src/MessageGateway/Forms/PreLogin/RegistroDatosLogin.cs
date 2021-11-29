//--------------------------------------------------------------------------------
// <copyright file="RegistroDatosLogin.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using MessageGateway.Handlers.RegistroDatosLogin;
using ClassLibrary.User;

namespace MessageGateway.Forms
{

    /// <summary>
    /// Formulario que engloba la creacion de un datosLogin nuevo.
    /// </summary>
    public class FrmRegistroDatosLogin : FormularioBase
    {

        /// <summary>
        /// Atributo en caso de venirse de una invitación.
        /// </summary>
        /// <value>IUsuario.</value>
        public Empresa OrganizacionEnRegistro { get; private set; }

        /// <summary>
        /// El Username con el que se identifica.
        /// </summary>
        public string NombreUsuario;

        /// <summary>
        /// La contraseña a asignarsele.
        /// </summary>
        public string Password;

        /// <summary>
        /// Sostiene la contraseña para confirmarla.
        /// </summary>
        public string passChkr;

        /// <summary>
        /// Obtiene el DatosLogin resultante de los datos tomados.
        /// </summary>
        public DatosLogin result
        {
            get
            {
                if (NombreUsuario != null && Password != null && OrganizacionEnRegistro != null)
                {
                return new DatosLogin(NombreUsuario, Password, OrganizacionEnRegistro);
                }
                return null;
            }
        }

        /// <summary>
        /// Constructor del formulario y sus handlers para un invitado.
        /// </summary>
        /// <param name="organizacion">Empresa Invitada</param>
        public FrmRegistroDatosLogin(Empresa organizacion)
        //Esta sobrecarga permite registrar desde una invitacion.
        {
            this.OrganizacionEnRegistro = organizacion;
            this.messageHandler =
                new HandlerRegDatosLogin(null);
        }

        /// <summary>
        /// Constructor generico del formulario y sus handlers.
        /// </summary>
        public FrmRegistroDatosLogin()
        {
            this.OrganizacionEnRegistro = null;
            this.messageHandler =
                new HandlerRegDatosLogin(null);
        }

        /// <summary>
        /// El estado de la creacion de los DatosLogin.
        /// </summary>
        public HandlerRegDatosLogin.faseRegDL CurrentState = HandlerRegDatosLogin.faseRegDL.Inicio;
    }
}