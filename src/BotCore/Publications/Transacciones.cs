//--------------------------------------------------------------------------------
// <copyright file="Autenticacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patron utilizado: Singleton.
// Esta clase utiliza este patrón porque solo se necesita una instancia y almacena un estado.
//--------------------------------------------------------------------------------

using System;
using System.Linq;
using ClassLibrary.Publication;
using Importers;

namespace BotCore.Publication
{
    /// <summary>
    /// Clase encargada de persistir y confirmar las compras una vez hechas.
    /// </summary>
    public class Transacciones
    {
        private DataAccess da;

        private static Transacciones instancia;

        /// <summary>
        /// Obtiene la instancia del Singleton.
        /// </summary>
        /// <value><see cref = "Transacciones"/>.</value>
        public static Transacciones Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Transacciones();
                }
                return instancia;
            }
        }
        private Transacciones()
        {
            this.da = DataAccess.Instancia;
        }

        /// <summary>
        /// Confirma la compra y la persiste.
        /// </summary>
        /// <param name="venta"><see cref = "Venta"/>.</param>
        public void ConcretarVenta(Venta venta)
        {
            if (! venta.Publicacion.Residuo.Habilitaciones.All(x => venta.Comprador.Habilitaciones.Contains(x)))
            {
                throw new ArgumentException("El emprendedor no posee las habilitaciones necesarias para manejar el residuo de esta publicación.");
            }

            if (venta.Publicacion.Comprado)
            {
                throw new InvalidOperationException("Esta publicación ya fue comprada.");
            }

            venta.Publicacion.Comprado = true;
            venta.Fecha = DateTime.Now;
            da.Insertar(venta);
        }
    }
}