//--------------------------------------------------------------------------------
// <copyright file="ListadoPublicacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using BotCore.Publication;
using System.Collections.Generic;
using ClassLibrary.Publication;
using MessageGateway.Forms;
using ClassLibrary.User;
using System;

namespace MessageGateway.Handlers.ListadoPublicaciones
{

  /// <summary>
  /// Handler encargado de mostrar publicaciones según los filtros aplicados por el usuario.
  /// </summary>
  public class HandlerListadoPublicaciones : MessageHandlerBase, IMessageHandler
  {

    /// <summary>
    /// Constructor, no recibe palabra clave.
    /// </summary>
    /// <param name="next"></param>
    public HandlerListadoPublicaciones(IMessageHandler next)
    : base((new string[] {"menu"}), next)
    {
      this.Next = next;
    }

    /// <summary>
    /// InternalHandle, muestra las publicaciones filtradas y los detalles de una publicación según el nombre.
    /// Devuelve un mapa con la ubicación o permite realizar la compra según la opción elegida.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (((CurrentForm as IListableForm).CurrentStateListado == fasesListado.Inicio)
      || (this.CanHandle(message) && ((CurrentForm as IListableForm).CurrentStateListado == fasesListado.EligiendoDetalles)))
      {
        StringBuilder sb = new StringBuilder();
        foreach (Publicacion publicacion in (CurrentForm as IListableForm).publicacionesFiltradas)
        {
          sb.AppendJoin('\n',
          $"Residuo: {publicacion.Residuo.Descripcion}\n",
          $"Vendedor: {publicacion.Vendedor.Nombre}\n",
          $"Precio: {publicacion.PrecioTotal}\n",
          "----------------------------------------------"
          );
        }
        sb.Append("Para ver detalles escribí el nombre del residuo que quieras ver");
        response = sb.ToString();
        (CurrentForm as IListableForm).CurrentStateListado = fasesListado.EligiendoDetalles;
        return true;
      }
      else if ((CurrentForm as IListableForm).CurrentStateListado == fasesListado.EligiendoDetalles)
      {
        StringBuilder sb = new StringBuilder();
        foreach (Publicacion publicacion in (CurrentForm as IListableForm).publicacionesFiltradas)
        {
          if (message.TxtMensaje.ToLower() == publicacion.Residuo.Descripcion.ToLower())
          {
            sb.AppendJoin('\n',
            $"Detalles de la publicación: {publicacion.Residuo.Descripcion}\n",
            $"{publicacion.GetTextToPrint()}\n",
            "-----------------------------------",
            "Marque la opción que desee: \n",
            "1 - Ver ubicación\n",
            "2 - Comprar"
          );
          (CurrentForm as IListableForm).publicacionSeparada = publicacion;
          response = sb.ToString();
          (CurrentForm as IListableForm).CurrentStateListado = fasesListado.VerUbicacion;
          return true;
          }
        }
        sb.Append("No se encontró ninguna publicación con ese nombre.");
        response = sb.ToString();
        return true;
      }
      else if ((CurrentForm as IListableForm).CurrentStateListado == fasesListado.VerUbicacion)
      {
        Publicacion publi = (CurrentForm as IListableForm).publicacionSeparada;
        Transacciones comprador = Transacciones.Instancia;
        if (message.TxtMensaje == "1")
        {
          (CurrentForm as FormularioBase).gateway.EnviarUbicacionEnMapa(message, publi.LugarRetiro.Latitude, publi.LugarRetiro.Longitude);
          response = "";
          return true;
        }
        if (message.TxtMensaje == "2")
        {
          Venta venta = new Venta(((CurrentForm as IPostLogin).InstanciaLoggeada as Emprendedor), publi);
          try
          {
            comprador.ConcretarVenta(venta);
          }
          catch(ArgumentException)
          {
            response = "No tenes las habilitaciones necesarias";
            return true;
          }
          catch(InvalidOperationException)
          {
            response = "Esta publicación ya fue comprada";
            return true;
          }
          response = "Comprado";
          return true;
        }
        response = "Si deseas salir escribe /abortar";
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }

    /// <summary>
    /// Fases del listado de publicaciones.
    /// </summary>
    public enum fasesListado
    {


      ///Primera fase.
      Inicio,

      ///Elegir la publicacion de la que quiero ver detalles.
      EligiendoDetalles,

      ///Mandar mapa con la ubicación.
      VerUbicacion,

      ///Final.
      Done
    }
  }
}