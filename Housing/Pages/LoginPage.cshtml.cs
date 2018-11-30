using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Models.InternalDTO;
using Housing.WebAPI.Services;
using Housing.WebAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Housing.Pages
{
    public class LoginPage : PageModel
    {
        [BindProperty]
        public LoginRequest LoginData { get; set; }
        public string ErrorMessage { get; private set; }

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
            ErrorMessage = "Error : ";
            if (!ModelState.IsValid)
            {
                ErrorMessage += "Oops, somethign went wrong";
                return Page();
            }

            AppUserService aus = new AppUserService( _context);
            AppUser appUser = await aus.Authenticate(LoginData);

            if (appUser == null)
            {
                ErrorMessage += "Invalid Login Credentials";
                return Page();
            } else
            {
                Response.Cookies.Append("Token", Crypto.GenerateJSONWebToken(appUser, _appSettings));
                ErrorMessage = "";
                return Redirect("/");
            }
        }
    }
}
