//--------------------------------------------------------------------------------
// <copyright file="Habilitacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace ClassLibrary.User
{
  /// <summary>
  /// Clase representativa de las habilitaciones existentes aplicables a los residuos y emprendedores.
  /// </summary>
  public class Habilitacion
  {
    /// <summary>
    /// Contructor del tipo Habilitación.
    /// </summary>
    /// <param name="nombre"><see langword="string"/>.</param>
    public Habilitacion(string nombre)
    {
      this.Nombre = nombre;
    }

    /// <summary>
    /// Nombre de la habilitación.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Nombre{ get; set; }
  }
}