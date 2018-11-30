using Housing.WebAPI.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace Housing.WebAPI.Utils
{
    public static class TokenVerifier
    {
        public static bool CheckUser(ClaimsPrincipal cp, string username)
        {
            if (cp.HasClaim(c => c.Type == "Username"))
            {
                string tkun = cp.Claims.FirstOrDefault(c => c.Type == "Username").Value;
                return tkun == username ? true : false;
            }
            else
            {
                return false;
            }

        }
        
        public static string GetUsername(ClaimsPrincipal cp)
        {
            if (cp.HasClaim(c => c.Type == "Username"))
            {
                return cp.Claims.FirstOrDefault(c => c.Type == "Username").Value;
            }
            else
            {
                return null;
            }
        }

        public static bool CheckStudent(ClaimsPrincipal cp)
        {
            return CheckRole(cp, UserRole.Student);
        }

        public static bool CheckOfficer(ClaimsPrincipal cp)
        {
            return CheckRole(cp, UserRole.Officer);
        }

        public static bool CheckLandlord(ClaimsPrincipal cp)
        {
            return CheckRole(cp, UserRole.Landlord);
        }


        public static bool CheckRole(ClaimsPrincipal cp, UserRole ur)
        {
            if (cp.HasClaim(c => c.Type == "Role"))
            {
                UserRole tkur = (UserRole)Enum.Parse(typeof(UserRole), cp.Claims.FirstOrDefault(c => c.Type == "Role").Value);
                return tkur == ur ? true : false;
            }
            else
            {
                return false;
            }
        }

        public static string GetLandlord(ClaimsPrincipal cp)
        {
            if(!CheckLandlord(cp))
            {
                return null;
            }

            if (cp.HasClaim(c => c.Type == "Username"))
            {
                string tkun = cp.Claims.FirstOrDefault(c => c.Type == "Username").Value;
                return tkun ?? null;
            }
            else
            {
                return null;
            }
            
        }

    }
}
