using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace WebsitePerformanceTracker.Elasticsearch
{
    class TrackerStatistics
    {
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string Name { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string Url { get; set; }

        public int Elapsed { get; set; }

        public int RepeatCount { get; set; }

        public DateTime Timestamp { get; set; }

        public int HtmlSize { get; set; }

    }
}
