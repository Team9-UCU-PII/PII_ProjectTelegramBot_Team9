//--------------------------------------------------------------------------------
// <copyright file="IPrintable.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace ClassLibrary.Publication 
{
    /// <summary>
    /// Interfaz que unifica tipos cuyo objetivo es devolver texto al bot para imprimir
    /// y retornar al usuario. Se considera una aplicación de patrón Expert ya que es una
    /// clase que implementa IPrintable aquella que posee toda la información necesaria para
    /// retornar una representación suya como cadena de texto. Si no aplicásemos este patrón,
    /// cada Handler necesitaría armar por su cuenta las cadenas con los atributos de cada clase,
    /// lo cual no solo es más propenso a errores e inconsistencias de formato sino que también
    /// puede violar la Ley de Demeter al generar mucho más acoplamiento del necesario entre las clases.
    /// </summary>
    public interface IPrintable 
    {
        /// <summary>
        /// Metodo caracteristico de printable, arma el string con formato deseado.
        /// </summary>
        /// <returns><see langword="string"/>.</returns>
        string GetTextToPrint();
    }
}