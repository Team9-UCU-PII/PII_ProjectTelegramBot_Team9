using ClassLibrary.User;
using MessageGateway.Handlers.RegistroEmpresa;
using System.Collections.Generic;

namespace MessageGateway.Forms
{
    public class FrmRegistroEmpresa : FormularioBase
    {
        public string NombreUsuario;
        public string Contrasenia;
        public string NombrePublico;
        public string Lugar;
        public string Rubro;
        public string Descripcion;
        public string Contacto;
        public Empresa EmpresaPreCreada;

        public FrmRegistroEmpresa(string nombreUsuario, string contrasenia, Empresa empresa)
        : base(new Dictionary<string, string> {})
        {
            this.NombreUsuario = nombreUsuario;
            this.Contrasenia = contrasenia;
            this.messageHandler =
                new HandlerInicio(
                new HandlerNombre(
                new HandlerLugar(
                new HandlerRubro(
                new HandlerDescripcion(
                new HandlerContacto())))));
        }
    }
}