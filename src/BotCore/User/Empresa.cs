using System.Collections.Generic;
using BotCore.Publication;

namespace BotCore.User
{
    public class Empresa: IUsuario
    {
        public string Nombre{ get; set;}

        public string Lugar;

        public string Rubro;

        public string Descripcion;

        public string Contacto;

        public string[] PalabrasClave;

        public List<Venta> Historial = new List<Venta>();

        public DatosLogin DatosLogin { get; private set; }

        public Empresa(string nombre, string lugar, string rubro, string descripcion, string contacto)
        {
            this.Nombre = nombre;
            this.Lugar = lugar;
            this.Rubro = rubro;
            this.Descripcion = descripcion;
            this.Contacto = contacto;
        }

        public Empresa()
        {
            
        }

        public void PublicarOferta(
            Residuo residuo, double precioUnitario, string moneda, int cantidad, 
            string lugarRetiro, string descripcion
        ) {
            Publicacion p = new Publicacion(residuo, precioUnitario, moneda, cantidad, lugarRetiro, this, descripcion);
        }

        public void PublicarOfertaRecurrente(
            Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro,
            string descripcion, int frecuenciaAnualRestock
        ) {
            Publicacion p = new PublicacionRecurrente(residuo, precioUnitario, moneda, cantidad,
            lugarRetiro, this, frecuenciaAnualRestock, descripcion);
        }
    }
}