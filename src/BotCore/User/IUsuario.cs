namespace BotCore.User
{
    public interface IUsuario
    {
        DatosLogin DatosLogin { get;}
        string Nombre{get;set;}
    }
}