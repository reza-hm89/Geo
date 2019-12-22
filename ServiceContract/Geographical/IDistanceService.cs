using Model.Geographical;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.Geographical
{
    public interface IDistanceService
    {
        Task<UserDistance> Insert(UserDistance model, bool useInMemory = true);
        IEnumerable<object> GetByUserId(string userId, bool useInMemory = true);
    }
}
