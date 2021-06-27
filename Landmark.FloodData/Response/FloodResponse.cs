using Landmark.FloodData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Landmark.FloodData.Response
{
    public class FloodResponse : IFloodResponse
    {
        private readonly IHttpClientUtility _httpClientUtility;
        public FloodResponse(IHttpClientUtility httpClientUtility)
        {
            _httpClientUtility = httpClientUtility;
        }
        public async Task<IEnumerable<Flood>> getFloods()
        {
            var environmentAgencyFloodAlertServicePayload = JsonConvert.DeserializeObject<EnvironmentAgencyFloodAlertServicePayload>(
                        await _httpClientUtility.GetAsync());
            if (environmentAgencyFloodAlertServicePayload == null || !environmentAgencyFloodAlertServicePayload.Items.Any())
            {
                return new List<Flood>();
            }
            return environmentAgencyFloodAlertServicePayload.Items.Select(item => new Flood()
            {
                Id = item.Id.Replace("http://environment.data.gov.uk/flood-monitoring/id/floods/", ""),// fetch from Config
                Region = item.EaRegionName,
                FloodAreaId = item.FloodAreaId,
                EaAreaName = item.EaAreaName,
                TimeRaised = item.TimeRaised,
                Severity = (SeverityLevel)item.SeverityLevel
            });
        }
        public async Task<IEnumerable<Flood>> getFloodsByRegion(string strRegion)
        {
            return (await getFloods()).Where(item => string.Equals(item.Region, strRegion, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}