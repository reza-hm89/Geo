using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model.Users;

namespace ServiceContract.Users
{
    public interface IUserService
    {
        Task<IEnumerable<User>> SelectAll();
        Task<bool> Register(User model);
        User GetByEmail(string email);
    }
}
