using NUnit.Framework;
using ClassLibrary.User;
using BotCore.User;
using System.Linq;

namespace Tests.UserStories
{
    [TestFixture]
    public class SendInviteTests 
    {
        private GestorInvitaciones gi;

        [SetUp]
        public void SetUp()
        {
            this.gi = GestorInvitaciones.Instancia;
        }

        [Test]
        public void InviteCompanyTest()
        {
            string destinatario = "Roberto";
            string nombreEmpresa = "SEMM";

            gi.EnviarInvitacion<Empresa>(destinatario, nombreEmpresa);

            var resultado = from Invitacion x in gi.InvitacionesEnviadas
                            where x.Destinatario == destinatario &&
                            x.OrganizacionInvitada is Empresa &&
                            x.OrganizacionInvitada.Nombre == nombreEmpresa &&
                            !x.fueAceptada
                            select x;

            Assert.IsTrue(resultado.Count() == 1);
        }

        [Test]
        public void InviteEntrepreneurTest()
        {
            string destinatario = "Gonzalo";
            string nombreEmprendedor = "Tapitas Oportunidades";

            gi.EnviarInvitacion<Emprendedor>(destinatario, nombreEmprendedor);

            var resultado = from Invitacion x in gi.InvitacionesEnviadas
                            where x.Destinatario == destinatario &&
                            x.OrganizacionInvitada is Emprendedor &&
                            x.OrganizacionInvitada.Nombre == nombreEmprendedor &&
                            !x.fueAceptada
                            select x;

            Assert.IsTrue(resultado.Count() == 1);
        }
    }
}