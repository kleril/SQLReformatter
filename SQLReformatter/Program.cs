using System;
using System.Configuration;
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
            initGlobals();

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
            Console.ReadLine();
        }

        public static string downloadUrl;
        public static string uploadUrl;

        private static void initGlobals()
        {
            try
            {
                downloadUrl = ConfigurationManager.ConnectionStrings["downloadUrl"].ConnectionString;
                uploadUrl = ConfigurationManager.ConnectionStrings["uploadUrl"].ConnectionString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
