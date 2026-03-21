using Microsoft.AspNetCore.Mvc;
using PruebaEmi.Domain.DTOs;
using PruebaEmi.Domain.Interfaces;

namespace PruebaEmi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Método para autenticar a un usuario y generar un token JWT. Recibe un objeto LoginRequest con el nombre de usuario y la contraseña, y devuelve un AuthResponse con el token y la información del usuario. Si las credenciales son incorrectas, devuelve un error 401 Unauthorized.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", detail = ex.Message });
            }
        }

        /// <summary>
        /// Método para registrar a un nuevo usuario. Recibe un objeto RegisterRequest con el nombre de usuario, correo electrónico, contraseña y rol, y devuelve un AuthResponse con el token y la información del usuario. Si el nombre de usuario o el correo electrónico ya existen, devuelve un error 409 Conflict. Si los datos de entrada no son válidos, devuelve un error 400 Bad Request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _authService.RegisterAsync(request);
                return CreatedAtAction(nameof(Register), new { username = response.Username }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", detail = ex.Message });
            }
        }
    }
}