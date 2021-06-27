using Landmark.FloodData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landmark.FloodData.Response
{
    public interface IFloodResponse
    {
        Task<IEnumerable<Flood>> getFloods();
        Task<IEnumerable<Flood>> getFloodsByRegion(string strRegion);
    }
}