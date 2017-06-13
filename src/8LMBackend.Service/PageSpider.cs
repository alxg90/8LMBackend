using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;

namespace _8LMBackend.Service
{
    public class PageSpider
    {
        const string localNameResolution = @"http://ang.mark8.media/";
        string HTML;
        Dictionary<string, Guid> src;
        Dictionary<string, Guid> href;

        public PageSpider(string HTML)
        {
            this.HTML = HTML;
        }

        Dictionary<string, Guid> Parse(string s)
        {
            Dictionary<string, Guid> result = new Dictionary<string, Guid>();
            int index = 0;
            while (true)
            {
                index = HTML.IndexOf(s, index);
                if (index > 0)
                {
                    int i = HTML.IndexOf("\"", index + s.Length + 1);
                    string sr = HTML.Substring(index + s.Length, i - index - s.Length);
                    index += sr.Length + s.Length;

                    if (sr.Contains(".."))
                        result.Add(sr, Guid.NewGuid());
                }
                else
                    break;
            }
            return result;
        }

        public MemoryStream Load()
        {
            src = Parse(" src=\"");
            href = Parse(" href=\"");

            return ZIP();
        }

        MemoryStream ZIP()
        {
            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                //Add SRC files
                foreach (var item in src)
                {
                    try
                    {
                        AddZIPEntry(archive, item.Key, item.Value.ToString());
                        HTML = HTML.Replace(item.Key, item.Value.ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                }

                //Add HREF files
                foreach (var item in href)
                {
                    try
                    {
                        AddZIPEntry(archive, item.Key, item.Value.ToString());
                        HTML = HTML.Replace(item.Key, item.Value.ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                }

                //Add HTML file
                var file = archive.CreateEntry("index.html");
                using (var entryStream = file.Open())
                using (var streamWriter = new StreamWriter(entryStream))
                {
                    streamWriter.Write(HTML);
                }
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        void AddZIPEntry(ZipArchive archive, string URL, string Name)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 5);
                var response = client.GetAsync(URL.Replace("..", localNameResolution)).Result;
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = response.Content.ReadAsStreamAsync().Result)
                    {
                        var file = archive.CreateEntry(Name);
                        using (var entryStream = file.Open())
                        {
                            stream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }
    }
}
