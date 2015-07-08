using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLReformatter
{
    class Program
    {
        private static List<String> oldFile;
        static void Main(string[] args)
        {
            try
            {
                oldFile = FileDownloader.download();
                foreach (String nl in oldFile)
                {
                    Console.WriteLine(nl);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //var newFile = Reformatter.reformat(oldFile);
            //FileUploader.upload(newFile);
            Console.ReadLine();
        }
    }
}
