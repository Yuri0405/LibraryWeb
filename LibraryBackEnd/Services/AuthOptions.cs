using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryBackEnd.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";//token issuer
        public const string AUDIENCE = "MyAuthClient";//token consumer
        const string KEY = "mysupersecret_secretkey!123";//encryption key
        public const int LIFETIME = 1;// lifetime of token - 1 minute
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
