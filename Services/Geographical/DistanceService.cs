using DAL;
using Model.Geographical;
using ServiceContract.Geographical;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
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
            var distances = FindAll(x => x.UserId == userId);

            if (useInMemory)
            {
                RedisClient client = new RedisClient();

                var list = new List<object>();

                foreach (var item in distances)
                {
                    list.Add(client.Get<double>(item.UserId + item.Id));
                }

                return list;
            }

            return distances;
        }
    }
}
