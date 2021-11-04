using NUnit.Framework;
using Importers;
using ClassLibrary.Publication;
using ClassLibrary.User;
using BotCore.Publication;
using System.Collections.Generic;
using System.Linq;

namespace Tests.UserStories
{
    public class SearchOffersTests
    {
        private DataAccess da;
        private Publicador publicador;
        private Busqueda buscador;
        
        private List<Categoria> CatalogoCategorias;
        private List<Habilitacion> CatalogoHabilitaciones;
        private List<Residuo> CatalogoResiduos;
        private Empresa Empresa;

        [SetUp]
        public void Setup()
        {
            this.da = DataAccess.Instancia;
            this.publicador = Publicador.Instancia;
            this.buscador = Busqueda.Instancia;

            this.CatalogoCategorias = new List<Categoria> {
                new Categoria("Madera"),
                new Categoria("Metal"),
                new Categoria("Plástico"),
                new Categoria("Goma")
            };

            this.CatalogoHabilitaciones = new List<Habilitacion> {
                new Habilitacion("H1"),
                new Habilitacion("H2"),
                new Habilitacion("D1"),
                new Habilitacion("Q1")
            };

            this.CatalogoResiduos = new List<Residuo> {
                new Residuo(CatalogoCategorias[0], "Planchas MDF", "m2",
                new List<Habilitacion> {CatalogoHabilitaciones[1], CatalogoHabilitaciones[3]}),
                new Residuo(CatalogoCategorias[1], "Aluminio", "kg",
                new List<Habilitacion> {CatalogoHabilitaciones[0]}),
                new Residuo(CatalogoCategorias[2], "Tubos de PVC", "kg",
                new List<Habilitacion> {CatalogoHabilitaciones[3]})
            };

            this.Empresa = new Empresa("CUTCSA", "Punta Carretas", "Transporte", "Empresa de transporte", "099123456");

            this.publicador.PublicarOferta(CatalogoResiduos[2], 60, "U$S", 150, "Malvín Norte", Empresa, "Lote de tubos PVC");
            this.publicador.PublicarOfertaRecurrente(CatalogoResiduos[1], 35, "U$S", 20, "Aguada", Empresa, "Repuestos hechos chatarra, mayormente aluminio.", 3);
            this.publicador.PublicarOferta(CatalogoResiduos[0], 65, "$", 590, "Punta de Rieles", Empresa, "Planchas de MDF en buen estado.");
        }

        [Test]
        public void SearchByZone()
        {
            string lugarRetiro = "Punta de Rieles";

            Dictionary<Busqueda.FiltrosPosibles, object> filtro = new Dictionary<Busqueda.FiltrosPosibles, object> {
                {Busqueda.FiltrosPosibles.LugarRetiro, lugarRetiro}
            };

            List<Publicacion> expected = da.Obtener<Publicacion>().Concat(da.Obtener<PublicacionRecurrente>()).Where(x => x.LugarRetiro == lugarRetiro).ToList();
            List<Publicacion> actual = buscador.BuscarPublicaciones(filtro);

            IEnumerable<Publicacion> union = expected.Union(actual);

            Assert.IsTrue(actual.Distinct().Count() == union.Count() && actual.All(x => x.LugarRetiro == lugarRetiro));
        }

        [Test]
        public void SearchByCategory()
        {
            Categoria categoria = CatalogoCategorias[0];
            
            Dictionary<Busqueda.FiltrosPosibles, object> filtro = new Dictionary<Busqueda.FiltrosPosibles, object> {
                {Busqueda.FiltrosPosibles.Categorias, categoria}
            };

            List<Publicacion> expected = da.Obtener<Publicacion>().Concat(da.Obtener<PublicacionRecurrente>()).Where(x => x.Residuo.Categoria == categoria).ToList();
            List<Publicacion> actual = buscador.BuscarPublicaciones(filtro);

            IEnumerable<Publicacion> union = expected.Union(actual);

            Assert.IsTrue(actual.Distinct().Count() == union.Count() && actual.All(x => x.Residuo.Categoria == categoria));
        }

        [Test]
        public void SearchOneTimePublications()
        {
            int frecuenciaAnualRestock = 0;
            Dictionary<Busqueda.FiltrosPosibles, object> filtro = new Dictionary<Busqueda.FiltrosPosibles, object> {
                {Busqueda.FiltrosPosibles.FrecuenciaRestock, frecuenciaAnualRestock}
            };

            List<Publicacion> expected = da.Obtener<Publicacion>()
                                        .Concat(da.Obtener<PublicacionRecurrente>())
                                        .Where(x => x is not PublicacionRecurrente).ToList();
            List<Publicacion> actual = buscador.BuscarPublicaciones(filtro);

            IEnumerable<Publicacion> union = expected.Union(actual);

            Assert.IsTrue(actual.Distinct().Count() == union.Count() &&
                            actual.All(x => x is not PublicacionRecurrente));
        }

        [Test]
        public void SearchRecurrentPublications()
        {            
            int frecuenciaAnualRestock = 3;
            Dictionary<Busqueda.FiltrosPosibles, object> filtro = new Dictionary<Busqueda.FiltrosPosibles, object> {
                {Busqueda.FiltrosPosibles.FrecuenciaRestock, frecuenciaAnualRestock}
            };

            List<Publicacion> expected = da.Obtener<Publicacion>()
                                        .Concat(da.Obtener<PublicacionRecurrente>())
                                        .Where(x => x is PublicacionRecurrente &&
                                        (x as PublicacionRecurrente).FrecuenciaAnualRestock == frecuenciaAnualRestock).ToList();
            List<Publicacion> actual = buscador.BuscarPublicaciones(filtro);

            IEnumerable<Publicacion> union = expected.Union(actual);

            Assert.IsTrue(actual.Distinct().Count() == union.Count() &&
                            actual.All(x => x is PublicacionRecurrente && (x as PublicacionRecurrente).FrecuenciaAnualRestock == frecuenciaAnualRestock));
        }
    }
}