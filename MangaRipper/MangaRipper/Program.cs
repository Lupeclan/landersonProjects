using System.Configuration;
using System.IO;
using System.Net;

namespace MangaRipper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string fileFolder = ConfigurationManager.AppSettings["DownloadPath"];

            var currentManga = new Manga();            
            var web = new WebWrapper();

            string url = BuildUrl(currentManga);
            bool basePageReached = false;
            while (!basePageReached)
            {
                string page = web.Get(url, DecompressionMethods.GZip);

                var html = new HtmlHelper(page);
                var imageElements = html.GetImageNodes();
                var mainImageElement = imageElements.GetNodeById("image");
                var currentImageUrl = mainImageElement.GetAttributeValue("src", null);

                if (currentImageUrl.IsValid())
                {
                    currentManga.ImageCount++;
                    string path = Path.Combine(fileFolder, currentManga.Name, "vol " + currentManga.Volume.ToString(), "chapter " + currentManga.Chapter.ToString());
                    string fileName = string.Format("{0}.jpg", currentManga.ImageCount);
                    web.DownloadFile(currentImageUrl, path, fileName);

                    var mainImageParentElement = mainImageElement.ParentNode;
                    var nextPage = mainImageParentElement.GetAttributeValue("href", null);
                    if (nextPage == "javascript:void(0);")
                    {
                        currentManga.Chapter++;
                        url = BuildUrl(currentManga, "1.html");
                    }
                    else
                    {
                        url = BuildUrl(currentManga, nextPage);
                    }  
                }
            }
        }

        private static string BuildUrl(Manga currentManga, string pageName = null)
        {
            string url = currentManga.MangaFoxUrl;
            if (pageName == null)
            {
                url += "/v01/c000/1.html";
            }
            else
            {
                string volume = "v";
                if (currentManga.Volume < 10)
                {
                    volume += "0";
                }

                volume += currentManga.Volume;

                string chapter = "c";
                if (currentManga.Chapter < 10)
                {
                    chapter += "00";
                }
                else if (currentManga.Chapter < 100)
                {
                    chapter += "0";
                }

                chapter += currentManga.Chapter;

                url += string.Format("/{0}/{1}/{2}", volume, chapter, pageName);
            }

            return url;
        }
    }
}