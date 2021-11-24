using ClassLibrary.User;
using ClassLibrary.Publication;
using Importers;
using NUnit.Framework;
using BotCore.Publication;
using System.Collections.Generic;
using System.Linq;

namespace Tests.UserStories
{
    [TestFixture]
    public class PublishOfferTests
    {
        Empresa empresa;
        DataAccess da;
        Publicador p;

        [SetUp]
        public void Setup()
        {
            da = DataAccess.Instancia;
            empresa = new Empresa("SEMM", "Punta de Rieles", "Atención médica", "Desc", "098123456");
            da.Insertar(empresa);
            p = Publicador.Instancia;
        }

        [TearDown]
        public void Teardown()
        {
            da.Obtener<Empresa>().ToList().ForEach(x => da.Eliminar(x));
            da.Obtener<Publicacion>().ToList().ForEach(x => da.Eliminar(x));
        }

        [Test]
        public void PublishOffer()
        {
            List<Habilitacion> habilitaciones = new List<Habilitacion> {new Habilitacion("Hab1"), new Habilitacion("Hab2")};
            Categoria categoria = new Categoria("CAT");
            Residuo residuo = new Residuo(categoria, "bla", "m/s", habilitaciones);

            p.PublicarOferta(residuo, 15.99, "$", 50, "Puerto del Buceo", empresa, "Descrip", categoria);

            var resultado = from Publicacion p in da.Obtener<Publicacion>()
                            where p.Residuo == residuo && p.Vendedor == empresa
                            select p;

            Assert.IsTrue(resultado.Count() == 1);
        }

        [Test]
        public void PublishRecurrentOffer()
        {
            List<Habilitacion> habilitaciones = new List<Habilitacion> {new Habilitacion("Hab1"), new Habilitacion("Hab2")};
            Categoria categoria = new Categoria("CAT");
            Residuo residuo = new Residuo(categoria, "bla", "m/s", habilitaciones);

            p.PublicarOfertaRecurrente(residuo, 9.99, "U$S", 15, "Bella Unión", empresa, "Desc", categoria, 4);

            var resultado = from PublicacionRecurrente p in da.Obtener<PublicacionRecurrente>()
                            where p.Residuo == residuo && p.Vendedor == empresa
                            select p;

            Assert.IsTrue(resultado.Count() == 1);
        }
    }
}