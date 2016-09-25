using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsitePerformanceTracker.Elasticsearch
{
    class ElasticSearchConfigurationJson
    {
        public string elasticSearchNodeAddress { get; set; }
        public string indexName { get; set; }
    }
}
