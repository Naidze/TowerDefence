using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Adapter
{
    public class CrawlerAdapter : Minion, IMinion
    {
        private readonly Crawler _crawler;

        public CrawlerAdapter(Crawler crawler, string name) : base(name, 20, 4, 15)
        {
            _crawler = crawler;
        }

        public bool Move()
        {
            return _crawler.Crawl();
        }
    }
}
