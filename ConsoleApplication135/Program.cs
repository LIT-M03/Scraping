using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

//using CsQuery;

namespace ConsoleApplication135
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            //string html = client.DownloadString("http://www.matzav.com");

            //File.WriteAllText("matzav.html", html);
            string html = File.ReadAllText("matzav.html");

            //CQ dom = html;

            //var itemDetails = dom[".item-details"];
            //foreach (var div in itemDetails)
            //{
            //    CQ item = div.Cq();
            //    Console.WriteLine(item["a:eq(3)"].Text());
            //}



            //CQ anchors = dom["h4 a"];
            //foreach (IDomObject obj in anchors)
            //{
            //    Console.WriteLine(obj.InnerText);
            //}

            //var rows = dom[".item-details"];
            //foreach (var row in rows)
            //{
                
            //    //foreach (var item in row.ChildElements)
            //    //{
            //    //    Console.WriteLine(item.InnerText);
            //    //}
                
            //}


            var parser = new HtmlParser();
            var document = parser.Parse(html);
            IHtmlCollection<IElement> elements = document.QuerySelectorAll(".item-details");
            foreach (IElement element in elements)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine(element.QuerySelector("a").TextContent);
                var td = element.QuerySelector("div.td-excerpt");
                if (td != null)
                {
                    Console.WriteLine(td.TextContent);
                }
                
                Console.WriteLine();
            }


            Console.ReadKey(true);
        }
    }
}
