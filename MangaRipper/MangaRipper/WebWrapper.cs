using System;
using System.IO;
using System.Net;

namespace MangaRipper
{
    internal sealed class WebWrapper
    {
        private WebClient Client;

        public WebWrapper()
        {
            Client = new WebClient();
        }

        public string Get(string uri, DecompressionMethods compressionMethod = DecompressionMethods.None)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = compressionMethod;

            var result = request.GetResponse();
            using (var stream = new StreamReader(result.GetResponseStream()))
            {
                string response = stream.ReadToEnd();
                return response;
            }
        }

        public void DownloadFile(string uri, string path, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var url = new Uri(uri);
            Client.DownloadFile(url, Path.Combine(path, fileName));
        }
    }
}