using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLReformatter
{
    class Program
    {
        private static List<String> oldFile;
        private static string outputFile = Directory.GetCurrentDirectory();
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
            Console.WriteLine(outputFile);

            System.IO.File.WriteAllLines(outputFile, newFile);

            FileUploader.upload(outputFile);
            Console.ReadLine();

			FileDownloader.downloadFileFromTFS ();

        }

        public static string downloadUrl;
        public static string uploadUrl;
        private static string newFileName;

        private static void initGlobals()
        {
            try
            {
                downloadUrl = ConfigurationManager.ConnectionStrings["downloadUrl"].ConnectionString;
                uploadUrl = ConfigurationManager.ConnectionStrings["uploadUrl"].ConnectionString;
                newFileName = ConfigurationManager.ConnectionStrings["newFileName"].ConnectionString;
                outputFile = outputFile + "\\" +  newFileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
