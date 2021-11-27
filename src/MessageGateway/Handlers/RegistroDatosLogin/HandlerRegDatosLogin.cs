using System.Text;
using System.Collections.Generic;
using ClassLibrary.User;
using MessageGateway.Forms;
using Importers;

namespace MessageGateway.Handlers.RegistroDatosLogin
{
    public class HandlerRegDatosLogin : MessageHandlerBase
    {
        public HandlerRegDatosLogin(IMessageHandler next)
        : base(new string[] {}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                "Registro de nuevo usuario",
                "\n",
                "Ingresa tu nombre de usuario.");
                response = sb.ToString();

                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.tomandoUser;
                return true;
            }
            else if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.tomandoUser)
            {
                List<IUsuario> usersExistentes = database.Obtener<IUsuario>();
                StringBuilder sb = new StringBuilder();
                foreach (IUsuario user in usersExistentes)
                {
                    if (user.DatosLogin.NombreUsuario == message.TxtMensaje)
                    {
                        sb.Append("Este nombre de usuario ya esta tomado");
                        response = sb.ToString();
                        return true;
                    }
                }
                
                sb.Append("Muy bien, ahora escribe una contraseña.");
                response = sb.ToString();
                (CurrentForm as FrmRegistroDatosLogin).NombreUsuario = message.TxtMensaje;
                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.tomandoPass;
                return true;
            }
            else if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.tomandoPass)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Confirma tu contraseña.");
                response = sb.ToString();
                passChkr = message.TxtMensaje;

                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.confirmandoPass;
                return true;
            }
            else if ((CurrentForm as FrmRegistroDatosLogin).CurrentState == faseRegDL.confirmandoPass)
            {
                StringBuilder sb = new StringBuilder();
                
                if (passChkr != message.TxtMensaje)
                {
                    sb.Append("Error al confirmar, intenta de nuevo.");
                    response = sb.ToString();
                    return true; 
                }
                sb.Append("¡Contraseña Confirmada!");
                response = sb.ToString();
                (CurrentForm as FrmRegistroDatosLogin).Password = passChkr;
                (CurrentForm as FrmRegistroDatosLogin).CurrentState = faseRegDL.Done;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        public enum faseRegDL
        {
            Inicio,
            tomandoUser,
            tomandoPass,
            confirmandoPass,
            Done
        }
        private DataAccess database = DataAccess.Instancia;
        private string passChkr;
        public DatosLogin result;
    }
}