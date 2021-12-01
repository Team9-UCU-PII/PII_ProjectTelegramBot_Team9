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
        /// Lista donde se almacenan las invitaciones enviadas para mantener un registro.
        /// </summary>
        public List<Invitacion> InvitacionesEnviadas = new List<Invitacion>();

        /// <summary>
        /// Metodo que crea la <see cref = "Invitacion"/> y la envia al destinatario especificado.
        /// Se trata de una implementación de patrón Creator, ya que considerando la muy estrecha relación
        /// entre esta clase y la clase Invitacion, es razonable que la responsabilidad de instanciar
        /// esa clase recaiga en este Gestor, de forma que el resto de clases quedan libres de la responsabilidad
        /// de crear objetos Invitacion.
        /// </summary>
        /// <param name="nombreTempUsuario">el nombre provisorio con el que se almacena el usuario.</param>
        public Invitacion AlmacenarInvitacion(string nombreTempUsuario)
        {
            Empresa user = new Empresa(nombreTempUsuario);
            user.Nombre = nombreTempUsuario;
            Invitacion invite = new Invitacion(user);
            this.InvitacionesEnviadas.Add(invite);
            return invite;
        }

        /// <summary>
        /// Metodo utilizado para validar que la invitación fue aceptada.
        /// </summary>
        /// <param name="token">el código que el usuario debe ingresar para aceptar la invitación.</param>
        /// <param name="invite">la invitación aceptada, o null si no se encontró una invitación con ese token.</param>
        /// <returns>true si existía una invitación que aceptar, de lo contrario false.</returns>
        public bool ValidarInvitacion(string token, out Invitacion invite) 
        {
            invite = this.InvitacionesEnviadas.Where(
                i => i.Token == token && !i.FueAceptada
            ).SingleOrDefault();
            if (invite != null)
            {
                invite.Aceptar();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}