using System;
using WebsitePerformanceTracker.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;
using WebsitePerformanceTracker.Elasticsearch;
using WebsitePerformanceTracker.WebsiteTester;

namespace WebsitePerformanceTracker
{
    [TestClass]
    public class TestWebsite
    {
        [TestMethod]
        public void TestAllPages()
        {
            var configurationProvider = new ConfigurationProvider();           

            var siteConfiguration = configurationProvider.ProvideSiteConfiguration();
            var elasticConfiguration = configurationProvider.ProvideElasticSearchConfiguration();

            var elasticProvider = new ElasticSearchClientProvider(elasticConfiguration);
            var elasticClient = elasticProvider.GetElasticClient();

            var tracker = new Tracker(elasticClient);
            tracker.Track(siteConfiguration);
        }
    }
}
