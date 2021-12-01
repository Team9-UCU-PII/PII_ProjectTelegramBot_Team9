//--------------------------------------------------------------------------------
// <copyright file="IDatabase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Importers
{
    /// <summary>
    /// Interfaz de acceso a la base de datos. Definimos esta interfaz guiándonos por el patrón Adapter
    /// y el principio de inversión de dependencias, de forma que la capa de Data Access (para la base
    /// de datos el código cliente) no depende de las implementaciones concretas de una base de datos,
    /// sino de una abstracción. De esta forma, los cambios en la implementación interna de cada mecanismo
    /// de persistencia son imperceptibles para el código que envía los mensajes, ya que este se comunica
    /// a través de la interfaz.
    /// Además, es un buen ejemplo del principio OCP, ya que es posible extender el programa agregando
    /// nuevas opciones de persistencia (una base de datos propiamente dicha, por ejemplo), pero no es
    /// posible modificar las ya existentes.
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