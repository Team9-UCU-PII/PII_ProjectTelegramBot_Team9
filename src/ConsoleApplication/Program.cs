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
    
    private static IHandler primerHandler;
    public static void Main()
    {
        primerHandler =
          new HolaHandler(
            new MenuHandler(

            )
          );
    }
  }
}