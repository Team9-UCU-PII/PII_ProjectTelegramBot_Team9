//--------------------------------------------------------------------------------
// <copyright file="IUsuario.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
// Patrón utilizado: Low coupling.
// Ayuda a mantener el acoplamiento bajo en tareas relacionadas con Empresa y Emprendedores.
//--------------------------------------------------------------------------------

using Importers;
using Importers.Json;

namespace ClassLibrary.User
{
    /// <summary>
    /// Interfaz que engloba tanto empresas como emprendimientos.
    /// </summary>
    public interface IUsuario : IJsonConvertible
    {
        /// <summary>
        /// Obtiene los datos para el inicio de sesión.
        /// </summary>
        /// <value><see cref ="DatosLogin"/>.</value>
        DatosLogin DatosLogin { get; }

        /// <summary>
        /// Obtiene o establece el nombre de la empresa/emprendimiento.
        /// </summary>
        /// <value><see langword="string"/>.</value>
        string Nombre { get; set; }
    }
}