using CoreData.Data.Context;
using CoreData.DTOs.Usuario;
using CoreData.Models.Usuario;
using CoreData.Services.Autenticacao.LoginTentativas;
using CoreData.Services.Autenticacao.SenhaHash;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;

namespace CoreData.Services
{
    public class UsuarioServico
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UsuarioServico(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<UsuarioLerDto> Registrar(UsuarioCriarDto dto)
        {
            var usuario = new Usuario
            {
                NomeUsuario = dto.NomeUsuario,
                LoginUsuario = dto.LoginUsuario,
                SenhaHash = SenhaHash.GerarHash(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioLerDto
            {
                IdUsuario = usuario.IdUsuario,
                NomeUsuario = usuario.NomeUsuario,
                LoginUsuario = usuario.LoginUsuario,
                DataCriacao = usuario.DataCriacao
            };
        }

        public async Task<string?> Login(UsuarioLoginDto dto)
        {
            if (!LoginTentativas.PodeTentar(dto.LoginUsuario))
                throw new InvalidOperationException("Muitas tentativas. Tente novamente mais tarde.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.LoginUsuario == dto.LoginUsuario);

            if (usuario == null || !SenhaHash.VerificarHash(dto.Senha, usuario.SenhaHash))
            {
                LoginTentativas.RegistrarFalha(dto.LoginUsuario);
                return null;
            }

            LoginTentativas.ResetarTentativas(dto.LoginUsuario);

            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_config["Jwt:Chave"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NomeUsuario)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
