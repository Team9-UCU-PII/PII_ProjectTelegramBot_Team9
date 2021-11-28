//--------------------------------------------------------------------------------
// <copyright file="IResiduoForm.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Handlers;
using System.Collections.Generic;

namespace MessageGateway.Forms
{

    /// <summary>
    /// Interfaz que engloba los formularios que necesitan de un residuo.
    /// </summary>
    public interface IResiduoForm
    {

        /// <summary>
        /// La instancia del residuo que se crea a partir de las siguientes propiedades.
        /// </summary>
        /// <value>Residuo.</value>
        Residuo residuo { get; }

        /// <summary>
        /// Descripción del residuo.
        /// </summary>
        /// <value>String.</value>
        string descripcion { get; set; }

        /// <summary>
        /// Unidad de Medida (Lts, Kg, etc...).
        /// </summary>
        /// <value>String.</value>
        string unit { get; set; }

        /// <summary>
        /// Categoria del residuo.
        /// </summary>
        /// <value>Categoria.</value>
        Categoria categoria { get; set; }

        /// <summary>
        /// Lista de habilitaciones necesarias para obtener el residuo.
        /// </summary>
        /// <value>List de Habilitacion</value>
        List<Habilitacion> habilitaciones { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la construcción del resiudo.
        /// </summary>
        /// <value></value>
        HandlerNewResiduo.fasesResiduo CurrentStateResiduo { get; set; }
    }
}