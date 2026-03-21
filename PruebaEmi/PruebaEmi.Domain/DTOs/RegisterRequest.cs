using System.ComponentModel.DataAnnotations;

namespace PruebaEmi.Domain.DTOs
{
    /// <summary>
    /// Clase que representa la solicitud de registro de un nuevo usuario. Contiene las propiedades necesarias para crear una cuenta, como el nombre de usuario, correo electrónico, contraseña y rol del usuario. Esta clase se utiliza para recibir los datos del cliente al momento de registrarse en la aplicación.
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Admin" o "User"
    }
}