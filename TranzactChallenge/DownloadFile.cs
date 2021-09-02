using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace TranzactChallenge
{
    public class DownloadFile
    {
        

        public async Task<bool> DownloadFileByUrl(string url,string path,string fileName)
        {
            try
            {
                
                Uri uri = new Uri(url);
                string pathFile = CreateDirectory(path);
                WebClient mywebClient = new WebClient();
                await mywebClient.DownloadFileTaskAsync(uri, pathFile + fileName);
                
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
        private string CreateDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return path;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception)
            {
                return "x";
            }
        }

    }
}
