using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Housing.Models;
using Housing.Models.ClientServerDTO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using AutoMapper;
using Housing.Models.ServerClientDTO;

namespace Housing.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly HousingContext _context;

        public AppUsersController(HousingContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public IEnumerable<AppUser> GetAppUser()
        {
            var properties = _context.AppUser
                .Where(p => p.Role == UserRole.Landlord)
                .Include(p => p.Properties)
                .ToList();
            return properties;
        }

        // GET: api/users/username
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await _context.AppUser.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }
            
            return Ok(appUser);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser([FromRoute] string id, [FromBody] AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.Username)
            {
                return BadRequest();
            }

            _context.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/users/authenticate
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

            //TODO: Check exists

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: appUser.PassSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            byte[] trueHash = appUser.PassHash;
            byte[] loginHash = Encoding.ASCII.GetBytes(hashed);

            if (trueHash.SequenceEqual(loginHash))
            {
                var loginSuccess = Mapper.Map<AppUser, LoginSuccess>(appUser);
                return Ok(loginSuccess);
            }

            return Ok();

        }

        // POST: api/users/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostAppUser([FromBody] RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string password = registerUser.Password;

            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            AppUser appUser = new AppUser(registerUser, Encoding.ASCII.GetBytes(hashed), salt);

            _context.AppUser.Add(appUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppUserExists(appUser.Username))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            var loginSuccess = Mapper.Map<AppUser, LoginSuccess>(appUser);
            return Ok(loginSuccess);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await _context.AppUser.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.AppUser.Remove(appUser);
            await _context.SaveChangesAsync();

            return Ok(appUser);
        }

        private bool AppUserExists(string id)
        {
            return _context.AppUser.Any(e => e.Username == id);
        }
    }
}