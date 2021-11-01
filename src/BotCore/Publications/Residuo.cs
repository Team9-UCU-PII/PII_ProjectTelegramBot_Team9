using System.Text;
using System.Linq;
using BotCore.User;

namespace BotCore.Publication
{
  public class Residuo : IPrintable
  {
    public Residuo(Categoria categoria, string descripcion, string unidadMedida, Habilitacion[] habilitaciones)
    {
      this.Categoria = categoria;
      this.Descripcion = descripcion;
      this.UnidadMedida = unidadMedida;
      this.Habilitaciones = habilitaciones;
    }

    public Categoria Categoria {get; set;}
    public string Descripcion {get; set;}
    public string UnidadMedida {get; set;}
    public Habilitacion[] Habilitaciones {get; set;}

    public string GetTextToPrint() {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Descripcion} ({this.Categoria})");
      text.AppendLine($"Los emprendedores requieren las siguientes habilitaciones para manejar este residuo: {string.Join(", ", this.Habilitaciones.Select((Habilitacion h) => h.Nombre))}");
      return text.ToString();
    }
  }
}