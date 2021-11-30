using System;
using BotCore.Publication;
using ClassLibrary.Publication;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{

  /// <summary>
  /// Formulario que recopila la información para poder crear un reporte.
  /// </summary>
  public class FrmReporte : FormularioBase, IFormulario, IPostLogin
  {

    /// <summary>
    /// Constructor del formulario con su handler.
    /// </summary>
    public FrmReporte()
    {
      this.messageHandler =
      new HandlerReporte(null);
    }

    /// <summary>
    /// Año de inicio para pasar al DataTime para el reporte.
    /// </summary>
    /// <value>int</value>
    public int AnioInicio { get; set; }

    /// <summary>
    /// Mes de inicio para pasar al DataTime para el reporte.
    /// </summary>
    /// <value>int</value>
    public int MesInicio { get; set; }

    /// <summary>
    /// Día de inicio para pasar al DataTime para el reporte.
    /// </summary>
    /// <value>int</value>
    public int DiaInicio { get; set; }

    /// <summary>
    /// Año de fin para pasar al DataTime para el reporte.
    /// </summary>
    /// <value>int</value>
    public int AnioFin { get; set; }

    /// <summary>
    /// Mes de fin para pasar al DataTime para el reporte.
    /// </summary>
    /// <value>int</value>
    public int MesFin { get; set; }

    /// <summary>
    /// Día de fin para pasar al DataTime para el reporte.
    /// </summary>
    /// <value>int</value>
    public int DiaFin { get; set; }

    /// <summary>
    /// Para acceder a al usuario loggeado.
    /// </summary>
    /// <value></value>
    public IUsuario InstanciaLoggeada { get; set; }

    /// <summary>
    /// El reporte final creado.
    /// </summary>
    /// <value><see cref = "Reporte" />.</value>
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

    /// <summary>
    /// EL estado del formulario.
    /// </summary>
    /// <value></value>
    public HandlerReporte.faseReporte CurrentState { get; set; }
  }
}