//--------------------------------------------------------------------------------
// <copyright file="Empresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>

// Patrón utilizado: Expert - SRP
// Por SRP delega la tarea de crear publicación.
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using System.Collections.Generic;
using Importers.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary.User
{
    /// <summary>
    /// Clase representativa de las empresas registradas y su información competente.
    /// </summary>
    public class Empresa: JsonConvertibleBase, IUsuario
    {
        /// <summary>
        /// Obtiene o establece el nombre de la empresa
        /// </summary>
        /// <value><see langword="string"/>.</value>
        [JsonInclude]
        public string Nombre { get; set; }

        /// <summary>
        /// Local o zona donde se realizaría retiro.
        /// </summary>
        [JsonInclude]
        public Location Lugar;

        /// <summary>
        /// Rubro de la empresa.
        /// </summary>
        [JsonInclude]
        public string Rubro;

        /// <summary>
        /// Descripción de la empresa.
        /// </summary>
        [JsonInclude]
        public string Descripcion;

        /// <summary>
        /// Numero de telefono, mail o cualquier via activa de contacto.
        /// </summary>
        [JsonInclude]
        public string Contacto;

        /// <summary>
        /// Identificadores clave de la empresa.
        /// </summary>
        [JsonInclude]
        public string[] PalabrasClave;

        /// <summary>
        /// Historial de ventas de la empresa.
        /// </summary>
        [JsonInclude]
        public List<Venta> Historial = new List<Venta>();

        /// <summary>
        /// Obtiene los datos para inciar en la empresa.
        /// </summary>
        /// <value><see cref ="DatosLogin"/></value>
        [JsonInclude]
        public DatosLogin DatosLogin { get; private set; }

        /// <summary>
        /// Constructor genérico de la clase Empresa.
        /// </summary>
        /// <param name="nombre"><see langword = "string"/>.</param>
        /// <param name="lugar"><see langword = "string"/>.</param>
        /// <param name="rubro"><see langword = "string"/>.</param>
        /// <param name="descripcion"><see langword = "string"/>.</param>
        /// <param name="contacto"><see langword = "string"/>.</param>
        public Empresa(string nombre, Location lugar, string rubro, string descripcion, string contacto)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Descripcion = descripcion;
            this.Contacto = contacto;
        }
        
        /// <summary>
        /// Contructor vacio para la creación de instancias temporales
        /// en el GestorInvitaciones.
        /// </summary>
        [JsonConstructor]
        public Empresa()
        {  
        }

        /// <summary>
        /// Método creador y publicador de una publicación.
        /// </summary>
        /// <param name="residuo"><see cref="Residuo"/>.</param>
        /// <param name="precioUnitario"><see langword="double"/>.</param>
        /// <param name="moneda"><see langword="string"/>.</param>
        /// <param name="cantidad"><see langword="int"/>.</param>
        /// <param name="lugarRetiro"><see langword="string"/>.</param>
        /// <param name="descripcion"><see langword="string"/>.</param>
        /// <param name="categoria"><see langword="string"/>.</param>
        public Publicacion CrearOferta(
            Residuo residuo,
            double precioUnitario,
            string moneda,
            int cantidad, 
            Location lugarRetiro,
            string descripcion,
            Categoria categoria)
        {
            return new Publicacion(residuo, precioUnitario, moneda, cantidad, lugarRetiro, this, descripcion, categoria);
        }

        /// <summary>
        /// Método creador y publicador de una publicación recurrente.
        /// </summary>
        /// <param name="residuo"><see cref="Residuo"/>.</param>
        /// <param name="precioUnitario"><see langword="double"/>.</param>
        /// <param name="moneda"><see langword="string"/>.</param>
        /// <param name="cantidad"><see langword="int"/>.</param>
        /// <param name="lugarRetiro"><see langword="string"/>.</param>
        /// <param name="descripcion"><see langword="string"/>.</param>
        /// <param name="categoria"><see langword="string"/>.</param>
        /// <param name="frecuenciaAnualRestock"><see langword="int"/>.</param>
        public PublicacionRecurrente CrearOfertaRecurrente(
            Residuo residuo,
            double precioUnitario,
            string moneda,
            int cantidad,
            Location lugarRetiro,
            string descripcion,
            Categoria categoria,
            int frecuenciaAnualRestock)
        {
            return new PublicacionRecurrente(residuo, precioUnitario, moneda, cantidad, lugarRetiro, this, frecuenciaAnualRestock, descripcion, categoria);
        }

        /// <summary>
        /// Metodo para guardar en JSon.
        /// </summary>
        /// <param name="exporter">JSonexporter.</param>
        public override void JsonSave(JsonExporter exporter)
        {
            exporter.Save(this);
        }
    }
}