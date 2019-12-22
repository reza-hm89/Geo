using DAL;
using Model.Users;
using Microsoft.AspNetCore.Identity;
using ServiceContract.Users;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Service.Users
{
    public class UserService : GenericRepository<User>, IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<User>> SelectAll()
        {
            return await GetAllAsync();
        }

        public User GetByEmail(string email)
        {
            return Find(x => x.Email == email);
        }

        public async Task<bool> Register(User model)
        {
            await AddAsync(model);

            return true;
        }

    }
}
