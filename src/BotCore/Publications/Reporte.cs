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
        public List<Venta> Ventas {get;}
        public IUsuario Usuario {get;}

        private Reporte(DateTime fechaInicio, DateTime fechaFin, List<Venta> ventas, IUsuario usuario) {
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            this.Ventas = ventas;
            this.Usuario = usuario;
        }

        public static Reporte Generar(DateTime fechaInicio, DateTime fechaFin, IUsuario usuario) {
            List<Venta> ventas = DataAccess.Instancia.Obtener<Venta>();
            ventas = ventas.Where((Venta v) => v.Comprador == usuario || v.Publicacion.Vendedor == usuario).ToList();
            return new Reporte(fechaInicio, fechaFin, ventas, usuario);
        }

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