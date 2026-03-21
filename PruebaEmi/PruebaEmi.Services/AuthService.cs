using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaEmi.Domain.DTOs;
using PruebaEmi.Domain.Entities;
using PruebaEmi.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaEmi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Método para autenticar a un usuario. Verifica el username y password, y si son correctos, genera un token JWT con la información del usuario (id, username, email, role) y lo devuelve junto con la fecha de expiración del token.
        /// </summary>
        /// <param name="request"> Un objeto que trae un usuario y la contrase de quien se quiere loquear.</param>
        /// <returns> Un objeto con la información del logueo.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            // Buscar usuario por username
            var users = await _userRepository.FindAsync(u => u.Username == request.Username);
            var user = users.FirstOrDefault();

            if (user == null)
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");

            // Verificar contraseña
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");

            // Generar token
            var token = GenerateJwtToken(user);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = expiresAt
            };
        }

        /// <summary>
        /// Método para registrar a un nuevo usuario. Verifica que el username y email no existan, valida el rol, hashea la contraseña, crea un nuevo usuario en la base de datos y genera un token JWT con la información del nuevo usuario (id, username, email, role) y lo devuelve junto con la fecha de expiración del token.
        /// </summary>
        /// <param name="request"> Datos del usuario a registrar  </param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Verificar si el usuario ya existe
            var existingUsers = await _userRepository.FindAsync(
                u => u.Username == request.Username || u.Email == request.Email
            );

            if (existingUsers.Any())
                throw new InvalidOperationException("El usuario o email ya existe");

            // Validar rol
            if (request.Role != "Admin" && request.Role != "User")
                throw new ArgumentException("El rol debe ser 'Admin' o 'User'");

            // Hash de la contraseña
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Crear nuevo usuario
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.AddAsync(user);

            // Generar token
            var token = GenerateJwtToken(createdUser);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            return new AuthResponse
            {
                Token = token,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Role = createdUser.Role,
                ExpiresAt = expiresAt
            };
        }

        /// <summary>
        /// Beneración del token JWT con la información del usuario (id, username, email, role) y la configuración de JWT (secret key, issuer, audience) para firmar el token y establecer su expiración en 24 horas.
        /// </summary>
        /// <param name="user"> Los datos del usuario para generar el toquen  </param>
        /// <returns></returns>
        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}