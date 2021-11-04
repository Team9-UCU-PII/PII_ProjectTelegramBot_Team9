//--------------------------------------------------------------------------------
// <copyright file="Autenticacion.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patron utilizado: Singleton
// Esta clase utiliza este patrón porque solo se necesita una instancia y almacena un estado.
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using ClassLibrary.User;
using Importers;

namespace BotCore.Publication
{
    public class Publicador
    {
        private DataAccess da; 

        private static Publicador instancia;
        public static Publicador Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Publicador();
                }
                return instancia;
            }
        }
        private Publicador()
        {
            this.da = DataAccess.Instancia;
        }

        public void PublicarOferta(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion)
        {
            da.Insertar(vendedor.CrearOferta(
                residuo,
                precioUnitario,
                moneda,
                cantidad,
                lugarRetiro,
                descripcion
            ));
        }

        public void PublicarOfertaRecurrente(Residuo residuo, double precioUnitario, string moneda, int cantidad, string lugarRetiro, Empresa vendedor, string descripcion, int frecuenciaAnualRestock)
        {
            da.Insertar(vendedor.CrearOfertaRecurrente(
                residuo,
                precioUnitario,
                moneda,
                cantidad,
                lugarRetiro,
                descripcion,
                frecuenciaAnualRestock
            ));
        }
    }
}