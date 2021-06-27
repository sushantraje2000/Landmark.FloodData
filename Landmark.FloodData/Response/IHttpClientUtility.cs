using System.Net.Http;
using System.Threading.Tasks;

namespace Landmark.FloodData.Response
{
    public interface IHttpClientUtility
    {
        Task<string> GetAsync();
    }
}