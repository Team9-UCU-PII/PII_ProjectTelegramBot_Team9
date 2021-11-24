using NUnit.Framework;
using ClassLibrary.User;
using BotCore.User;
using Importers;
using Tests.TestClasses;
using System.Linq;
using System.Collections.Generic;
using MessageGateway;

namespace Tests.UserStories
{
    [TestFixture]
    public class AcceptInviteTests
    {
        private GestorInvitaciones gi;
        private DataAccess da;
        private IGateway gateway;

        [SetUp]
        public void Setup()
        {
            gi = GestorInvitaciones.Instancia;
            gateway = TestGateway.Instancia;
            da = DataAccess.Instancia;
        }

        [TearDown]
        public void Teardown()
        {
            gi.InvitacionesEnviadas.RemoveAll(x => true);
            da.Obtener<Empresa>().ToList().ForEach(x => da.Eliminar(x));
            da.Obtener<Emprendedor>().ToList().ForEach(x => da.Eliminar(x));
            da.Obtener<DatosLogin>().ToList().ForEach(x => da.Eliminar(x));
        }

        [Test]
        public void AcceptCompanyInvite()
        {
            Invitacion invitacionEnviada = gi.AlmacenarInvitacion<Empresa>("SEMM");
            string tokenEnviado = (gateway as TestGateway).ObtenerUltimoTokenInvitacion();
            Invitacion invite;
            Assert.IsTrue(gi.ValidarInvitacion(tokenEnviado, out invite));
            Assert.AreSame(invitacionEnviada, invite);

            string nombre = "Mercado Libre";
            string rubro = "E-Commerce";
            string descripcion = "Portal de ventas online";
            string ubicacion = "18 de Julio 1234";
            string contacto = "098123456";
            Empresa empresa = new Empresa(nombre, ubicacion, rubro, descripcion, contacto);

            string username = "usuario";
            string password = "contraseña";

            RegistroUsuario.RegistrarUsuario(username, password, empresa);

            Assert.IsTrue(da.Obtener<Empresa>().Count() == 1, string.Join(", ", da.Obtener<Empresa>().Select(x => x.Nombre)));
            DatosLogin dl = da.Obtener<DatosLogin>().Single();
            Assert.IsTrue(dl.Usuario == empresa);
        }

        [Test]
        public void CompanyInviteWrongLink()
        {
            string nombreEmpresa = "MercadoLibre";
            gi.AlmacenarInvitacion<Empresa>("SEMM");
            Invitacion invite;
            Assert.IsFalse(gi.ValidarInvitacion("código_inválido", out invite));
            Assert.IsNull(invite);

            var empresasGuardadas = from Empresa e in da.Obtener<Empresa>()
                                    where e.Nombre == nombreEmpresa
                                    select e;

            Assert.IsTrue(empresasGuardadas.Count() == 0);

            var datosLoginGuardados = from DatosLogin dl in da.Obtener<DatosLogin>()
                                        where dl.Usuario.Nombre == nombreEmpresa
                                        select dl;
            
            Assert.IsTrue(datosLoginGuardados.Count() == 0);
        }

        [Test]
        public void CompanyInviteWrongPersonAccepting()
        {
            string nombreEmpresa = "MercadoLibre";
            gi.AlmacenarInvitacion<Empresa>(nombreEmpresa);
            string tokenEnviado = (gateway as TestGateway).ObtenerUltimoTokenInvitacion();
            Invitacion invite;
            Assert.IsFalse(gi.ValidarInvitacion(tokenEnviado, out invite));
            Assert.IsNull(invite);

            var empresasGuardadas = from Empresa e in da.Obtener<Empresa>()
                                    where e.Nombre == nombreEmpresa
                                    select e;

            Assert.IsTrue(empresasGuardadas.Count() == 0);

            var datosLoginGuardados = from DatosLogin dl in da.Obtener<DatosLogin>()
                                        where dl.Usuario.Nombre == nombreEmpresa
                                        select dl;
            
            Assert.IsTrue(datosLoginGuardados.Count() == 0);
        }

        [Test]
        public void AcceptEntrepreneurInvite()
        {
            string nombreEmprendedor = "Greenpeace";
            Invitacion invitacionEnviada = gi.AlmacenarInvitacion<Emprendedor>(nombreEmprendedor);
            string tokenEnviado = (gateway as TestGateway).ObtenerUltimoTokenInvitacion();
            Invitacion invite;
            Assert.IsTrue(gi.ValidarInvitacion(tokenEnviado, out invite));
            Assert.AreSame(invitacionEnviada, invite);

            string nombre = "Greenpeace.org";
            string lugar = "Eduardo Víctor Haedo 2201";
            string rubro = "Activismo";
            string especializacion = "Ecología";
            List<Habilitacion> habilitaciones = new List<Habilitacion>();
            Emprendedor emprendedor = new Emprendedor(nombre, lugar, rubro, especializacion, habilitaciones);

            string username = "usuario";
            string password = "contraseña";

            RegistroUsuario.RegistrarUsuario(username, password, emprendedor);

            Assert.IsTrue(da.Obtener<Emprendedor>().Count() == 1);
            DatosLogin dl = da.Obtener<DatosLogin>().Single();
            Assert.IsTrue(dl.Usuario == emprendedor);
        }
    }
}