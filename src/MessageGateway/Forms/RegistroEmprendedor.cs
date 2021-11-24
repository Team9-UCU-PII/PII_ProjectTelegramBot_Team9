using ClassLibrary.User;
using MessageGateway.Handlers.RegistroEmpresa;
using System.Collections.Generic;

namespace MessageGateway.Forms
{
    public class FrmRegistroEmprendedor : FormularioBase
    {
        public string NombreUsuario;
        public string Contrasenia;
        public string NombrePublico;
        public string Lugar;
        public string Rubro;
        public string Especializacion;
        public List<Habilitacion> Habilitaciones;
        public Emprendedor EmprendedorPreCreado;

        public FrmRegistroEmprendedor(string nombreUsuario, string contrasenia, Emprendedor emprendedor)
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