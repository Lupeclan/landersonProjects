using HtmlAgilityPack;

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
    }
}