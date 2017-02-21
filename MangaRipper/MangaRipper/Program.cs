using System;
using System.Linq;
using System.Net;

namespace MangaRipper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var currentManga = new Manga();            
            var web = new WebWrapper();

            string url = currentManga.GetUrl(Manga.BASE_PAGE);
            bool basePageReached = false;
            while (!basePageReached)
            {
                string page = web.Get(url, DecompressionMethods.GZip);

                var html = new HtmlHelper(page);
                var mainImageElement = html.GetNodeById("image");
                var currentImageUrl = string.Empty;
                try
                {
                    currentImageUrl = mainImageElement.GetAttributeValue("src", null);
                }
                catch (Exception)
                {
                    basePageReached = true;
                }

                if (currentImageUrl.IsValid())
                {
                    currentManga.ImageCount++;                   
                    string fileName = string.Format("{0}.jpg", currentManga.ImageCount);
                    web.DownloadFile(currentImageUrl, currentManga.FilePath, fileName);

                    var imageParentElement = mainImageElement.ParentNode;
                    string nextPage = imageParentElement.Attributes["href"].Value;
                    
                    if (nextPage == "javascript:void(0);")
                    {
                        var navElement = html.GetNodeById("chnav");
                        var aElements = navElement.Descendants("a");
                        var nextChapterLinkElement = aElements.Last();
                        var nextChapterLink = nextChapterLinkElement.GetAttributeValue("href", null);

                        currentManga.SetVolumeFromUrl(nextChapterLink);
                        currentManga.SetChapterFromUrl(nextChapterLink);
                        url = currentManga.GetUrl(Manga.BASE_PAGE);
                    }
                    else
                    {
                        url = currentManga.GetUrl(nextPage);
                    }
                }
            }
        }
    }
}