//--------------------------------------------------------------------------------
// <copyright file="IDatabase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Importers 
{
    /// <summary>
    /// Interaz de acceso a la base de datos.
    /// </summary>
    public interface IDatabase 
    {
        /// <summary>
        /// Retorno del singleton.
        /// </summary>
        /// <value></value>
        static IDatabase Instancia {get;}
        /// <summary>
        /// Guardar.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        void Insertar<T>(T objeto);
        /// <summary>
        /// Sobreescribir.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        void Actualizar<T>(T objetoOriginal, T objetoModificado);
        /// <summary>
        /// Leer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List T</returns>
        List<T> Obtener<T>();
        /// <summary>
        /// Borrar.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        void Eliminar<T>(T objeto);
    }
}