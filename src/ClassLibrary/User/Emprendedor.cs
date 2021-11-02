//--------------------------------------------------------------------------------
// <copyright file="Emprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;

namespace ClassLibrary.User
{

    /// <summary>
    /// Clase representativa de los emprendedores con su información competente.
    /// </summary>
    public class Emprendedor: IUsuario
    {

        /// <summary>
        /// Nombre del emprendimiento o emprendedor.
        /// </summary>
        /// <value><see langword="string"/></value>
        public string Nombre{ get; set;}

    /// <summary>
    /// Localizacion del local o residencia del emprendedor.
    /// </summary>
        public string Lugar;

        /// <summary>
        /// Rubro general del emprendedor.
        /// </summary>
        public string Rubro;

        /// <summary>
        /// La especialización del emprendedor.
        /// </summary>
        public string Especializacion;

        /// <summary>
        /// Habilitaciones vigentes del emprendedor.
        /// </summary>
        public string[] Habilitaciones;

        /// <summary>
        /// Historial de las ventas del emprendedor.
        /// </summary>
        public List<Venta> Historial = new List<Venta>();

        /// <summary>
        /// Los datos necesarios para loggearse a dicho emprendedor.
        /// </summary>
        /// <value><see cref = "DatosLogin"/></value>
        public DatosLogin DatosLogin {get; private set;}

        /// <summary>
        /// Constructor generico del emprendedor.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="lugar"></param>
        /// <param name="rubro"></param>
        /// <param name="especializacion"></param>
        public Emprendedor(string nombre, string lugar, string rubro, string especializacion)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Especializacion = especializacion;
        }

        /// <summary>
        /// Un constructor vacio para la creacion temporal de emprendedor
        /// en el GestorInvitaciones.
        /// </summary>
        public Emprendedor()
        {
        }
    }
}