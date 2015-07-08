using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLReformatter
{
    class Program
    {
        private static File oldFile;
        static void Main(string[] args)
        {
            try
            {
                oldFile = FileDownloader.download();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var newFile = Reformatter.reformat(oldFile);
            FileUploader.upload(newFile);
        }
    }
}
