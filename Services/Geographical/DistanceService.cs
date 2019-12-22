using DAL;
using Microsoft.EntityFrameworkCore;
using Model.Geographical;
using ServiceContract.Geographical;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Geographical
{
    public class DistanceService : GenericRepository<UserDistance>, IDistanceService
    {
        private readonly ApplicationDbContext _context;

        public DistanceService(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<UserDistance> Insert(UserDistance model, bool useInMemory = true)
        {
            var obj = await AddAsync(model);

            if (useInMemory)
            {
                using (RedisClient client = new RedisClient())
                {
                    client.Set<double>(model.UserId + obj.Id, model.Distance);
                }
            }

            return obj;
        }


        public IEnumerable<object> GetByUserId(string userId, bool useInMemory = true)
        {
            if (useInMemory)
            {
                var distances1 = _context.UserDistances
                                  .AsNoTracking()
                                  .Where(x => x.UserId == userId)
                                  .ToList();

                RedisClient client = new RedisClient();

                var list = new List<object>();

                foreach (var item in distances1)
                {
                    list.Add(client.Get<double>(item.UserId + item.Id));
                }

                return list;
            }

            var distances = _context.UserDistances
                                   .AsNoTracking()
                                   .Where(x => x.UserId == userId)
                                   .Select(x => (object)x.Distance)
                                   .ToList();

            return distances;
        }
    }
}
