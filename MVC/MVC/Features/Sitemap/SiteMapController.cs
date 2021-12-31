﻿using Generic.Models;
using Generic.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Controllers.Administrative
{
    public class SiteMapController : Controller
    {

        private readonly ISiteMapRepository _siteMapRepository;

        public SiteMapController(ISiteMapRepository siteMapRepository)
        {
            _siteMapRepository = siteMapRepository;
        }

        // GET: SiteMap
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            var nodes = new List<SitemapNode>();
            // This site uses a custom navigation type, so we will base the Sitemap off of that.
            nodes.AddRange(await _siteMapRepository.GetSiteMapUrlSetAsync(new SiteMapOptions()
                {
                    Path = "/MasterPage/Navigation",
                    ClassNames = new string[] { "Generic.Navigation" },
                    UrlColumnName = "NavigationLinkUrl",
                    CacheItemName = "SiteMap"
                })
            );
            // Add custom SitemapNodes here if you wish
            nodes.AddRange(await _siteMapRepository.GetSiteMapUrlSetAsync(new SiteMapOptions()
            {
                Path = "/%",
                ClassNames = new string[] { "Generic.BasicPage" },
                // ClassNames = new string[] { "Generic.GenericPage" },
                CacheItemName = "SiteMapPages"
            }));
            // Now render manually, sadly the SimpleMVCSitemap disables output cache somehow
            return Content(GetSitemapXml(nodes), "text/xml", Encoding.UTF8);
        }

        /// <summary>
        /// Formats the Nodes into the proper XML
        /// </summary>
        /// <param name="Nodes"></param>
        /// <returns></returns>
        private static string GetSitemapXml(IEnumerable<SitemapNode> nodes)
        {
                return $"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">{string.Join("\n", nodes.Select(x => SitemapNodeToXmlString(x)))}</urlset>";
        }

        /// <summary>
        /// Formats the Node into the proper XML
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        private static string SitemapNodeToXmlString(SitemapNode node)
        {
            string changeFreq = string.Empty;
            if (node.ChangeFrequency.HasValue) {
                switch (node.ChangeFrequency.Value)
                {
                    case ChangeFrequency.Always:
                        changeFreq = "always";
                        break;
                    case ChangeFrequency.Daily:
                        changeFreq = "daily";
                        break;
                    case ChangeFrequency.Hourly:
                        changeFreq = "hourly";
                        break;
                    case ChangeFrequency.Monthly:
                        changeFreq = "monthly";
                        break;
                    case ChangeFrequency.Never:
                        changeFreq = "never";
                        break;
                    case ChangeFrequency.Weekly:
                        changeFreq = "weekly";
                        break;
                    case ChangeFrequency.Yearly:
                        changeFreq = "yearly";
                        break;
                }
            }

            return string.Format("<url>{0}{1}{2}{3}</url>",
                $"<loc>{node.Url}</loc>",
                node.LastModificationDate.HasValue ? $"<lastmod>{node.LastModificationDate.Value.ToString("yyyy-MM-ddTHH:mm:sszzz")}</lastmod>" : "",
                !string.IsNullOrWhiteSpace(changeFreq) ? $"<changefreq>{changeFreq}</changefreq>" : "",
                node.Priority.HasValue ? $"<priority>{node.Priority.Value}</priority>" : ""
                );
        }
    }
}