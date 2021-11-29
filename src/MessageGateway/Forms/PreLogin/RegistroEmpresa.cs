//--------------------------------------------------------------------------------
// <copyright file="RegistroEmpresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{

    /// <summary>
    /// Formulario que engloba la creacion de una empresa.
    /// </summary>
    public class FrmRegistroEmpresa : FormularioBase , ILocationForm
    {

        /// <summary>
        /// El nombre publico de la empresa.
        /// </summary>
        public string NombrePublico;

        /// <summary>
        /// Rubro de la empresa.
        /// </summary>
        public string Rubro;

        /// <summary>
        /// Descripción de la empresa.
        /// </summary>
        public string Descripcion;

        /// <summary>
        /// Un medio de contacto con la empresa.
        /// </summary>
        public string Contacto;

        /// <summary>
        /// Instancia de Empresa precargada (desde una invitación).
        /// </summary>
        public Empresa EmpresaPreCreada;

        /// <summary>
        /// Credenciales de loggeo de la empresa.
        /// </summary>
        public DatosLogin UserCreds;

        /// <summary>
        /// LA empresa que se crea desde este form.
        /// </summary>
        /// <value>Empresa.</value>
        public Empresa EmpresaFinal
        {
            get
            {
                if (NombrePublico != null && Ubicacion != null && Rubro != null && Descripcion != null && Contacto != null && UserCreds != null)
                {
                    return new Empresa(NombrePublico,Ubicacion,Rubro,Descripcion,Contacto, UserCreds);
                }
                return null;
            }
        }

        /// <summary>
        /// Formulario encargado de almacenar la informacion para registrar una empresa.
        /// </summary>
        public FrmRegistroEmpresa(IUsuario empresa, DatosLogin Credentials)
        {
            CurrentState = HandlerRegEmpresa.fasesRegEmpresa.Inicio;
            this.UserCreds = Credentials;
            this.messageHandler =
                new HandlerRegEmpresa(
                    new HandlerLocation(null)
                );
            this.EmpresaPreCreada = empresa as Empresa;
            this.city = "Montevideo";
            this.dpto = "Montevideo";
        }

        /// <summary>
        /// Estado del formulario respecto la creacion gral de la Empresa.
        /// </summary>
        public HandlerRegEmpresa.fasesRegEmpresa CurrentState;

        /// <summary>
        /// Estado de la construccion del Location especificamente.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerLocation.faseLocation.</value>
        public HandlerLocation.faseLocation CurrentStateLocation { get; set; }
        
        /// <summary>
        /// Ubicación del local o sede de la Empresa.
        /// </summary>
        /// <value>Location.</value>
        public Location Ubicacion
        {
            get
            {
                return LocationApiClient.Instancia.GetLocation(direccion,city,dpto);
            }
        }

        /// <summary>
        /// Departamento de la sede.
        /// </summary>
        /// <value>String.</value>
        public string dpto { get; set; }

        /// <summary>
        /// Ciudad de la sede.
        /// </summary>
        /// <value>String.</value>
        public string city { get; set; }

        /// <summary>
        /// Direccion de la sede (calle y puerta o Km).
        /// </summary>
        /// <value></value>
        public string direccion { get; set; }
    }
}