using ClassLibrary.User;
using System.Collections.Generic;
using System.Text;
using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.Login
{
    public class HandlerLogin : MessageHandlerBase
    {
        public HandlerLogin(IMessageHandler next)
        : base(new string[] {/*Intencionalmente en blanco.*/}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmLogin).CurrentState == fasesLogin.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Inicio de sesión",
                "\n",
                "Ingresa tu Nombre de Usuario.");
                response = sb.ToString();

                (CurrentForm as FrmLogin).CurrentState = fasesLogin.tomandoUser;
                return true;
            }
            else if ((CurrentForm as FrmLogin).CurrentState == fasesLogin.tomandoUser)
            {
                StringBuilder sb = new StringBuilder();
                List <IUsuario> currentUsers = dataBase.Obtener<IUsuario>();
                foreach (IUsuario user in currentUsers)
                {
                    if (user.DatosLogin.NombreUsuario == message.TxtMensaje)
                    {
                        this.supuestoUser = user;
                    }
                }
                if (this.supuestoUser == null)
                {
                    sb.Append("No se encontro a nadie con ese nombre de usuario.");
                    response = sb.ToString();
                    return true;
                }
                else
                {
                sb.Append($"Ahora ingresa tu contraseña");
                response = sb.ToString();
                (CurrentForm as FrmLogin).CurrentState = fasesLogin.tomandoPass;
                return true;
                }
            }
            else if ((CurrentForm as FrmLogin).CurrentState == fasesLogin.tomandoPass)
            {
                StringBuilder sb = new StringBuilder();
                if (supuestoUser.DatosLogin.Contrasenia == message.TxtMensaje)
                {
                    sb.Append($"Iniciada Sesión Correctamente!");
                    response = sb.ToString();
                    (CurrentForm as FrmLogin).userLoggeado = supuestoUser;
                    CurrentForm.ChangeForm(new FrmAltaOferta(), message.ChatID);
                    return true;
                }
                else
                {
                    sb.Append($"Nombre de usuario o contraseña errónea.");
                    response = sb.ToString();
                    supuestoUser = null;
                    (CurrentForm as FrmLogin).CurrentState = fasesLogin.tomandoPass;
                    return true;
                }
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        public enum fasesLogin
        {
            Inicio,
            tomandoUser,
            tomandoPass
        }
        private DataAccess dataBase = DataAccess.Instancia;
        private IUsuario supuestoUser;
    }
}