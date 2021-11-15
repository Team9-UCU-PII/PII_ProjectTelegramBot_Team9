//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using MessageGateway;

namespace ConsoleApplication
{
    /// <summary>
    /// Ejecucion inicial y principal del bot.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Metodo inicializador de programa.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("is on now");
            while (true)
            {
                IGateway client = AdaptadorTelegram.Instancia;
                IMessage lastMSG = client.MensajeRecibido;

                if (lastMSG.TxtMensaje == "mesi")
                {
                    client.EnviarMensaje(lastMSG.CrearRespuesta("dou"));
                }
            }
        }
    }
}