using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Services
{
    public class AppUserService
    {
        private readonly HousingContext _context;

        public AppUserService(
            HousingContext context)
        {
            _context = context;
        }

        //Register a user
        public async void Add(AppUser au)
        {
            _context.AppUser.Add(au);

            try
            {
                await _context.SaveChangesAsync();
                return;
            }
            catch (Exception)
            {
                if (_context.AppUser.Any(e => e.Username == au.Username))
                {
                    throw new Exception("User already exists");
                }
                else if (_context.AppUser.Any(e => e.Email == au.Email))
                {
                    throw new Exception("Try a different e-mail address");
                }
                else
                {
                   throw new Exception("Oops, something went wrong. Please contact an administrator for support");
                }
            }
        }

        //Return a user
        public async Task<AppUser> Authenticate(LoginRequest lr)
        {
            string username = lr.Username;
            string password = lr.Password;
            
            var appUser = await Get(username);

            if (appUser == null)
            {
                return null;
            }

            if (Crypto.CheckHash(password, appUser.PassSalt, appUser.PassHash))
            {
                return appUser;
            }

            return null;
        }

        public async Task<bool> Delete(string username)
        {
            var appUser = await Get(username);

            if (appUser == null)
            {
                return false;
            }

            _context.AppUser.Remove(appUser);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<AppUser> Get(string username)
        {
            return await _context.AppUser.FindAsync(username);
        }
        
    }
}
