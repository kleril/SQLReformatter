using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SQLReformatter
{
    class FileDownloader
    {
        public static List<String> download()
        {
            string url = Program.downloadUrl;
            List<String> lines = new List<String>();
            using (WebClient client = new WebClient())
            {
                string downloaded = client.DownloadString(url);
                string[] lineArray = downloaded.Split(new char[]{'\n'});
                lines.AddRange(lineArray);
            }
            return lines;
        }
    }
}
