namespace ChatBot
{
    public interface IUsuario
    {
        string NombreUsuario { get; set; }

        string Contraseña { get; }
    }
}