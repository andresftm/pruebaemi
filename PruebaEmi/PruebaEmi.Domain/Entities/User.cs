using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PruebaEmi.Domain.Entities
{
    /// <summary>
    /// Clase que representa a un usuario en el sistema, con propiedades para almacenar su información básica, como nombre de usuario, correo electrónico, contraseña (almacenada como hash), rol y fecha de creación.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Admin" o "User"
        public DateTime CreatedAt { get; set; }
    }
}

