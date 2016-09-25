using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace WebsitePerformanceTracker.Elasticsearch
{
    class ElasticSearchClientProvider
    {
        private ElasticSearchConfigurationJson _configuration;

        public ElasticSearchClientProvider(ElasticSearchConfigurationJson configuration)
        {
            _configuration = configuration;
        }

        public IElasticClient GetElasticClient()
        {
            var local = new Uri(_configuration.elasticSearchNodeAddress);
            var settings = new ConnectionSettings(local).DefaultIndex(_configuration.indexName);
            var elastic = new ElasticClient(settings);

            elastic.CreateIndex(_configuration.indexName);
            elastic.Map<TrackerStatistics>(m => m.AutoMap());

            return elastic;
        }
    }
}
