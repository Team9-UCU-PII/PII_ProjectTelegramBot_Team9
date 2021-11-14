//--------------------------------------------------------------------------------
// <copyright file="GestorInvitaciones.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patron utilizado: Singleton.
// Esta clase utiliza este patrón porque solo se necesita una instancia y almacena un estado.
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System;
using ClassLibrary.User;
using MessageGateway;

namespace BotCore.User
{
    /// <summary>
    /// Clase que se encarga de generar usuarios temporales y
    /// enviarselo a personas para facilitar su registro
    /// de manera personal.
    /// </summary>
    public class GestorInvitaciones
    {
        private GestorInvitaciones()
        {
            this.GatewayMensajes = IGateway.Instancia;
        }

        /// <summary>
        /// Metodo de acceso al singleton.
        /// </summary>
        /// <value><see cref = "GestorInvitaciones"/>.</value>
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

        private static GestorInvitaciones instancia { get; set; }

        /// <summary>
        /// Obtiene o establece el gateway para enviar la invitación.
        /// </summary>
        /// <value>Por Defecto: TelegramService. Se puede establecer cualquier <see iref = "IGateway"/>.</value>
        public IGateway GatewayMensajes { get; set; }

        /// <summary>
        /// Lista donde se almacenan las invitaciones enviadas para mantener un registro.
        /// </summary>
        public List<Invitacion> InvitacionesEnviadas = new List<Invitacion>();

        /// <summary>
        /// Metodo que crea la <see cref = "Invitacion"/> y la envia al destinatario especificado.
        /// </summary>
        /// <param name="destinatario">El contacto objetivo (username).</param>
        /// <param name="nombreTemp">Nombre placeholder para el IUsuario, el destinatario lo sobreescribirá luego.</param>
        /// <typeparam name="T"></typeparam>
        public void EnviarInvitacion<T>(string destinatario, string nombreTemp)
        where T : IUsuario, new()
        {
            IUsuario user = new T();
            user.Nombre = nombreTemp;
            Invitacion invite = new Invitacion(user, destinatario, GatewayMensajes);

            GatewayMensajes.EnviarMensaje(invite.ArmarMensajeInvitacion());

            this.InvitacionesEnviadas.Add(invite);
        }

        /// <summary>
        /// Metodo utilizado para validar que la invitación fue aceptada. Es utilizado
        /// externamente por <see cref = "MessageGateway"/>.
        /// </summary>
        /// <param name="usuarioAceptante"><see langword = "string"/>.</param>
        /// <param name="token"><see langword = "string"/>.</param>
        /// <returns><see cref = "Invitacion"/>.</returns>
        public Invitacion ValidarInvitacion(string usuarioAceptante, string token) 
        {
            Invitacion invite = this.InvitacionesEnviadas.Where(
                (Invitacion i) => i.Destinatario == usuarioAceptante && i.Token == token && !i.FueAceptada
            ).SingleOrDefault();
            if (invite != null)
            {
                invite.Aceptar();
                return invite;
            }
            else
            {
                return null;
            }
        }
    }
}