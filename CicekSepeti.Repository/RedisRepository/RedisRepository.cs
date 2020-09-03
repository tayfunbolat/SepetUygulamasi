using Common.Api;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{




    public class RedisRepository :IRedisRepository
    {
        private readonly RedisContext redisContext;
        public RedisRepository(RedisContext redisContext)
        {
            this.redisContext = redisContext;
        }

        public async Task Add(string key, string data)
        {
           await redisContext.GetDb().SetAddAsync(key, data);
        }
    }
}
