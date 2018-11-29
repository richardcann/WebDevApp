using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Models.InternalDTO;
using Housing.WebAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Pages
{
    public class LoginPage : PageModel
    {
        [BindProperty]
        public LoginRequest LoginData { get; set; }
       
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly HousingContext _context;

        public LoginPage(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            HousingContext context)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string password = LoginData.Password;
            var appUser = await _context.AppUser.FindAsync(LoginData.Username);

            if (appUser == null)
            {
                return NotFound();
            }

            if (Crypto.CheckHash(password, appUser.PassSalt, appUser.PassHash))
            {
                var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(appUser);
                Response.Cookies.Append("Token", Crypto.GenerateJSONWebToken(appUser, _appSettings));
                return LocalRedirect("/");
            }

            return Page();


            //if (ModelState.IsValid)
            //{
            //    var isValid = (LoginData.Username == "username" && LoginData.Password == "password");
            //    if (!isValid)
            //    {
            //        ModelState.AddModelError("", "username or password is invalid");
            //        return Page();
            //    }
            //    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, LoginData.Username));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, LoginData.Username));
            //    var principal = new ClaimsPrincipal(identity);
            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = LoginData.RememberMe });
            //    return RedirectToPage("Index");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "username or password is blank");
            //    return Page();
            //}
        }


    }
}
