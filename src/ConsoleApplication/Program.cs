//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using MessageGateway;
using BotCore.User;
using ClassLibrary.User;
using ClassLibrary.LocationAPI;

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
      while (true)
      {
        IGateway client = AdaptadorTelegram.Instancia;
        IMessage lastMSG = client.MensajeRecibido;

        var location = LocationApiClient.Instancia.GetLocation("Isla de Gorriti 2056");


        if (lastMSG.TxtMensaje == "mesi")
        {
            Console.WriteLine("is on now");
            GestorInvitaciones.Instancia.AlmacenarInvitacion<Empresa>("SEMM");
            IGateway client = AdaptadorTelegram.Instancia;
            client.Start();
            while (true)
            {
                /*
                var location = LocationApiClient.Instancia.GetLocation("Isla de Gorriti 2056");

                
                    if (lastMSG.TxtMensaje == "mesi")
                    {
                        client.EnviarUbicacionEnMapa(lastMSG, (float)location.Latitude, (float)location.Longitude);
                    }
                */
            }
        }
      }
    }
  }

  /*primerHandler =
    new HolaHandler(
      new MenuHandler(

      )
    );*/
}
