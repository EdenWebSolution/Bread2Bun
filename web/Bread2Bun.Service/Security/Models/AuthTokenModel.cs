using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class AuthTokenModel
    {
        public string Token { get; private set; }
        public dynamic ExtensionData { get; private set; }
        public AuthTokenModel()
        {

        }
        public AuthTokenModel(JwtSecurityToken token)
        {
            this.Token = new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthTokenModel(JwtSecurityToken token, dynamic extensionData)
        {
            this.Token = new JwtSecurityTokenHandler().WriteToken(token); ;
            this.ExtensionData = extensionData;
        }
    }
}
