using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace MangaRipper
{
    internal sealed class HtmlHelper
    {
        private HtmlDocument Document;

        public HtmlHelper(string html)
        {
            Document = new HtmlDocument();
            Document.LoadHtml(html);
        }

        public HtmlNode GetNodeById(string id)
        {
            foreach (var node in Document.DocumentNode.Descendants())
            {
                if (node.Attributes["id"] != null && node.Attributes["id"].Value == id)
                {
                    return node;
                }
            }

            return null;
        }

        public IEnumerable<HtmlNode> GetDivs()
        {
            var imgNodes = Document.DocumentNode.Descendants("div");
            return imgNodes;
        }

        public IEnumerable<HtmlNode> GetImageNodes()
        {
            var imgNodes = Document.DocumentNode.Descendants("img");
            return imgNodes;
        }
    }

    internal static class HtmlHelperExtensions
    {
        public static HtmlNode GetNodeById(this IEnumerable<HtmlNode> source, string id)
        {
            var element = source.Where(n => n.GetAttributeValue("id", null) == id).FirstOrDefault();
            return element;
        }
    }
}