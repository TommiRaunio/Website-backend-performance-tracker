using System.Collections.Generic;

namespace WebsitePerformanceTracker.Configuration
{
    public class SiteConfigurationJson
    {
        public string siteurl { get; set; }
        public List<Page> pages { get; set; }
        
        public class Page
        {
            public string name { get; set; }
            public string url { get; set; }
        }

    }
}
