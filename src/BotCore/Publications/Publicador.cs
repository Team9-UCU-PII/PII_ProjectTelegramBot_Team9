//--------------------------------------------------------------------------------
// <copyright file="Publicador.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
//Patrones utilizados: Creator y SRP
//Se le delega la tarea de Crear publicaciones para mejorar el SRP de EMpresa, ademas de darle
//la capacidad de crear instancis de publicación (y recurrente).
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using ClassLibrary.User;
using Importers;

namespace BotCore.Publication
{
    /// <summary>
    /// Clase creadora de instancias y persistidora de publicación.
    /// </summary>
    public class Publicador
    {
        private DataAccess da; 

        private static Publicador instancia;

        /// <summary>
        /// Obtiene la instancia del Singleton.
        /// </summary>
        /// <value><see cref = "Publicador"/>.</value>
        public static Publicador Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Publicador();
                }
                return instancia;
            }
        }
        private Publicador()
        {
            this.da = DataAccess.Instancia;
        }

        /// <summary>
        /// Crea y persiste en memoria la publicación.
        /// </summary>
        /// <param name="residuo"><see cref = "Residuo"/>.</param>
        /// <param name="precioUnitario"><see langword = "double"/>.</param>
        /// <param name="moneda"><see langword = "string"/>.</param>
        /// <param name="cantidad"><see langword = "int"/>.</param>
        /// <param name="lugarRetiro"><see langword = "string"/>.</param>
        /// <param name="vendedor"><see cref = "Empresa"/>.</param>
        /// <param name="descripcion"><see langword = "string"/>.</param>
        public void PublicarOferta(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion)
        {
            da.Insertar(vendedor.CrearOferta(
                residuo,
                precioUnitario,
                moneda,
                cantidad,
                lugarRetiro,
                descripcion
            ));
        }

        /// <summary>
        /// Crea y persiste en memoria una nueva publicación recurrente.
        /// </summary>
        /// <param name="residuo"><see cref = "Residuo"/>.</param>
        /// <param name="precioUnitario"><see langword = "double"/>.</param>
        /// <param name="moneda"><see langword = "string"/>.</param>
        /// <param name="cantidad"><see langword = "int"/>.</param>
        /// <param name="lugarRetiro"><see langword = "string"/>.</param>
        /// <param name="vendedor"><see cref = "Empresa"/>.</param>
        /// <param name="descripcion"><see langword = "string"/>.</param>
        /// <param name="frecuenciaAnualRestock"><see langword = "int"/>.</param>
        public void PublicarOfertaRecurrente(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion, int frecuenciaAnualRestock)
        {
            da.Insertar(vendedor.CrearOfertaRecurrente(
                residuo,
                precioUnitario,
                moneda,
                cantidad,
                lugarRetiro,
                descripcion,
                frecuenciaAnualRestock
            ));
        }
    }
}