using System.Collections.Generic;
using System.Text;

namespace BotCore.User
{
    /// <summary>
    /// Clase que se encarga de generar usuarios temporales y
    /// enviarselo a personas para facilitar su registro
    /// de manera personal.
    /// </summary>
    public class GestorInvitaciones
    {
        private GestorInvitaciones(){}
        private static GestorInvitaciones instancia {get;set;}

        //Lo hago singleton porque solo se precisa una instancia y tiene que guardar un estado (los invites enviados)
        /// <summary>
        /// Metodo de acceso al singleton.
        /// </summary>
        /// <value></value>
        public static GestorInvitaciones Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GestorInvitaciones();
                }
                return instancia;
            }
        }
        /// <summary>
        /// Lista donde se almacenan las invitaciones enviadas para mantener un registro.
        /// </summary>
        public List<Invitacion> invitacionesEnviadas = new List<Invitacion>();
        /// <summary>
        /// Se almacenan los <see cref = "IUsuario"/> que fueron invitados con éxito.
        /// </summary>
        public List<IUsuario> usuariosInvitados = new List<IUsuario>();
        /// <summary>
        /// Método que envia una invitacion a un numero o contacto.
        /// </summary>
        /// <param name="numeroObjetivo"></param>
        /// <param name="nombreTemp"></param>
        /// <typeparam name="T"></typeparam>
        public void EnviarInvitacion<T>(string numeroObjetivo, string nombreTemp) where T : IUsuario, new()
        {
            IUsuario user = new T();
            user.Nombre = nombreTemp;
            invitacionesEnviadas.Add(Invitacion.Enviar(numeroObjetivo, user));
            //se arma el txt y link y manda al bot
        }

        private bool ValidarInvitacion(Invitacion invite)
        {
            if  (invitacionesEnviadas.Contains(invite))
            {
                return true;
            }
            return false;
        }

        private string ArmarMensajeInvitacion(string Enlace)
        {
            StringBuilder mensaje = new StringBuilder();
            mensaje.Append("Has sido invitado a unirte al chatbot de Telegram\n");
            mensaje.Append($"Link para unirte y registrarte:");
            mensaje.Append(Enlace);

            return mensaje.ToString();
        }
    }
}