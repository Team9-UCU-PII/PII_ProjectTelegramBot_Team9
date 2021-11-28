//--------------------------------------------------------------------------------
// <copyright file="Habilitacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using Importers;
using Importers.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary.User
{
  /// <summary>
  /// Clase representativa de las habilitaciones existentes aplicables a los residuos y emprendedores.
  /// </summary>
  public class Habilitacion : JsonConvertibleBase
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
    /// Cosntructor de Json.
    /// </summary>
    [JsonConstructor]
    public Habilitacion()
    {

    }

    /// <summary>
    /// Obtiene o establece el nombre de la habilitación.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Nombre { get; set; }
    
    /// <summary>
    /// Metodo para guardar en Json.
    /// </summary>
    /// <param name="exporter"></param>
    public override void JsonSave(JsonExporter exporter)
    {
        exporter.Save(this);
    }
  }
}