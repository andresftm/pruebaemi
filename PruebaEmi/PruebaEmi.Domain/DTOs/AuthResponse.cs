namespace PruebaEmi.Domain.DTOs
{
    /// <summary>
    /// Clase que representa la respuesta de autenticación, incluyendo el token JWT y la información del usuario.
    /// </summary>
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}