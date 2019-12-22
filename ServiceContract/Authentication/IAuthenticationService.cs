using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContract.Authentication
{
    public interface IAuthenticationService
    {
        string GenerateToken(string userId);
    }
}
