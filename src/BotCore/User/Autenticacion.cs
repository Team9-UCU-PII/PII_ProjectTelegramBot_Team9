//--------------------------------------------------------------------------------
// <copyright file="Autenticacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
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
/// <summary>
/// Metodo de acceso al singleton.
/// </summary>
/// <value></value>
        public static Autenticacion Instancia 
        {
            get
            {
                if(instancia == null)
                {
                    Autenticacion.instancia = new Autenticacion();
                }
                return Autenticacion.instancia;
            }
        }

        private Autenticacion()
        {
            
        }
/// <summary>
/// Metodo que toma colaboracion de <see cref = "DataAccess"/> para comprobar el inicio de 
/// sesión.
/// </summary>
/// <param name="nombreUsuario"></param>
/// <param name="contrasenia"></param>
/// <returns><c>true</c> si nombreUsuario y contrasenia están viculados en 
/// <see cref = "DatosLogin"/> en la base de datos.</returns>
        public static bool ValidarUsuario(string nombreUsuario, string contrasenia)
        {             
            foreach(DatosLogin datos in DataAccess.Instancia.Obtener<DatosLogin>())
            {
                if(datos.NombreUsuario == nombreUsuario)
                {
                    if(datos.Contrasenia == contrasenia)
                    {
                        Console.WriteLine("Acceso correcto");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Usuario o Constraseña incorrecto");
                        return false;
                    }
                }
            }

            Console.WriteLine("No se encontró el usuario");
            return false;
        }
    }
}