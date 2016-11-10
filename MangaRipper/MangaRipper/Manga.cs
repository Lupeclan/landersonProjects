using System.Configuration;

namespace MangaRipper
{
    internal class Manga
    {
        private double chapter;

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

        public double Volume { get; set; }
        public int ImageCount { get; set; }
        public string Name { get; private set; }
        public string MangaFoxUrl { get; private set; }

        public Manga()
        {
            Volume = 1;
            MangaFoxUrl = ConfigurationManager.AppSettings["MangaMainUrl"];
            Name = MangaFoxUrl.Split('/')[4];
        }
    }
}