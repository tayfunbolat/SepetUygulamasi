using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
    public interface IRedisRepository
    {
        Task Add(string key, string data);
    }
}