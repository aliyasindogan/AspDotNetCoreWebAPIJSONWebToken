using System;

namespace AspDotNetCoreWebAPIJSONWebToken.DTOs
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
