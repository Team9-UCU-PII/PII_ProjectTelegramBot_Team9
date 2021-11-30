using System;
using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers
{
  public class HandlerReporte : MessageHandlerBase, IMessageHandler
  {
    public HandlerReporte(IMessageHandler next)
    : base((new string[] { "reporte" }), next)
    {
      this.Next = next;
    }

    protected override bool InternalHandle(IMessage message, out string response)
    {
      if ((CurrentForm as FrmReporte).CurrentState == faseReporte.Inicio)
      {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Vamos a crear un reporte\n");
        sb.Append($"Ingrese 'ok' para comenzar.\n ");
        response = sb.ToString();
        (CurrentForm as FrmReporte).CurrentState = faseReporte.AnioInicio;
        return true;
      }
      else if (message.TxtMensaje.ToLower() == "ok" && ((CurrentForm as FrmReporte).CurrentState == faseReporte.AnioInicio))
      {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Ingrese el año de inicio (yyyy)");
        response = sb.ToString();
        return true;
      }
      else if ((CurrentForm as FrmReporte).CurrentState == faseReporte.AnioInicio)
      {
        StringBuilder sb = new StringBuilder();
        if (int.TryParse(message.TxtMensaje, out int result))
        {
          (CurrentForm as FrmReporte).AnioInicio = result;
          sb.Append("Ingrese el mes de inicio (mm)");
          response = sb.ToString();
          (CurrentForm as FrmReporte).CurrentState = faseReporte.MesInicio;
          return true;
        }
        else
        {
          sb.Append($"Intenta de nuevo, asegurate ingresar en formato (yyyy)");
          response = sb.ToString();
          return true;
        }
      }
      else if ((CurrentForm as FrmReporte).CurrentState == faseReporte.MesInicio)
      {
        StringBuilder sb = new StringBuilder();
        if (int.TryParse(message.TxtMensaje, out int result))
        {
          (CurrentForm as FrmReporte).MesInicio = result;
          sb.Append("Ingrese el dia de inicio (dd)");
          response = sb.ToString();
          (CurrentForm as FrmReporte).CurrentState = faseReporte.DiaInicio;
          return true;
        }
        else
        {
          sb.Append($"Intenta de nuevo, asegurate ingresar en formato (mm)");
          response = sb.ToString();
          return true;
        }
      }
      else if ((CurrentForm as FrmReporte).CurrentState == faseReporte.DiaInicio)
      {
        StringBuilder sb = new StringBuilder();
        if (int.TryParse(message.TxtMensaje, out int result))
        {
          (CurrentForm as FrmReporte).DiaInicio = result;
          sb.Append("Ingrese el año de fin (yyyy)");
          response = sb.ToString();
          (CurrentForm as FrmReporte).CurrentState = faseReporte.AnioFin;
          return true;
        }
        else
        {
          sb.Append($"Intenta de nuevo, asegurate ingresar en formato (dd)");
          response = sb.ToString();
          return true;
        }
      }
      else if ((CurrentForm as FrmReporte).CurrentState == faseReporte.AnioFin)
      {
        StringBuilder sb = new StringBuilder();
        if (int.TryParse(message.TxtMensaje, out int result))
        {
          (CurrentForm as FrmReporte).AnioFin = result;
          sb.Append("Ingrese el mes de fin (mm)");
          response = sb.ToString();
          (CurrentForm as FrmReporte).CurrentState = faseReporte.MesFin;
          return true;
        }
        else
        {
          sb.Append($"Intenta de nuevo, asegurate ingresar en formato (yyyy)");
          response = sb.ToString();
          return true;
        }
      }
      else if ((CurrentForm as FrmReporte).CurrentState == faseReporte.MesFin)
      {
        StringBuilder sb = new StringBuilder();
        if (int.TryParse(message.TxtMensaje, out int result))
        {
          (CurrentForm as FrmReporte).MesFin = result;
          sb.Append("Ingrese el dia de fin (dd)");
          response = sb.ToString();
          (CurrentForm as FrmReporte).CurrentState = faseReporte.DiaFin;
          return true;
        }
        else
        {
          sb.Append($"Intenta de nuevo, asegurate ingresar en formato (mm)");
          response = sb.ToString();
          return true;
        }
      }
      else if ((CurrentForm as FrmReporte).CurrentState == faseReporte.DiaFin)
      {
        StringBuilder sb = new StringBuilder();
        if (int.TryParse(message.TxtMensaje, out int result))
        {
          (CurrentForm as FrmReporte).DiaFin = result;
          sb.Append("Listo!");
          response = sb.ToString();
          (CurrentForm as FrmReporte).CurrentState = faseReporte.Done;
          return true;
        }
        else
        {
          sb.Append($"Intenta de nuevo, asegurate ingresar en formato (dd)");
          response = sb.ToString();
          return true;
        }
      }
      else if ((CurrentForm as FrmReporte).reporteFinal != null && (CurrentForm as FrmReporte).CurrentState == faseReporte.Done)
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(((CurrentForm as FrmReporte).reporteFinal).GetTextToPrint());
        sb.AppendLine("Escribe /abortar para salir.");
        response = sb.ToString();
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }

    public enum faseReporte
    {
      Inicio,
      AnioInicio,
      MesInicio,
      DiaInicio,
      AnioFin,
      MesFin,
      DiaFin,
      Done
    }
  }
}