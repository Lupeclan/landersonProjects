using System.Configuration;

namespace MangaRipper
{
    internal class Manga
    {
        private int volume;
        private int chapter;

        public int Chapter
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

        public int Volume { get; set; }
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