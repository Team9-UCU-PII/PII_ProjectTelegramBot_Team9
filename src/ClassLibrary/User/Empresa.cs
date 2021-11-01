//--------------------------------------------------------------------------------
// <copyright file="Empresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;

namespace ClassLibrary.User
{
    /// <summary>
    /// Clase representativa de las empresas registradas y su información competente.
    /// </summary>
    public class Empresa: IUsuario
    {
        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        /// <value></value>
        public string Nombre{ get; set;}
        /// <summary>
        /// Local o zona donde se realizaría retiro.
        /// </summary>
        public string Lugar;
        /// <summary>
        /// Rubro de la empresa.
        /// </summary>
        public string Rubro;
        /// <summary>
        /// Descripción de la empresa.
        /// </summary>
        public string Descripcion;
        /// <summary>
        /// Numero de telefono, mail o cualquier via activa de contacto.
        /// </summary>
        public string Contacto;
        /// <summary>
        /// Identificadores clave de la empresa.
        /// </summary>
        public string[] PalabrasClave;
    /// <summary>
    /// Historial de ventas de la empresa.
    /// </summary>
        public List<Venta> Historial = new List<Venta>();
        /// <summary>
        /// Datos para inciar en la empresa.
        /// </summary>
        /// <value></value>
        public DatosLogin DatosLogin { get; private set; }
        /// <summary>
        /// Constructor genérico de la clase Empresa.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="lugar"></param>
        /// <param name="rubro"></param>
        /// <param name="descripcion"></param>
        /// <param name="contacto"></param>
        public Empresa(string nombre, string lugar, string rubro, string descripcion, string contacto)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Descripcion = descripcion;
            this.Contacto = contacto;
        }
        /// <summary>
        /// Contructor vacio para la creación de instancias temporales.
        /// <see cref = "GestorInvitaciones"/>
        /// </summary>
        public Empresa()
        {  
        }

        public void PublicarOferta(
            Residuo residuo, double precioUnitario, string moneda, int cantidad, 
            string lugarRetiro, string descripcion
        ) {
            Publicacion p = new Publicacion(residuo, precioUnitario, moneda, cantidad, lugarRetiro, this, descripcion);
        }

        public void PublicarOfertaRecurrente(
            Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro,
            string descripcion, int frecuenciaAnualRestock
        ) {
            Publicacion p = new PublicacionRecurrente(residuo, precioUnitario, moneda, cantidad,
            lugarRetiro, this, frecuenciaAnualRestock, descripcion);
        }
    }
}