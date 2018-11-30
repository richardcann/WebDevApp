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
using Housing.WebAPI.Services;

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
            AppUserService aus = new AppUserService(_context);
            AppUser AppUser = await aus.Get(TokenVerifier.GetUsername(usercp));

            var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(AppUser);
            return Ok(loginSuccess);
        }

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

            if (!TokenVerifier.CheckUser(userCp, id))
            {
                AppUserService aus = new AppUserService(_context);
                if(await aus.Delete(id))
                {
                    return Ok();
                } else
                {
                    return NotFound();
                }
            }
            
            return Unauthorized();
        }
        
    }
}