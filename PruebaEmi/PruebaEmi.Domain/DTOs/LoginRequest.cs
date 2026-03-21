namespace PruebaEmi.Domain.DTOs
{
    /// <summary>
    /// clase que representa la solicitud de inicio de sesión, contiene las propiedades necesarias para autenticar a un usuario, como el nombre de usuario y la contraseña.
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}