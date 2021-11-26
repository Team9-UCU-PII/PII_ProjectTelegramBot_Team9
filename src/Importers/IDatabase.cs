//--------------------------------------------------------------------------------
// <copyright file="IDatabase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.User;

namespace Importers
{
    /// <summary>
    /// Interaz de acceso a la base de datos.
    /// </summary>
    internal interface IDatabase
    {
        /// <summary>
        /// Obtiene instancia del singleton.
        /// </summary>
        /// <value><see iref = "IDatabase.Instancia"/>.</value>
        static IDatabase Instancia { get; }

        /// <summary>
        /// Guardar.
        /// </summary>
        /// <param name="objeto">Instancia a persistirse.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        void Insertar<T>(T objeto) where T : IPersistible;

        /// <summary>
        /// Sobreescribir.
        /// </summary>
        /// <param name="objetoOriginal">El objeto existente.</param>
        /// <param name="objetoModificado">El objeto nuevo.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        void Actualizar<T>(T objetoOriginal, T objetoModificado) where T : IPersistible;

        /// <summary>
        /// Leer.
        /// </summary>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        /// <returns><see langword="List T"/>.</returns>
        List<T> Obtener<T>() where T : class, IPersistible;

        /// <summary>
        /// Borrar.
        /// </summary>
        /// <param name="objeto">Instancia/s a borrarse.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        void Eliminar<T>(T objeto) where T : IPersistible;
    }
}