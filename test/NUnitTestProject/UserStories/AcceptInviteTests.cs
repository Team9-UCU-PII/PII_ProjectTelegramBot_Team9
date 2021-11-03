using NUnit.Framework;
using ClassLibrary.User;
using BotCore.User;
using Importers;
using Tests.TestClasses;
using System.Linq;
using System.Collections.Generic;

namespace Tests.UserStories
{
    [TestFixture]
    public class AcceptInviteTests
    {
        private GestorInvitaciones gi;
        private DataAccess da;

        [SetUp]
        public void Setup()
        {
            gi = GestorInvitaciones.Instancia;
            gi.GatewayMensajes = TestGateway.Instancia;
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
            string destinatarioEmpresa = "Julia";
            string nombreEmpresa = "MercadoLibre";
            gi.EnviarInvitacion<Empresa>(destinatarioEmpresa, nombreEmpresa);
            string enlaceEnviado = (gi.GatewayMensajes as TestGateway).ObtenerUltimoEnlaceInvitacion(destinatarioEmpresa);
            Invitacion invite = gi.ValidarInvitacion(destinatarioEmpresa, enlaceEnviado);
            Assert.IsNotNull(invite);

            string nombre = "Mercado Libre";
            string rubro = "E-Commerce";
            string descripcion = "Portal de ventas online";
            string ubicacion = "18 de Julio 1234";
            string contacto = "098123456";
            Empresa empresa = new Empresa(nombre, ubicacion, rubro, descripcion, contacto);

            string username = "usuario";
            string password = "contraseña";

            RegistroUsuario.RegistrarUsuario(username, password, empresa);

            Assert.IsTrue(da.Obtener<Empresa>().Count() == 1);
            DatosLogin dl = da.Obtener<DatosLogin>().Single();
            Assert.IsTrue(dl.Usuario == empresa);
        }

        [Test]
        public void CompanyInviteWrongLink()
        {
            string destinatarioEmpresa = "Julia";
            string nombreEmpresa = "MercadoLibre";
            gi.EnviarInvitacion<Empresa>(destinatarioEmpresa, nombreEmpresa);
            Invitacion invite = gi.ValidarInvitacion(destinatarioEmpresa, "123456");
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
            string destinatarioEmpresa = "Julia";
            string nombreEmpresa = "MercadoLibre";
            gi.EnviarInvitacion<Empresa>(destinatarioEmpresa, nombreEmpresa);
            Invitacion invite = gi.ValidarInvitacion("Ezequiel", nombreEmpresa);
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
            string destinatarioEmprendedor = "Manuel";
            string nombreEmprendedor = "Greenpeace";
            gi.EnviarInvitacion<Emprendedor>(destinatarioEmprendedor, nombreEmprendedor);
            string enlaceEnviado = (gi.GatewayMensajes as TestGateway).ObtenerUltimoEnlaceInvitacion(destinatarioEmprendedor);
            Invitacion invite = gi.ValidarInvitacion(destinatarioEmprendedor, enlaceEnviado);
            Assert.IsNotNull(invite);

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