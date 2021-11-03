//--------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Importers
{

    /// <summary>
    /// Clase que utilizará el bot para acceder a la base de datos.
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Obtiene acceso al singleton.
        /// </summary>
        /// <value><see cref = "DataAccess"/>.</value>
        public static DataAccess Instancia
        {
            get
            {
                if (DataAccess.instancia == null)
                {
                    DataAccess.instancia = new DataAccess(DatabaseMemoria.Instancia);
                }

                return DataAccess.instancia;
            }
        }

        private static DataAccess instancia;

        private DataAccess(IDatabase db)
        {
            this.db = db;
        }

        private IDatabase db;

        /// <summary>
        /// Almacena una nueva instancia en la base de datos.
        /// </summary>
        /// <param name="objeto">Instancia sin persistir.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        public void Insertar<T>(T objeto)
        {
            this.db.Insertar(objeto);
        }

        /// <summary>
        /// Update a un objeto ya existente en la base de datos.
        /// </summary>
        /// <param name="objetoOriginal">El objeto existente.</param>
        /// <param name="objetoModificado">El objeto nuevo.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        public void Actualizar<T>(T objetoOriginal, T objetoModificado)
        {
            this.db.Actualizar(objetoOriginal, objetoModificado);
        }

        /// <summary>
        /// Recupera instancia/s desde la base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de la instancia/s.</typeparam>
        /// <returns><see langword="List T"/>.</returns>
        public List<T> Obtener<T>()
        {
            return this.db.Obtener<T>();
        }

        /// <summary>
        /// Borra elementos de la base de datos.
        /// </summary>
        /// <param name="objeto">Instancia a borrarse.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        public void Eliminar<T>(T objeto)
        {
            this.db.Eliminar(objeto);
        }
    }
}