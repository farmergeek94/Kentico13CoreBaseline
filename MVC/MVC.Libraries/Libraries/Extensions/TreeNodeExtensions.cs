﻿using CMS.DocumentEngine;
using CMS.Helpers;
using Generic.Models;
using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kentico.PageBuilder.Web.Mvc
{
    public static class TreeNodeExtensionsForComponentViewModel
    {
        public static PageIdentity ToPageIdentity(this TreeNode node)
        {
            return CMS.DocumentEngine.TreeNodeExtensions.ToPageIdentity(node);
        }
    }
}
namespace CMS.DocumentEngine
{
    public static class TreeNodeExtensions
    {
        /// <summary>
        /// Converts a TreeNode to the PageIdentity, useful for pages retrieved througH PageBuilder
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static PageIdentity ToPageIdentity(this TreeNode node)
        {
            var pageIdentity = new PageIdentity()
            {
                NodeID = node.NodeID,
                NodeGUID = node.NodeGUID,
                DocumentID = node.DocumentID,
                DocumentGUID = node.DocumentGUID,
                Path = node.NodeAliasPath,
                Alias = node.NodeAlias,
                Name = node.DocumentName,
                NodeLevel = node.NodeLevel
            };

            var relativeAndAbsoluteUrl = CacheHelper.Cache(cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency($"documentid|{pageIdentity.DocumentID}");
                }
                try
                {
                    return new Tuple<string, string>(DocumentURLProvider.GetUrl(node), DocumentURLProvider.GetAbsoluteUrl(node));
                }
                catch (Exception)
                {
                    // Will need to re-query the page, must be missing columns
                }
                if (pageIdentity.DocumentID > 0)
                {
                    // get full page
                    var fullNode = new DocumentQuery()
                    .WhereEquals(nameof(TreeNode.DocumentID), node.DocumentID)
                    .EnsureUrls()
                    .GetEnumerableTypedResult()
                    .FirstOrDefault();
                    return new Tuple<string, string>(DocumentURLProvider.GetUrl(node), DocumentURLProvider.GetAbsoluteUrl(node));
                } else
                {
                    return new Tuple<string, string>(string.Empty, string.Empty);
                }
            }, new CacheSettings(10, "GetNodeUrlsForPageIdentity", node.DocumentID));
            pageIdentity.RelativeUrl = relativeAndAbsoluteUrl.Item1.Replace("~", "");
            pageIdentity.AbsoluteUrl = relativeAndAbsoluteUrl.Item2.Replace("~", "");
            return pageIdentity;
        }
    }
}
