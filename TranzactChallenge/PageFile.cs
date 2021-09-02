using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranzactChallenge
{
    public class PageFile
    {


        public async Task<List<AllHour>> GetLastFivePagesOfCurrentDay()
        {

            var links = await GetAllPagesByDate(DateTime.Now);

            //Dates
            DateTime currentDateWithoutFormat = DateTime.Now;
            string currentDate = DateTime.Now.ToString("yyyyMMdd"); 
            int currentHour = int.Parse(DateTime.Now.ToString("HH"));
            //fechas documents
            string pageDate = "";
            int pageHour = 0;
            AllHour page;
            List<AllHour> pagesSelectedToDownload = new List<AllHour>();
            List<AllHour> pages = new List<AllHour>();
            int nItemsFounded = 0;
            foreach (var Nodo in links)
            {
                page = new AllHour();
                page.NameFile = Nodo.Attributes["href"].Value.ToString();
                page.Link = GetUrlFilePages(currentDateWithoutFormat)+page.NameFile;
               
                if (page.NameFile != "../")
                {
                    pageDate = page.NameFile.Substring(10, 8);
                    pageHour = int.Parse(page.NameFile.Substring(19, 2));
                    if (pageDate == currentDate)

                        if (pageDate == currentDate)
                        {
                            if (pageHour <= currentHour)
                                pages.Add(page);
                        }
                }
            }
            nItemsFounded = pages.Count();
            if (nItemsFounded < 5)
            {
                var yesterdayDate = DateTime.Now.AddDays(-1);
                pages =await GetLastPagesOfBeforeDay(yesterdayDate, nItemsFounded, pages);
                pagesSelectedToDownload = pages;
            }
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    pagesSelectedToDownload.Add(pages[nItemsFounded - i]);
                }
            }
          
            return pagesSelectedToDownload;
        }
        public async Task<List<AllHour>> GetLastPagesOfBeforeDay(DateTime yesterdayDate, int nItemsFound, List<AllHour> pagesFounded)
        {
            AllHour page;
            string pageDate = "";
            int pageHour = 0;
            //date
            DateTime yesterdayDateWithoutFormat = yesterdayDate;
            string yesterdayString = yesterdayDate.ToString("yyyyMMdd");
            int yesterdayMaxHour = 23;

            var PageLinks =await GetAllPagesByDate(yesterdayDate);
            int nItemsFoundInYesterday = PageLinks.Count()-1;
            for (int i = nItemsFoundInYesterday; i <= nItemsFoundInYesterday - 5; i--)
            {
                page = new AllHour();
                page.NameFile = PageLinks[i].Attributes["href"].Value.ToString();
                page.Link = GetUrlFilePages(yesterdayDateWithoutFormat) + page.NameFile;
                if (page.NameFile != "../")
                {

                    pageDate = page.NameFile.Substring(10, 8);
                    pageHour = int.Parse(page.NameFile.Substring(19, 2));
                    if (pageDate == yesterdayString)
                        {
                            if (pageHour <= yesterdayMaxHour)
                            pagesFounded.Add(page);
                        }
                }
            }
            return pagesFounded;
        }

        public async Task<List<HtmlNode>> GetAllPagesByDate(DateTime date)
        {
            string url = GetUrlFilePages(date);
            List<string> Titulos = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc =await web.LoadFromWebAsync(url);
            var Pagelinks = doc.DocumentNode.SelectNodes("//a[@href]").ToList();
            return Pagelinks;
        }

        public string GetUrlFilePages(DateTime date)
        {
            string year = date.ToString("yyyy");
            string yearMonth = date.ToString("yyyy-MM");
            string url = "https://dumps.wikimedia.org/other/pageviews/" + year + "/" + yearMonth + "/";

            return url;
        }
    }
}
