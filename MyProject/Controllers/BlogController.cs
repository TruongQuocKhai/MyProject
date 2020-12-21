using CommonModels.ADO;
using CommonModels.EF;
using MyProject.CommonRSS;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MyProject.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        MyProjectDb db = new MyProjectDb();
        public ActionResult Index()
        {
            var model = new ContentCategoryADO().GetContentCategory();
            return View(model);
        }

        public PartialViewResult Partial_ContentCategory()
        {
            var model = new ContentCategoryADO().GetContentCategory();
            return PartialView(model);
        }

        public ActionResult PostFeed(string type)
        {
            // lay chuyen muc
            ContentCategory contentCate = db.ContentCategories.Where(x => x.MetaTitle.Contains(type)).FirstOrDefault();
            // neu chuyen muc khong ton tai, tra ve NotFound
            if (contentCate == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Content> posts = (from s in db.Contents where s.MetaTitle.Contains(type) select s).ToList();
            var feed = new SyndicationFeed(contentCate.Name, "RSS feed",
                new Uri("http://localhost:51530/RSS"),
                Guid.NewGuid().ToString(),
                DateTime.Now);

            var items = new List<SyndicationItem>();
            foreach (Content content in posts)
            {
                string postUrl = String.Format("http://localhost:51530/" + content.MetaTitle + "-{0}", content.ID);
                var item = new SyndicationItem(Helper.RemoveIllegalCharacters(content.Title),
                    Helper.RemoveIllegalCharacters(content.Description),
                    new Uri(postUrl),
                    content.ID.ToString(),
                    content.CreatedDate.Value
                    );
                items.Add(item);
            }
            feed.Items = items;
            return new RSSResult { Feed = feed };
        }
        // View page Read RSS
        public ActionResult ReadRSS()
        {
            return View();
        }

        // Read RSS from URL
        [HttpPost]
        public ActionResult ReadRSS(string url)
        {
            WebClient wclient = new WebClient();
            wclient.Encoding = ASCIIEncoding.UTF8;
            string RSSData = wclient.DownloadString(url);

            XDocument xml = XDocument.Parse(RSSData, LoadOptions.PreserveWhitespace);
            var RSSFeed = (from x in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   CreatedDate = ((string)x.Element("pubDate"))
                               });

            ViewBag.RSSFeed = RSSFeed;
            ViewBag.URL = url;
            return View();
        }

        // Page Contact
        public PartialViewResult Contact()
        {
            return PartialView();
        }
    }

}