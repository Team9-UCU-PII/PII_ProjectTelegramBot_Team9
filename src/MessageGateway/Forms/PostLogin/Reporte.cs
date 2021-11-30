using System;
using BotCore.Publication;
using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
  public class FrmReporte : FormularioBase, IFormulario, IPostLogin
  {
    public FrmReporte()
    {
      this.messageHandler =
      new HandlerReporte(null);
    }

    public int AnioInicio { get; set; }
    public int MesInicio { get; set; }
    public int DiaInicio { get; set; }
    public int AnioFin { get; set; }
    public int MesFin { get; set; }
    public int DiaFin { get; set; }

    public IUsuario InstanciaLoggeada { get; set; }

    public Reporte reporteFinal
    {
      get
      {
        if (AnioInicio != 0 && MesInicio != 0 && DiaInicio != 0 && AnioFin != 0 && MesFin != 0 && DiaFin != 0)
        {
          DateTime fechaFin = new DateTime(AnioFin, MesFin, DiaFin);
          DateTime fechaInicio = new DateTime(AnioInicio, MesInicio, DiaInicio);
          return Reporte.Generar(fechaInicio, fechaFin, this.InstanciaLoggeada);
        }
        else
        {
          return null;
        }
      }
    }

    public HandlerReporte.faseReporte CurrentState { get; set; }
  }
}