using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranzactChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var PageFile = (new PageFile());
            var file = (new DownloadFile());
            string pathFile = @"D:\Test\";
            var pagesToDownload = await PageFile.GetLastFivePagesOfCurrentDay();
            Console.WriteLine("The files will be downloaded");
            foreach (var item in pagesToDownload)
            {

               Console.WriteLine("File "+item.NameFile + " downloaded = "+await file.DownloadFileByUrl(item.Link,pathFile, item.NameFile));
              
            }
            Console.ReadKey();
            

        }

        
    }


}
