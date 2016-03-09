using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using AngleSharp.Parser.Html;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetTrendingRepos());
        }

        private IEnumerable<GithubTrendingRepo> GetTrendingRepos()
        {
            var parser = new HtmlParser();
            var client = new WebClient();
            client.Headers["User-Agent"] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
            string html = client.DownloadString("https://github.com/trending");
            //System.IO.File.WriteAllText(Server.MapPath("~/content") + "/foo.html", html);
            var document = parser.Parse(html);
            var list = new List<GithubTrendingRepo>();
            
            var lis = document.QuerySelectorAll(".repo-list-item");
            foreach (var li in lis)
            {
                var repo = new GithubTrendingRepo();
                var anchor = li.QuerySelectorAll("a")[1];
                repo.Name = anchor.TextContent;
                repo.Url = anchor.GetAttribute("href");
                repo.Blurb = li.QuerySelector(".repo-list-description").TextContent;
                var meta = li.QuerySelector(".repo-list-meta").TextContent;
                repo.Language = meta.Split('•')[0];
                list.Add(repo);
            }

            return list;
        }
    }

    public class GithubTrendingRepo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public string Blurb { get; set; }
    }

}
