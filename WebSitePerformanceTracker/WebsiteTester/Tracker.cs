using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using WebsitePerformanceTracker.Configuration;
using WebsitePerformanceTracker.Elasticsearch;

namespace WebsitePerformanceTracker.WebsiteTester
{
    public class Tracker
    {
        private readonly IElasticClient _elasticClient;
        private const int RepeatCount = 10;

        public Tracker(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void Track(SiteConfigurationJson siteConfiguration)
        {
            Parallel.ForEach(siteConfiguration.pages, (webPage) =>
            {
                var url = GetPageUrl(webPage.url, siteConfiguration.siteurl);

                MeasureWebpage(webPage.name, url);
            });
        }

        private void MeasureWebpage(string name, string url)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Accept-Encoding: gzip, deflate, sdch, br");

            var ticks = new List<long>();
            string dom = "";

            for (int i = 0; i < 10; i++)
            {
                var watch = Stopwatch.StartNew();
                dom = webClient.DownloadString(url);
                watch.Stop();
                ticks.Add(watch.Elapsed.Ticks);
                Thread.Sleep(300);
            }

            var averageTicks = (long)ticks.Average();
            var averageSpan = new TimeSpan(averageTicks);
            var domSize = dom.Length*sizeof (char);

            Console.WriteLine($"{url}. Average load time {averageSpan.Milliseconds} ms. Dom size {domSize.ToString("n")} bytes");

            var statistics = new TrackerStatistics();
            statistics.Elapsed = averageSpan.Milliseconds;
            statistics.Timestamp = DateTime.Now;
            statistics.Name = name;
            statistics.RepeatCount = RepeatCount;
            statistics.HtmlSize = domSize;

            _elasticClient.Index(statistics);
        }

        private string GetPageUrl(string pageUrl, string siteurl)
        {
            var url = "";
            if (pageUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                url = pageUrl;
            }
            else
            {
                var baseUri = new Uri(siteurl);
                url = new Uri(baseUri, pageUrl).ToString();
            }
            return url;
        }
    }
}
