using NUnit.Framework;
using ClassLibrary.User;
using BotCore.User;
using System.Linq;
using Tests.TestClasses;
using MessageGateway;

namespace Tests.UserStories
{
    [TestFixture]
    public class SendInviteTests 
    {
        private GestorInvitaciones gi;
        private IGateway gateway;

        [SetUp]
        public void SetUp()
        {
            this.gi = GestorInvitaciones.Instancia;
            this.gateway = TestGateway.Instancia;
        }

        [Test]
        public void InviteCompanyTest()
        {
            string nombreEmpresa = "SEMM";

            gi.AlmacenarInvitacion<Empresa>(nombreEmpresa);

            var resultado = from Invitacion x in gi.InvitacionesEnviadas
                            where x.OrganizacionInvitada is Empresa &&
                            x.OrganizacionInvitada.Nombre == nombreEmpresa &&
                            !x.FueAceptada
                            select x;

            Assert.IsTrue(resultado.Count() == 1);
        }

        [Test]
        public void InviteEntrepreneurTest()
        {
            string nombreEmprendedor = "Tapitas Oportunidades";

            gi.AlmacenarInvitacion<Emprendedor>(nombreEmprendedor);

            var resultado = from Invitacion x in gi.InvitacionesEnviadas
                            where x.OrganizacionInvitada is Emprendedor &&
                            x.OrganizacionInvitada.Nombre == nombreEmprendedor &&
                            !x.FueAceptada
                            select x;

            Assert.IsTrue(resultado.Count() == 1);
        }
    }
}