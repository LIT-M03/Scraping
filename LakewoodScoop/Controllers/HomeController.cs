using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AngleSharp.Parser.Html;

namespace LakewoodScoop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetStories());
        }

        [HttpPost]
        public ActionResult GetDetails(string url)
        {
            return Json(GetText(url));
        }

        private LakewoodScoopStory GetText(string url)
        {
            var parser = new HtmlParser();
            var client = new WebClient();
            var html = client.DownloadString(url);
            var document = parser.Parse(html);

            var post = document.QuerySelector(".post");
            return new LakewoodScoopStory
            {
                Html = post.InnerHtml
            };
        }

        private IEnumerable<LakewoodScoopHeadline> GetStories()
        {
            var parser = new HtmlParser();
            var client = new WebClient();
            var html = client.DownloadString("http://www.thelakewoodscoop.com");
            var document = parser.Parse(html);

            var posts = document.QuerySelectorAll(".post");
            var result = new List<LakewoodScoopHeadline>();
            foreach (var post in posts)
            {
                var story = new LakewoodScoopHeadline();
                var anchor = post.QuerySelector("h2 a");
                story.Title = anchor.TextContent;
                story.Url = anchor.GetAttribute("href");
                story.Date = post.QuerySelector("small").TextContent;
                var image = post.QuerySelector("img");
                if (image != null)
                {
                    story.ImageUrl = image.GetAttribute("src");
                }

                var p = post.QuerySelector("p");
                story.Blurb = p.TextContent;
                result.Add(story);

            }

            return result;

        }

    }

    public class LakewoodScoopStory
    {
        public string Html { get; set; }
    }

    public class LakewoodScoopHeadline
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public string Blurb { get; set; }
    }
}
