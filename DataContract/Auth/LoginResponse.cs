using System;
using System.Collections.Generic;
using System.Text;

namespace DataContract.Auth
{
    public class LoginResponse : ApiResult
    {
        public string auth_token { get; set; }
        public string refreshToken { get; set; }
    }
}
