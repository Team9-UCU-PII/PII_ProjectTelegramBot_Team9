//--------------------------------------------------------------------------------
// <copyright file="Autenticacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patron utilizado: Singleton.
// Esta clase utiliza este patrón porque solo se necesita una instancia y almacena un estado.
//--------------------------------------------------------------------------------

using System;
using ClassLibrary.User;
using Importers;

namespace BotCore.User
{
    /// <summary>
    /// Esta clase se encarga de tomar datos para loggear una persona y verificar
    /// su informacion a la hora de iniciar sesión.
    /// </summary>
    public class Autenticacion
    {
        private static Autenticacion instancia;

        private Autenticacion()
        {
        }

/// <summary>
/// Metodo de acceso al singleton.
/// </summary>
/// <value></value>
        public static Autenticacion Instancia
        {
            get
            {
                if (instancia == null)
                {
                    Autenticacion.instancia = new Autenticacion();
                }

                return Autenticacion.instancia;
            }
        }

/// <summary>
/// Metodo que toma colaboracion de <see cref = "DataAccess"/> para comprobar el inicio de
/// sesión.
/// </summary>
/// <param name="nombreUsuario">String de username.</param>
/// <param name="contrasenia">String de password.</param>
/// <param name="organizacion">IUsuario saliente.</param>
/// <returns><c>true</c> si nombreUsuario y contrasenia están viculados en
/// <see cref = "DatosLogin"/> en la base de datos.</returns>
        public static bool ValidarUsuario(string nombreUsuario, string contrasenia, out IUsuario organizacion)
        {
            organizacion = null;
            foreach (DatosLogin datos in DataAccess.Instancia.Obtener<DatosLogin>())
            {
                if (datos.NombreUsuario == nombreUsuario && datos.Contrasenia == contrasenia)
                {
                    organizacion = datos.Usuario;
                    Console.WriteLine("Acceso correcto");
                    return true;
                }
            }

            Console.WriteLine("Usuario y/o contraseña incorrectos.");
            return false;
        }
    }
}