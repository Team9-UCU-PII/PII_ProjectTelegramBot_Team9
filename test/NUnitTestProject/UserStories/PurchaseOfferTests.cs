using NUnit.Framework;
using Importers;
using ClassLibrary.User;
using ClassLibrary.Publication;
using BotCore.Publication;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace Tests.UserStories
{
    [TestFixture]
    public class PurchaseOfferTests
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
        private Categoria categoria;
        private Residuo residuo;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this.da = DataAccess.Instancia;
            this.transacciones = Transacciones.Instancia;
            this.publicador = Publicador.Instancia;
            CatalogoHabilitaciones.ForEach(x => da.Insertar(x));
            this.empresa = new Empresa("SEMM", "Buceo", "Atención médica", "desc", "098123456");
            List<Habilitacion> habilitaciones = new List<Habilitacion> {
                this.CatalogoHabilitaciones[0],
                this.CatalogoHabilitaciones[1]
            };
            
            categoria = new Categoria("CAT");
            da.Insertar(categoria);
            residuo = new Residuo(categoria, "bla", "m/s", habilitaciones);
            da.Insertar(residuo);
        }

        [SetUp]
        public void Setup()
        {
            this.publicador.PublicarOferta(residuo, 100, "$", 5, "Obelisco", empresa, "desc oferta", categoria);

            this.ofertaPublicada = da.Obtener<Publicacion>().Single();
        }

        [TearDown]
        public void Teardown()
        {
            da.Obtener<Emprendedor>().ToList().ForEach(x => da.Eliminar(x));
            da.Obtener<Publicacion>().ToList().ForEach(x => da.Eliminar(x));
        }

        [OneTimeTearDown]
        public void OneTImeTeardown()
        {
            if (Directory.Exists("Data\\"))
            {
                Directory.Delete("Data\\", true);
            }
        }

        [Test]
        public void MakePurchase()
        {
            Emprendedor emprendedor = new Emprendedor(
                "Tapitas Oportunidades", "Centro", "Animales", "Procesamiento de plástico",
                new List<Habilitacion> {
                    CatalogoHabilitaciones[0],
                    CatalogoHabilitaciones[1],
                    CatalogoHabilitaciones[2]
                }
            );
            da.Insertar(emprendedor);

            Venta ventaAConcretar = new Venta(emprendedor, ofertaPublicada);
            this.transacciones.ConcretarVenta(ventaAConcretar);

            var publicacionComprada = da.Obtener<Publicacion>().Single();

            Assert.IsTrue(publicacionComprada.Comprado);

            var venta = da.Obtener<Venta>().Single();

            Assert.IsNotNull(venta.Fecha);
            Assert.IsTrue(venta.Comprador == emprendedor);
        }

        [Test]
        public void TryPurchasingBoughtOffer()
        {
            Emprendedor emprendedor = new Emprendedor(
                "Tapitas Oportunidades", "Centro", "Animales", "Procesamiento de plástico",
                new List<Habilitacion> {
                    CatalogoHabilitaciones[0],
                    CatalogoHabilitaciones[1],
                    CatalogoHabilitaciones[2]
                }
            );
            da.Insertar(emprendedor);

            Venta ventaAConcretar = new Venta(emprendedor, ofertaPublicada);
            this.transacciones.ConcretarVenta(ventaAConcretar);
            Assert.Throws(
                typeof(InvalidOperationException),
                () => this.transacciones.ConcretarVenta(ventaAConcretar)
            );
        }

        [Test]
        public void TryPurchasingWithIncorrectHabilitations()
        {
            Emprendedor emprendedor = new Emprendedor(
                "Tapitas Oportunidades", "Centro", "Animales", "Procesamiento de plástico",
                new List<Habilitacion> {
                    CatalogoHabilitaciones[0]
                }
            );
            da.Insertar(emprendedor);

            Venta ventaAConcretar = new Venta(emprendedor, ofertaPublicada);
            Assert.Throws(
                typeof(ArgumentException),
                () => this.transacciones.ConcretarVenta(ventaAConcretar)
            );
        }
    }
}