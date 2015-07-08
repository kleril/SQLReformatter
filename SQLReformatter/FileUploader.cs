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
        public static void upload(string fileName)
        {
            string url = Program.downloadUrl;
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.UploadFile(url, fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
