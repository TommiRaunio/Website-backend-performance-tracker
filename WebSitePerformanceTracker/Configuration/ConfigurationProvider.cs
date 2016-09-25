using System.IO;
using Newtonsoft.Json;
using WebsitePerformanceTracker.Elasticsearch;

namespace WebsitePerformanceTracker.Configuration
{    
    class ConfigurationProvider
    {
        private const string WebsiteConfigurationFileName = "Website.json";
        private const string ElasticSearchConfigurationFileName = "Elasticsearch.json";

        public SiteConfigurationJson ProvideSiteConfiguration()
        {
            var fileContent = File.ReadAllText(WebsiteConfigurationFileName);            
            var configuration = new SiteConfigurationJson();

            if (!string.IsNullOrEmpty(fileContent))
            {
                configuration = JsonConvert.DeserializeObject<SiteConfigurationJson>(fileContent);
            }

            return configuration;
        }

        public ElasticSearchConfigurationJson ProvideElasticSearchConfiguration()
        {
            var fileContent = File.ReadAllText(ElasticSearchConfigurationFileName);
            var configuration = new ElasticSearchConfigurationJson();

            if (!string.IsNullOrEmpty(fileContent))
            {
                configuration = JsonConvert.DeserializeObject<ElasticSearchConfigurationJson>(fileContent);
            }

            return configuration;
        }
    }
}

