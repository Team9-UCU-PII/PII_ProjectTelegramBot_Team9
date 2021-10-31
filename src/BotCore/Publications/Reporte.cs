using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using BotCore.User;
using Importers;

namespace BotCore.Publication {
    /// <summary>
    /// Genera un reporte del historial de un <see iref ="IUsuario"/>, implementa la interfaz <see iref ="IPrintable"/>.
    /// </summary>
    public class Reporte : IPrintable {
        /// <summary>
        /// Propiedad readonly de <see cref ="Reporte"/>.
        /// </summary>
        /// <value>instancia de <see cref ="DateTime"/> </value>
        public DateTime FechaInicio {get;}
        /// <summary>
        /// Propiedad readonly de <see cref ="Reporte"/>.
        /// </summary>
        /// <value>instancia de <see cref ="DateTime"/> </value>
        public DateTime FechaFin {get;}
        /// <summary>
        /// Propiedad readonly de <see cref ="Reporte"/>.
        /// </summary>
        /// <value>Lista de instancias de <see cref ="Venta"/> </value>
        public List<Venta> Ventas {get;}
        /// <summary>
        /// Propiedad readonly de <see cref ="Reporte"/>.
        /// </summary>
        /// <value>instancia del tipo <see iref ="IUsuario"/> </value>
        public IUsuario Usuario {get;}

        private Reporte(DateTime fechaInicio, DateTime fechaFin, List<Venta> ventas, IUsuario usuario) {
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            this.Ventas = ventas;
            this.Usuario = usuario;
        }
/// <summary>
/// Metodo estatico que genera una instancia de <see cref ="Reporte"/> para el <see iref ="IUsuario"/> 
/// especificado en un periodo de tiempo.
/// </summary>
/// <param name="fechaInicio"></param>
/// <param name="fechaFin"></param>
/// <param name="usuario"></param>
/// <returns> <see cref ="Reporte"/> </returns>
        public static Reporte Generar(DateTime fechaInicio, DateTime fechaFin, IUsuario usuario) {
            List<Venta> ventas = DataAccess.Instancia.Obtener<Venta>();
            ventas = ventas.Where((Venta v) => v.Comprador == usuario || v.Publicacion.Vendedor == usuario).ToList();
            return new Reporte(fechaInicio, fechaFin, ventas, usuario);
        }
/// <summary>
/// Implementacion de <see iref ="IPrintable"/>.
/// </summary>
/// <returns></returns>
        public string GetTextToPrint() {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine($"Período de reporte: {this.FechaInicio} al {this.FechaFin}");
            texto.AppendLine($"Usuario: {this.Usuario.Nombre}");
            texto.AppendLine("Inicio del reporte");
            texto.AppendLine();
            foreach (Venta v in this.Ventas) {
                texto.AppendLine(v.GetTextToPrint());
                texto.AppendLine();
            }
            texto.AppendLine();
            texto.AppendLine("Fin del reporte");
            return texto.ToString();
        }
    }
}