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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Pages
{
    public class RegisterPage : PageModel
    {
        [BindProperty]
        public RegisterUser RegisterUser { get; set; }

        public string ErrorMessage { get; private set; }

        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly HousingContext _context;

        public RegisterPage(
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
                return BadRequest(ModelState);
            }

            if (RegisterUser.Username == null)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            byte[] salt = Crypto.GenerateSalt();
            byte[] hash = Crypto.GenerateHash(RegisterUser.Password, salt);

            AppUser AppUser = new AppUser(RegisterUser, hash, salt);

            TryValidateModel(AppUser);

            if (ModelState.IsValid)
            {
                _context.AppUser.Add(AppUser);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (_context.AppUser.Any(e => e.Username == AppUser.Username))
                    {

                        ErrorMessage += "User already exists";
                        return Page();
                        //new StatusCodeResult(StatusCodes.Status409Conflict);
                    }
                    else if (_context.AppUser.Any(e => e.Email == AppUser.Email))
                    {
                        ErrorMessage += "Try a different e-mail address";
                        return Page();
                    }
                    else
                    {
                        ErrorMessage += "Oops, something went wrong. Please contact an administrator for support";
                        return Page();
                    }
                }

                ErrorMessage = "";
                var loginSuccess = _mapper.Map<AppUser, LoginSuccess>(AppUser);
                Response.Cookies.Append("Token", Crypto.GenerateJSONWebToken(AppUser, _appSettings));
                return Redirect("/");
            }

            if(AppUser.Username.Length< 3 || AppUser.Username.Length> 30){
                ErrorMessage += "Username must be between 3 and 30 characters long";
            } else if(AppUser.Username.Length == 0){
                ErrorMessage += "Please enter your First Name";
            } else if(AppUser.Username.Length > 30){
                ErrorMessage += "First Name must be less than 30 characters long";
            } else if (AppUser.LastName.Length == 0){
                ErrorMessage += "Please enter your Last Name";
            } else if (AppUser.LastName.Length > 30){
                ErrorMessage += "Last Name must be less than 30 characters long";
            } else if (AppUser.Role != UserRole.Landlord ||
                        AppUser.Role != UserRole.Officer ||
                        AppUser.Role != UserRole.Student)
            {
                ErrorMessage += "Invalid Role";
            } else if (AppUser.Email.Length == 0){
                ErrorMessage += "Please enter your e-mail address";
            } else if (!new EmailAddressAttribute().IsValid(AppUser.Email))
            {
                ErrorMessage += "Invalid e-mail address";
            } else
            {
                ErrorMessage += "Oops, something went wrong. Please contact an administrator for support";
            }
            return Page();
          
        }
    }
}
