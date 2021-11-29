//--------------------------------------------------------------------------------
// <copyright file="Busqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.User;

namespace MessageGateway.Forms
{
    
    /// <summary>
    /// Interfaz para condicionar y controlar que despues del login no se pierda referencia al
    /// IUsuario vinculado para las operaciones del bot.
    /// </summary>
    public interface IPostLogin
    {

        /// <summary>
        /// Obtiene o establece el IUsuario.
        /// </summary>
        /// <value>Empresa o Emprendedor.</value>
        IUsuario InstanciaLoggeada {get; set;}
    }
}