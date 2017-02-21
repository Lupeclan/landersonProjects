using System.Configuration;
using System.IO;

namespace MangaRipper
{
    internal class Manga
    {
        private double chapter;
        private string fileFolder;
        private double volume;

        public double Chapter
        {
            get
            {
                return chapter;
            }
            set
            {
                ImageCount = 0;
                chapter = (value <= 0) ? 1 : value;
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

        public double Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = (value <= 0) ? 1 : value;
            }
        }

        public int ImageCount { get; set; }
        public string Name { get; private set; }
        public string MangaFoxUrl { get; private set; }

        public const string BASE_PAGE = "1.html";

        public Manga()
        {
            string volume = ConfigurationManager.AppSettings["Volume"];
            Volume = ParseString(volume);

            string chapter = ConfigurationManager.AppSettings["Chapter"];
            Chapter = ParseString(chapter);

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
            if (Chapter <= 1 || Volume <= 1)
            {
                url += "/v01/c000/" + BASE_PAGE;
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

        private double ParseString(string input)
        {
            double tempValue = 0;
            double.TryParse(input, out tempValue);
            return tempValue;
        }
    }
}