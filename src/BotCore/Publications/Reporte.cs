using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using BotCore.User;
using Importers;

namespace BotCore.Publication {
    public class Reporte : IPrintable {
        public DateTime FechaInicio {get;}
        public DateTime FechaFin {get;}
        public Historial Transacciones {get;}

        private Reporte(DateTime fechaInicio, DateTime fechaFin, Historial transacciones) {
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            this.Transacciones = transacciones;
        }

        public static Reporte Generar(DateTime fechaInicio, DateTime fechaFin, IUsuario usuario) {
            List<Venta> ventas = DataAccess.Obtener<Venta>();
            ventas = ventas.Where((Venta v) => v.Comprador == usuario || v.Publicacion.Vendedor == usuario);
            Historial transacciones = new Historial(ventas, usuario);
            return new Reporte(fechaInicio, fechaFin, transacciones);
        }

        public string GetTextToPrint() {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine($"Período de reporte: {this.FechaInicio} al {this.FechaFin}");
            texto.AppendLine($"Usuario: {this.Transacciones.Usuario.GetTextToPrint()}");
            texto.AppendLine("Inicio del reporte");
            foreach (Venta v in this.Transacciones.Ventas) {
                texto.AppendLine(v.GetTextToPrint());
            }
            texto.AppendLine("Fin del reporte");
            return texto.ToString();
        }
    }
}