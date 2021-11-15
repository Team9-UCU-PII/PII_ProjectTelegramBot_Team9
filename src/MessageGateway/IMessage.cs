//--------------------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace MessageGateway
{
    public interface IMessage
    {
        string ChatID { get; }

        string TxtMensaje { get; }

        IMessage CrearRespuesta(string txt);
    }
}