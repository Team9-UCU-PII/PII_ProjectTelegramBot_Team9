namespace BotCore.Publication {
    /// <summary>
    /// Interfaz que unifica tipos cuyo objetico es devolver texto al bot para imprimir
    /// y retornar al usuario.
    /// </summary>
    public interface IPrintable {
        /// <summary>
        /// Metodo caracteristico de printable, arma el string con formato deseado.
        /// </summary>
        /// <returns>string</returns>
        string GetTextToPrint();
    }
}