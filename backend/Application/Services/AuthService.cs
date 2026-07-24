using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CadernosDeErros.Application.Interfaces;
using CadernosDeErros.DTOs;
using CadernosDeErros.Entities;
using CadernosDeErros.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CadernosDeErros.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var normalizedEmail = registerDto.Email.Trim().ToLower();

            var existingUser = await _context.Usuarios
                .AnyAsync(u => u.Email.ToLower() == normalizedEmail);

            if (existingUser)
            {
                throw new InvalidOperationException("Este e-mail já está cadastrado.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Senha);

            var usuario = new Usuario
            {
                Nome = registerDto.Nome.Trim(),
                Email = normalizedEmail,
                SenhaHash = passwordHash,
                DataCriacao = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return GenerateAuthResponse(usuario);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var normalizedEmail = loginDto.Email.Trim().ToLower();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash))
            {
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
            }

            return GenerateAuthResponse(usuario);
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null) return null;

            return new UserProfileDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao
            };
        }

        private AuthResponseDto GenerateAuthResponse(Usuario usuario)
        {
            var secret = _configuration["Jwt:Secret"] ?? "CadernosDeErros_SuperSecretKey_For_JWT_Authentication_2026!#$";
            var issuer = _configuration["Jwt:Issuer"] ?? "CadernosDeErrosAPI";
            var audience = _configuration["Jwt:Audience"] ?? "CadernosDeErrosApp";
            var expireDays = int.TryParse(_configuration["Jwt:ExpireDays"], out var days) ? days : 7;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(expireDays);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = tokenString,
                Expiration = expiration
            };
        }
    }
}
