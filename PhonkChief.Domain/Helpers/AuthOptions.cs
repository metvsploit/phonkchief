using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PhonkChief.Domain.Helpers
{
    public class AuthOptions
    {
        public const string ISSUER = "PhonkChiefServer"; 
        public const string AUDIENCE = "PhonkChiefClient"; 
        const string KEY = "yfjb@:ds!39dEFsgnv";   
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
