using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;

namespace PhonkChief.Domain.Helpers
{
    public static class HashPassword
    {
        public static string Hash(this string password)
        {
            var hashPass = Crypto.HashPassword(password);
            return hashPass;
        }

        public static bool Verification(this string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }
    }
}
