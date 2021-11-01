//--------------------------------------------------------------------------------
// <copyright file="Publicacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using BotCore.User;

namespace BotCore.Publication
{
  /// <summary>
  /// Tipo base de publicación, comprende Descripcion, Residuo, Precio, Moneda, Cantidad, Lugar de Retiro y la Empresa Vendedor.
  /// </summary>
  public class Publicacion
  {
    /// <summary>
    /// Constructor de Clase Publicacion.
    /// </summary>
    /// <param name="residuo"> Instancia tipo Residuo</param>
    /// <param name="precio"></param>
    /// <param name="moneda"></param>
    /// <param name="cantidad"></param>
    /// <param name="lugarRetiro">Debe implementar API de maps, por ahora string</param>
    /// <param name="vendedor">Instancia de IUsuario EMPRESA que vende lo publicado</param>
    /// <param name="descripcion"></param>
    public Publicacion(Residuo residuo, double precio, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion)
    {
      this.Residuo = residuo;
      this.Precio = precio;
      this.Moneda = moneda;
      this.Cantidad = cantidad;
      this.LugarRetiro = lugarRetiro;
      this.Vendedor = vendedor;
      this.Descripcion = descripcion;
      this.Comprado = false;
    }
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo Residuo</value>
    public Residuo Residuo {get; set;}
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo int</value>
    public double Precio {get; set;}
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo string</value>
    public string Moneda {get; set;}
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo int</value>
    public int Cantidad {get; set;}
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo Ubicacion (Aun no implementada API de maps)</value>
    public string LugarRetiro {get; set;}
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo IUsuario, instancia de Empresa</value>
    public Empresa Vendedor {get; set;}
/// <summary>
/// Property de Publicacion.
/// </summary>
/// <value>Tipo String</value>
    public string Descripcion{get; set;}
/// <summary>
/// Property de Publicacion que determina si la publicacion ya se compró.
/// </summary>
/// <value>Tipo bool</value>
    public bool Comprado{get; set;}
  }
}