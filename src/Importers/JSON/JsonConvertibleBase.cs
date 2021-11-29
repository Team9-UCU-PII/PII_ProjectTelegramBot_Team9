namespace Importers.Json
{
    /// <summary>
    /// Clase base abstracta que redefine los operadores == y != para determinar si dos objetos son iguales
    /// en base a su ID de serialización.
    /// </summary>
    public abstract class JsonConvertibleBase : IJsonConvertible
    {
        /// <summary>
        /// ID utilizado para determinar si dos objetos son iguales.
        /// </summary>
        /// <value>El ID que identifica a este objeto serializado entre los de su tipo.</value>
        public int SerializationID { get; set; }

        /// <summary>
        /// Método que realiza la persistencia de un IJsonConvertible en un archivo.
        /// </summary>
        /// <param name="exporter">Un JsonExporter para realizar la serialización y exportación.</param>
        public abstract void JsonSave(JsonExporter exporter);

        /// <summary>
        /// Indica si dos objetos JsonConvertibleBase se pueden considerar iguales en base a su ID de serialización.
        /// </summary>
        /// <param name="a">El primer objeto.</param>
        /// <param name="b">El segundo objeto.</param>
        /// <returns>true si ambos objetos tienen el mismo ID de serialización, false en caso contrario.</returns>
        public static bool operator ==(JsonConvertibleBase a, JsonConvertibleBase b)
        {
            if ((object) a == null && (object) b == null)
            {
                return true;
            }

            if ((object) a == null ^ (object) b == null)
            {
                return false;
            }

            return a.SerializationID == b.SerializationID;
        }

        /// <summary>
        /// Indica si dos objetos JsonConvertibleBase se pueden considerar distintos en base a su ID de serialización.
        /// </summary>
        /// <param name="a">El primer objeto.</param>
        /// <param name="b">El segundo objeto.</param>
        /// <returns>true si los objetos tienen distinto ID de serialización, false en caso contrario.</returns>
        public static bool operator !=(JsonConvertibleBase a, JsonConvertibleBase b)
        {
            return !(a == b);
        }
    }
}