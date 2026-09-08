using EnterpriseTaskManager.Application.Auth;
using EnterpriseTaskManager.Application.DTOs.Auth;
using EnterpriseTaskManager.Application.Services.Interfaces;
using EnterpriseTaskManager.Domain.Entities;
using EnterpriseTaskManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EnterpriseTaskManager.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtSettings _jwt;

        public AuthService(AppDbContext db, IOptions<JwtSettings> jwtSettings)
        {
            _db = db;
            _jwt = jwtSettings.Value;
        }


        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);

            if (existingUser != null) throw new Exception("User already exists");

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = HashPassword(dto.Password),
                DisplayName = dto.FullName,
                Role = "User"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (user == null)
                throw new Exception("Invalid credentials.");

            if (!VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials.");

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes)
            };
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
