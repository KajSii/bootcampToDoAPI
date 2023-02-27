

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bootcamp.ToDoList.Backend.Database;
using Bootcamp.ToDoList.Backend.Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Login(string username, string password)
        {
            // Vyber usera z DB
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

            // Skontroluj ci existuje
            if (user is null)
            {
                throw new Exception("User not found");
            }

            // Skontroluj ci heslo je korektne
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Bad password");
            }

            // Vrat user id string (potom bude JWT)
            return CreateToken(user);
        }

        public async Task<string> Register(User user, string password)
        {
            // Skontrolovat ci user existuje
            if (await this.UserExists(user.UserName))
            {
                throw new Exception("User already exists");
            }

            // Vytvorit hash
            this.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passswordSalt);

            // Priradit hash + salt userovi
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passswordSalt;

            // Pridat do DB
            await _context.Users.AddAsync(user);

            // Ulozit zmeny
            await _context.SaveChangesAsync();

            return "User successfully created.";
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

            if (user is null)
            {
                return false;
            }

            return true;
            // return !(user is null);
            // return user is not null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString())
            };

            var tokenKey = _configuration["Token:Key"];
            var issuer = _configuration["Token:Issuer"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Issuer = issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}