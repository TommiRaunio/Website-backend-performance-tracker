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

        public IElasticClient GetElasticClient(ElasticSearchConfigurationJson configuration)
        {
            var local = new Uri(configuration.elasticSearchNodeAddress);
            var settings = new ConnectionSettings(local).DefaultIndex(configuration.indexName);
            var elastic = new ElasticClient(settings);

            elastic.CreateIndex(configuration.indexName);
            elastic.Map<TrackerStatistics>(m => m.AutoMap());

            Console.WriteLine($"Elasticsearch instance {configuration.elasticSearchNodeAddress}. Index is {configuration.indexName}");

            return elastic;
        }
    }
}
