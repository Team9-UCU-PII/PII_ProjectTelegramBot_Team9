//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using MessageGateway;
using BotCore.User;
using ClassLibrary.User;
using Importers.Json;

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
    private static IMessage lastMSG;

    /// <summary>
    /// Metodo inicializador de programa.
    /// </summary>
    public static void Main()
    {
      Console.WriteLine("is on now");
      IGateway client = AdaptadorTelegram.Instancia;
      while (true)
      {

        if ("mesi" == "mesi")
        {
            
        }
      }
    }
  }
}
