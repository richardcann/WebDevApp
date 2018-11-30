using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.InternalDTO;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Utils;
using Microsoft.Extensions.Options;

namespace Housing.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AppUsersController : Controller
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
       
        //// POST: api/users/register
        //// Upon registration, return token & login by default
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> PostAppUser([FromBody] RegisterUser RegisterUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if(RegisterUser.Username == null)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status409Conflict);
        //    }

        //    byte[] salt = Crypto.GenerateSalt();
        //    byte[] hash = Crypto.GenerateHash(RegisterUser.Password, salt);

        //    AppUser AppUser = new AppUser(RegisterUser, hash, salt);

        //    TryValidateModel(AppUser);

        //    if(ModelState.IsValid)
        //    {
        //        _context.AppUser.Add(AppUser);

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (AppUserExists(AppUser.Username))
        //            {
        //                return new StatusCodeResult(StatusCodes.Status409Conflict);
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(AppUser);
        //        return Ok(loginSuccess);
        //    }

        //    return BadRequest();
            
        //}
        
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
                return NotFound();
            }

            if (Crypto.CheckHash(password, appUser.PassSalt, appUser.PassHash))
            {
                var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(appUser);
                return Ok(loginSuccess);
            }

            return NotFound();
        }

        // GET: api/users/fromtoken
        // Users can only retrieve their creds with this api call
        [Authorize]
        [HttpGet("fromtoken")]
        public async Task<IActionResult> GetAppUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usercp = HttpContext.User;
            var appuser = await _context.AppUser.FindAsync(TokenVerifier.GetUsername(usercp));
            if (appuser == null)
            {
                return NotFound();
            }

            var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(appuser);
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
        
    }
}