using System.Collections.Generic;
using ClassLibrary.Publication;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
  public interface IListableForm
  {
    List<Publicacion> publicacionesFiltradas {get; set;}
  }
}