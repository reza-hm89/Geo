using DAL;
using Model.Authentication;
using ServiceContract.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Authentication
{
    public class TokenService : GenericRepository<RefreshToken>, ITokenService
    {
        ApplicationDbContext Context;

        public TokenService(ApplicationDbContext context) : base(context)
        { Context = context; }

        public RefreshToken InsertOne(RefreshToken t)
        {
            return Add(t);
        }

        public RefreshToken SelectByUser(string userId)
        {
            return Find(x => x.UserId == userId);
        }

        public void DeleteOne(RefreshToken t)
        {
            Delete(t);
        }
    }
}
