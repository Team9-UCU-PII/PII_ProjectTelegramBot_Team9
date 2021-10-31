namespace BotCore.User
{
    /// <summary>
    /// Interfaz que engloba tanto empresas como emprendimientos.
    /// </summary>
    public interface IUsuario
    {
        /// <summary>
        /// Los datos para el inicio de sesión.
        /// </summary>
        /// <value></value>
        DatosLogin DatosLogin { get;}
        /// <summary>
        /// El nombre de la empresa/emprendimiento.
        /// </summary>
        /// <value></value>
        string Nombre{get;set;}
    }
}