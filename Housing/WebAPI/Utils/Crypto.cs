using Housing.WebAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Housing.WebAPI.Utils
{
    public static class Crypto
    {
        public static bool CheckHash(string password, byte[] salt, byte[] hash)
        {
            byte[] byteHashed = GenerateHash(password, salt);
            return byteHashed.SequenceEqual(hash);
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static byte[] GenerateHash(string password, byte[] salt)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));

            return Encoding.ASCII.GetBytes(hash);
        }

        public static string GenerateJSONWebToken(AppUser appUser, AppSettings appSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("Username", appUser.Username),
                new Claim("Role", appUser.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(appSettings.Issuer,
                appSettings.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
