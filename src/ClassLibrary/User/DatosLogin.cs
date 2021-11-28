//--------------------------------------------------------------------------------
// <copyright file="DatosLogin.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text.Json.Serialization;
using Importers.Json;

namespace ClassLibrary.User
{
    /// <summary>
    /// Las instancias de esta clase representan los usuarios creados en el bot,
    /// almacendando la empresa o emprendedor al que estan vinculados y su información
    /// de cuenta.
    /// </summary>
    public class DatosLogin : JsonConvertibleBase
    {

        /// <summary>
        /// Id de la Serialización.
        /// </summary>
        /// <value>Int.</value>
        public int SerializationID { get; set; }


        /// <summary>
        /// Obtiene o establece el username de la cuenta.
        /// </summary>
        /// <value><see langword="string"/>.</value>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Obtiene la password de la cuenta.
        /// </summary>
        /// <value><see langword="string"/>.</value>
        public string Contrasenia { get; }

        /// <summary>
        /// Obtiene  el <see cref = "Emprendedor"/> o <see cref = "Empresa"/> a la que se vincula esta cuenta.
        /// </summary>
        /// <value><see cref = "Emprendedor"/> o <see cref = "Empresa"/>.</value>
        [JsonInclude]
        public IUsuario Usuario { get; }

        /// <summary>
        /// Método constructor del usuario.
        /// </summary>
        /// <param name="nombreUsuario"><see langword = "string"/>.</param>
        /// <param name="contrasenia"><see langword = "string"/>.</param>
        /// <param name="usuario"><see iref = "IUsuario"/>.</param>
        public DatosLogin(string nombreUsuario, string contrasenia, IUsuario usuario)
        {
            this.NombreUsuario = nombreUsuario;
            this.Contrasenia = contrasenia;
            this.Usuario = usuario;
        }

        /// <summary>
        /// Cosntructor de Json.
        /// </summary>
        [JsonConstructor]
        public DatosLogin()
        {
            
        }

        /// <summary>
        /// Metodo para guardar en Json.
        /// </summary>
        /// <param name="exporter"></param>
        public override void JsonSave(JsonExporter exporter)
        {
            exporter.Save(this);
        }
    }
}