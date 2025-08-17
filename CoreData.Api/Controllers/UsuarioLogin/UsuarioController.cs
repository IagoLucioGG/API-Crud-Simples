using CoreData.DTOs.Usuario;
using CoreData.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreData.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServico _usuarioServico;

        public UsuarioController(UsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioCriarDto dto)
        {
            var usuario = await _usuarioServico.Registrar(dto);
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            try
            {
                var token = await _usuarioServico.Login(dto);
                if (token == null)
                    return Unauthorized("Login ou senha inválidos.");

                return Ok(new { Token = token });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Muitas tentativas"))
            {
                return StatusCode(429, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var usuarioNome = User.Identity?.Name;
            return Ok(new { IdUsuario = usuarioId, NomeUsuario = usuarioNome });
        }
    }
}
