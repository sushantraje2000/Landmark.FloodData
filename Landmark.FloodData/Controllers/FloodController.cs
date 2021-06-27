using Landmark.FloodData.Response;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Landmark.FloodData.Controllers
{
    public class FloodController : ApiController
    {
        private readonly IFloodResponse _floodResponse;

        public FloodController(IFloodResponse floodResponse)
        {
            _floodResponse = floodResponse;
        }
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Json(await _floodResponse.getFloods());
            }
            catch (HttpException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> Get(string region)
        {
            try
            {
                return Json(await _floodResponse.getFloodsByRegion(region));
            }
            catch (HttpException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
    }
}
