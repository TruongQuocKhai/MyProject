using CommonModels.ADO;
using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using System.Xml.Linq;
using System.Globalization;
using System.Text;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // Sitemap xml
        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            nodes.Add(
                new SitemapNode()
                {
                    Url = Url.Action("Index", "Home", null, Request.Url.Scheme),
                    Priority = 1
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = Url.Action("AllProducts", "Product", null, Request.Url.Scheme),
                   Priority = 0.9
               });
            //nodes.Add(
            //   new SitemapNode()
            //   {
            //       Url = Url.Action("Contact", "Home", null, Request.Url.Scheme),
            //       Priority = 0.9
            //   });
            var listProducts = new ProductADO().ListProducts();
            int countPage = (int)Math.Ceiling(listProducts.Count() / 10.0);
            for (int page = 1; page <= countPage; page++)
            {
                foreach (var product in listProducts)
                {
                    nodes.Add(
                       new SitemapNode()
                       {
                           Url = Url.Action("ProductDetail", "Product", new { id = product.ID, page = page }, Request.Url.Scheme),
                           Frequency = SitemapFrequency.Weekly,
                           Priority = 0.8
                       });
                }
            }
            //foreach (var product in listProducts)
            //{
            //    nodes.Add(
            //       new SitemapNode()
            //       {
            //           Url = Url.Action("ProductDetail", "Product", new { id = product.ID }, Request.Url.Scheme),
            //           Frequency = SitemapFrequency.Weekly,
            //           Priority = 0.8
            //       });
            //}
            return nodes;
        }
        //Get sitemap document
        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                xmlns + "url",
                new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                sitemapNode.LastModified == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                sitemapNode.Frequency == null ? null : new XElement(
                xmlns + "changefreq",
                sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var sitemapNodes = GetSitemapNodes();
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }
    }
}