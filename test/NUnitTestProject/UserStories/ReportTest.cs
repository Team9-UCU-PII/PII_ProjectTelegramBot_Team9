using NUnit.Framework;
using Importers;
using ClassLibrary.User;
using ClassLibrary.Publication;
using BotCore.Publication;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests.UserStories
{
    [TestFixture]
    public class ReportTest
    {
        private DataAccess da;
        private Empresa empresa;
        private Publicador publicador;
        private Transacciones transacciones;
        private List<Habilitacion> CatalogoHabilitaciones = new List<Habilitacion> {
            new Habilitacion("Hab1"),
            new Habilitacion("Hab2"),
            new Habilitacion("Hab3"),
            new Habilitacion("Hab4")
        };
        private Publicacion ofertaPublicada;

        [SetUp]
        public void Setup()
        {
            this.da = DataAccess.Instancia;
            this.transacciones = Transacciones.Instancia;
            this.publicador = Publicador.Instancia;
            this.empresa = new Empresa("SEMM", "Buceo", "Atención médica", "desc", "098123456");
            List<Habilitacion> habilitaciones = new List<Habilitacion> {
                this.CatalogoHabilitaciones[0],
                this.CatalogoHabilitaciones[1]
            };
            Categoria categoria = new Categoria("CAT");
            Residuo residuo = new Residuo(categoria, "bla", "m/s", habilitaciones);
            this.publicador.PublicarOferta(residuo, 100, "$", 5, "Obelisco", empresa, "desc oferta", categoria);

            this.ofertaPublicada = da.Obtener<Publicacion>().Single();
        }

        [Test]
        public void MakeReport()
        {
            Emprendedor emprendedor = new Emprendedor(
                "Tapitas Oportunidades", "Centro", "Animales", "Procesamiento de plástico",
                new List<Habilitacion> {
                    CatalogoHabilitaciones[0],
                    CatalogoHabilitaciones[1],
                    CatalogoHabilitaciones[2]
                }
            );

            Venta ventaAConcretar = new Venta(emprendedor, ofertaPublicada);
            this.transacciones.ConcretarVenta(ventaAConcretar);

            Reporte reporte = Reporte.Generar(new DateTime(2021, 11, 3), new DateTime(2021, 11, 5), emprendedor);

            Assert.IsTrue(reporte.FechaInicio == new DateTime(2021, 11, 3) && reporte.FechaFin == new DateTime(2021, 11, 5) && reporte.Usuario == emprendedor);            
        }
    }
}