using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Landmark.FloodData.Response
{
    public class HttpClientUtility : IHttpClientUtility
    {
        private HttpResponseMessage _httpResponseMessage;
        public async Task<string> GetAsync()
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("http://environment.data.gov.uk") })// fetch from Config
            {
                _httpResponseMessage = await httpClient.GetAsync("flood-monitoring/id/floods");// fetch from Config
                if (_httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return await _httpResponseMessage.Content.ReadAsStringAsync();
            }
        }
    }
}