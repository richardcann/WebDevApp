using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.InternalDTO;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Housing.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly HousingContext _context;

        public AppUsersController(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            HousingContext context)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _context = context;
        }

        // GET: api/users
        //[HttpGet]
        //public IEnumerable<AppUser> GetAppUser()
        //{
        //    var properties = _context.AppUser
        //        .Where(p => p.Role == UserRole.Landlord)
        //        .Include(p => p.Properties)
        //        .ToList();
        //    return properties;
        //}

        // POST: api/users/register
        // Upon registration, return token & login by default
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostAppUser([FromBody] RegisterUser RegisterUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] salt = GenerateSalt();
            byte[] hash = GenerateHash(RegisterUser.Password, salt);

            AppUser AppUser = new AppUser(RegisterUser, hash, salt);
            _context.AppUser.Add(AppUser);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppUserExists(AppUser.Username))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(AppUser);
            loginSuccess.Token = GenerateJSONWebToken(AppUser);
            return Ok(loginSuccess);
        }

        // POST: api/users/authenticate
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> PostAppUser([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string password = loginRequest.Password;
            var appUser = await _context.AppUser.FindAsync(loginRequest.Username);

            if (appUser == null)
            {
                return Ok();
            }

            if (CheckHash(password, appUser.PassSalt, appUser.PassHash))
            {
                var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(appUser);
                loginSuccess.Token = GenerateJSONWebToken(appUser);
                return Ok(loginSuccess);
            }

            return Ok();
        }

        // GET: api/users/username
        // Users can only retrieve their creds with this api call
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userCp = HttpContext.User;
            var appUser = await _context.AppUser.FindAsync(id);

            if (appUser == null || !TokenVerifier.CheckUser(userCp, id))
            {
                return NotFound();
            }

            var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(appUser);
            return Ok(loginSuccess);
        }

        //// PUT: api/users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAppUser([FromRoute] string id, [FromBody] AppUser appUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != appUser.Username)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(appUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AppUserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        
        // DELETE: api/users/username
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCp = HttpContext.User;
            var appUser = await _context.AppUser.FindAsync(id);

            if (appUser == null || !TokenVerifier.CheckUser(userCp, id))
            {
                return NotFound();
            }

            _context.AppUser.Remove(appUser);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AppUserExists(string id)
        {
            return _context.AppUser.Any(e => e.Username == id);
        }
        
        private static bool CheckHash(string password, byte[] salt, byte[] hash)
        {
            byte[] byteHashed = GenerateHash(password, salt);
            return byteHashed.SequenceEqual(hash);
        }

        private static byte[] GenerateSalt()
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

        private string GenerateJSONWebToken(AppUser appUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("Username", appUser.Username),
                new Claim("Role", appUser.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_appSettings.Issuer,
                _appSettings.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}