using Model.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContract.Authentication
{
    public interface ITokenService
    {
        void DeleteOne(RefreshToken t);
        RefreshToken InsertOne(RefreshToken t);
        RefreshToken SelectByUser(string userId);
    }
}
