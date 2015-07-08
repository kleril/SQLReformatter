using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SQLReformatter
{
    class FileUploader
    {
        public static void upload(List<String> f)
        {
            string url = Program.downloadUrl;
            using (WebClient client = new WebClient())
            {
                client.UploadFile(url, fileName);
            }
        }
    }
}
