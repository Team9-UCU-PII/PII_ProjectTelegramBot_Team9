using System;
using Importers;

namespace BotCore.User
{
    public class Autenticacion
    {
        private static Autenticacion instancia;

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