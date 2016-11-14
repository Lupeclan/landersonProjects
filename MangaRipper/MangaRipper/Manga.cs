using System.Configuration;
using System.IO;

namespace MangaRipper
{
    internal class Manga
    {
        private double chapter;
        private string fileFolder;

        public double Chapter
        {
            get
            {
                return chapter;
            }
            set
            {
                ImageCount = 0;
                chapter = value;
            }
        }

        public string FilePath
        {
            get
            {
                string path = Path.Combine(fileFolder, Name, "vol " + Volume.ToString(), "chapter " + Chapter.ToString());
                return path;
            }
        }

        public double Volume { get; set; }
        public int ImageCount { get; set; }
        public string Name { get; private set; }
        public string MangaFoxUrl { get; private set; }

        public Manga()
        {
            Volume = 1;
            MangaFoxUrl = ConfigurationManager.AppSettings["MangaMainUrl"];
            fileFolder = ConfigurationManager.AppSettings["DownloadPath"];
            Name = MangaFoxUrl.Split('/')[4];
        }

        public void SetVolumeFromUrl(string url)
        {
            var split = url.Split('/');
            string volume = split[5].Substring(1);
            Volume = double.Parse(volume);
        }

        public void SetChapterFromUrl(string url)
        {
            var split = url.Split('/');
            string chapter = split[6].Substring(1);
            Chapter = double.Parse(chapter);
        }

        public string GetUrl(string pageName = null)
        {
            string url = MangaFoxUrl;
            if (pageName == null)
            {
                url += "/v01/c000/1.html";
            }
            else
            {
                string volume = "v";
                if (Volume < 10)
                {
                    volume += "0";
                }

                volume += Volume;

                string chapter = "c";
                if (Chapter < 10)
                {
                    chapter += "00";
                }
                else if (Chapter < 100)
                {
                    chapter += "0";
                }

                chapter += Chapter;

                url += string.Format("/{0}/{1}/{2}", volume, chapter, pageName);
            }

            return url;
        }
    }
}