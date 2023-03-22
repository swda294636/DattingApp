using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Service
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            //產生對稱式key，來簽屬token
            _key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            //聲明
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            //簽署憑證
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            //使用SecurityTokenDescriptor建立JWD相關設定
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials =creds
            };

            //使用JwtSecurityTokenHandler來建立JWD
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //WriteToken() 直接JWD將物件轉換成string
            return tokenHandler.WriteToken(token);

        }

        
    }
}