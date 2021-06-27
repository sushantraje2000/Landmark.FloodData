
using Landmark.FloodData.Response;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using Landmark.FloodData.Models;
using AutoFixture;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnitTest.Landmark.FloodData
{
    [TestFixture]
    public class FloodResponseTest
    {
        private FloodResponse _FloodResponse;
        private IHttpClientUtility _httpClientUtility;
        [SetUp]
        public void Setup()
        {
            _httpClientUtility = Substitute.For<IHttpClientUtility>();
            _FloodResponse = new FloodResponse(_httpClientUtility);
        }
        [Test]
        public async Task When_getFloods_APIRespondNull_Then_Should_Return_EmptyList()
        {
            var result = await _FloodResponse.getFloods();
            Assert.IsTrue(result.Count() == 0);
        }
        [Test]
        public async Task When_getFloods_APIRespondString_Then_Should_Match()
        {
            var fixture = new Fixture();
            var environmentAgencyFloodAlertServicePayload = fixture.Create<EnvironmentAgencyFloodAlertServicePayload>();
            _httpClientUtility.GetAsync().Returns(JsonConvert.SerializeObject(environmentAgencyFloodAlertServicePayload));
            var data = environmentAgencyFloodAlertServicePayload.Items.Select(item => new Flood()
            {
                Id = item.Id,
                Region = item.EaRegionName,
                FloodAreaId = item.FloodAreaId,
                EaAreaName = item.EaAreaName,
                TimeRaised = item.TimeRaised,
                Severity = (SeverityLevel)item.SeverityLevel
            });

            var result = await _FloodResponse.getFloods();
            Assert.IsTrue(result.Count() == 3);
            Assert.AreEqual(JsonConvert.SerializeObject(data), JsonConvert.SerializeObject(result));
        }
        [Test]
        public async Task When_getFloodsByRegion_APIRespondString_Then_Should_Match()
        {
            var fixture = new Fixture();
            var region = fixture.Create<string>();
            var environmentAgencyFloodAlertServicePayload = fixture.Create<EnvironmentAgencyFloodAlertServicePayload>();
            var listEnvironmentAgencyFloodAlert = fixture.CreateMany<EnvironmentAgencyFloodAlert>(5).ToList();
            listEnvironmentAgencyFloodAlert.Take(3).ToList().ForEach(a => a.EaRegionName = region);
            environmentAgencyFloodAlertServicePayload.Items = listEnvironmentAgencyFloodAlert;
            _httpClientUtility.GetAsync().Returns(JsonConvert.SerializeObject(environmentAgencyFloodAlertServicePayload));
            var data = environmentAgencyFloodAlertServicePayload.Items.Where(a => a.EaRegionName == region).Select(item => new Flood()
            {
                Id = item.Id,
                Region = item.EaRegionName,
                FloodAreaId = item.FloodAreaId,
                EaAreaName = item.EaAreaName,
                TimeRaised = item.TimeRaised,
                Severity = (SeverityLevel)item.SeverityLevel
            });

            var result = await _FloodResponse.getFloodsByRegion(region);
            Assert.IsTrue(result.Count() == 3);
            Assert.AreEqual(JsonConvert.SerializeObject(data), JsonConvert.SerializeObject(result));
        }
    }
}
