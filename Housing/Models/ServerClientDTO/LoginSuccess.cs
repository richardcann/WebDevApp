using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Models.ServerClientDTO
{
    //DTO to relay successful login info 
    public class LoginSuccess
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
    }
}
